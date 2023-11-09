//
// Kino/Bloom v2 - Bloom filter for Unity
//
// Copyright (C) 2015, 2016 Keijiro Takahashi
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

// Modified by Jean Moreno for Cartoon FX Remaster Demo
// - effect previews in SceneView
// - disabled a code warning

using UnityEngine;

namespace Kino {
    [ExecuteInEditMode]
    [RequireComponent( typeof( Camera ) )]
    [ImageEffectAllowedInSceneView]
    public class Bloom : MonoBehaviour {
        #region Public Properties

        /// Prefilter threshold (gamma-encoded)
        /// Filters out pixels under this level of brightness.
        public float ThresholdGamma {
            get { return Mathf.Max( _threshold, 0 ); }
            set { _threshold = value; }
        }

        /// Prefilter threshold (linearly-encoded)
        /// Filters out pixels under this level of brightness.
        public float ThresholdLinear {
            get { return GammaToLinear( ThresholdGamma ); }
            set { _threshold = LinearToGamma( value ); }
        }

        [SerializeField]
        [Tooltip( "Filters out pixels under this level of brightness." )]
        private float _threshold = 0.8f;

        /// Soft-knee coefficient
        /// Makes transition between under/over-threshold gradual.
        public float SoftKnee {
            get { return _softKnee; }
            set { _softKnee = value; }
        }

        [SerializeField, Range( 0, 1 )]
        [Tooltip( "Makes transition between under/over-threshold gradual." )]
        private float _softKnee = 0.5f;

        /// Bloom radius
        /// Changes extent of veiling effects in a screen
        /// resolution-independent fashion.
        public float Radius {
            get { return _radius; }
            set { _radius = value; }
        }

        [SerializeField, Range( 1, 7 )]
        [Tooltip( "Changes extent of veiling effects\n" +
                 "in a screen resolution-independent fashion." )]
        private float _radius = 2.5f;

        /// Bloom intensity
        /// Blend factor of the result image.
        public float Intensity {
            get { return Mathf.Max( _intensity, 0 ); }
            set { _intensity = value; }
        }

        [SerializeField]
        [Tooltip( "Blend factor of the result image." )]
        private float _intensity = 0.8f;

        /// High quality mode
        /// Controls filter quality and buffer resolution.
        public bool HighQuality {
            get { return _highQuality; }
            set { _highQuality = value; }
        }

        [SerializeField]
        [Tooltip( "Controls filter quality and buffer resolution." )]
        private bool _highQuality = true;

        /// Anti-flicker filter
        /// Reduces flashing noise with an additional filter.
        [SerializeField]
        [Tooltip( "Reduces flashing noise with an additional filter." )]
        private bool _antiFlicker = true;

        public bool AntiFlicker {
            get { return _antiFlicker; }
            set { _antiFlicker = value; }
        }

        #endregion

        #region Private Members

#pragma warning disable 0649
        [SerializeField, HideInInspector]
        private Shader _shader;
#pragma warning restore 0649

        private Material _material;

#pragma warning disable IDE0044 // Add readonly modifier

        private const int kMaxIterations = 16;
        private RenderTexture[ ] _blurBuffer1 = new RenderTexture[ kMaxIterations ];
        private RenderTexture[ ] _blurBuffer2 = new RenderTexture[ kMaxIterations ];
#pragma warning restore IDE0044 // Add readonly modifier

        private float LinearToGamma( float x ) {
#if UNITY_5_3_OR_NEWER
            return Mathf.LinearToGammaSpace( x );
#else
			if (x <= 0.0031308f)
				return 12.92f * x;
			else
				return 1.055f * Mathf.Pow(x, 1 / 2.4f) - 0.055f;
#endif
        }

        private float GammaToLinear( float x ) {
#if UNITY_5_3_OR_NEWER
            return Mathf.GammaToLinearSpace( x );
#else
			if (x <= 0.04045f)
				return x / 12.92f;
			else
				return Mathf.Pow((x + 0.055f) / 1.055f, 2.4f);
#endif
        }

        #endregion

        #region MonoBehaviour Functions

        private void OnEnable( ) {
            Shader shader = _shader ? _shader : Shader.Find( "Hidden/Kino/Bloom" );
            _material = new Material( shader ) {
                hideFlags = HideFlags.DontSave
            };
        }

        private void OnDisable( ) {
            DestroyImmediate( _material );
        }

        private void OnRenderImage( RenderTexture source, RenderTexture destination ) {
            bool useRGBM = Application.isMobilePlatform;

            // source texture size
            int tw = source.width;
            int th = source.height;

            // halve the texture size for the low quality mode
            if ( !_highQuality ) {
                tw /= 2;
                th /= 2;
            }

            // blur buffer format
            RenderTextureFormat rtFormat = useRGBM ?
                RenderTextureFormat.Default : RenderTextureFormat.DefaultHDR;

            // determine the iteration count
            float logh = Mathf.Log( th, 2 ) + _radius - 8;
            int logh_i = ( int ) logh;
            int iterations = Mathf.Clamp( logh_i, 1, kMaxIterations );

            // update the shader properties
            float lthresh = ThresholdLinear;
            _material.SetFloat( "_Threshold", lthresh );

            float knee = ( lthresh * _softKnee ) + 1e-5f;
            Vector3 curve = new( lthresh - knee, knee * 2, 0.25f / knee );
            _material.SetVector( "_Curve", curve );

            bool pfo = !_highQuality && _antiFlicker;
            _material.SetFloat( "_PrefilterOffs", pfo ? -0.5f : 0.0f );

            _material.SetFloat( "_SampleScale", 0.5f + logh - logh_i );
            _material.SetFloat( "_Intensity", Intensity );

            // prefilter pass
            RenderTexture prefiltered = RenderTexture.GetTemporary( tw, th, 0, rtFormat );
            int pass = _antiFlicker ? 1 : 0;
            Graphics.Blit( source, prefiltered, _material, pass );

            // construct a mip pyramid
            RenderTexture last = prefiltered;
            for ( int level = 0; level < iterations; level++ ) {
                _blurBuffer1[ level ] = RenderTexture.GetTemporary(
                    last.width / 2, last.height / 2, 0, rtFormat
                );

                pass = ( level == 0 ) ? ( _antiFlicker ? 3 : 2 ) : 4;
                Graphics.Blit( last, _blurBuffer1[ level ], _material, pass );

                last = _blurBuffer1[ level ];
            }

            // upsample and combine loop
            for ( int level = iterations - 2; level >= 0; level-- ) {
                RenderTexture basetex = _blurBuffer1[ level ];
                _material.SetTexture( "_BaseTex", basetex );

                _blurBuffer2[ level ] = RenderTexture.GetTemporary(
                    basetex.width, basetex.height, 0, rtFormat
                );

                pass = _highQuality ? 6 : 5;
                Graphics.Blit( last, _blurBuffer2[ level ], _material, pass );
                last = _blurBuffer2[ level ];
            }

            // finish process
            _material.SetTexture( "_BaseTex", source );
            pass = _highQuality ? 8 : 7;
            Graphics.Blit( last, destination, _material, pass );

            // release the temporary buffers
            for ( int i = 0; i < kMaxIterations; i++ ) {
                if ( _blurBuffer1[ i ] != null )
                    RenderTexture.ReleaseTemporary( _blurBuffer1[ i ] );

                if ( _blurBuffer2[ i ] != null )
                    RenderTexture.ReleaseTemporary( _blurBuffer2[ i ] );

                _blurBuffer1[ i ] = null;
                _blurBuffer2[ i ] = null;
            }

            RenderTexture.ReleaseTemporary( prefiltered );
        }

        #endregion
    }
}
