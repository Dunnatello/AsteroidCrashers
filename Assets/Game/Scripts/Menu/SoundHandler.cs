/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Sound Handler
 * Script Objective: Allows the player to change their audio settings in the "Settings" scene.
 * 
 * Source(s): https://docs.unity3d.com/2018.2/Documentation/ScriptReference/UI.Slider-onValueChanged.html
 *            
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace TeamBracket.Audio {
    public class SoundHandler : MonoBehaviour {

        [SerializeField] private List<Slider> sliderList = new( );
        [SerializeField] private List<AudioMixer> audioMixerList = new( );

        // Dictionaries can't be serialized in Unity, so lists are used to populate and save the references,
        // and then the dictionaries are created in Start().
        private readonly Dictionary<string, Slider> musicSliders = new( );
        private readonly Dictionary<string, AudioMixer> audioMixers = new( );

        private void Start( ) {

            // Populate dictionaries.
            foreach ( var musicGroup in audioMixerList ) {

                Debug.Log( "GROUP NAME " + musicGroup.name );
                audioMixers[ musicGroup.name ] = musicGroup;

            }

            foreach ( var slider in sliderList ) {

                string sliderName = slider.gameObject.name;
                musicSliders[ sliderName ] = slider;

                // Add Slider Callback
                slider.onValueChanged.AddListener( delegate { OnSliderValueChanged( sliderName ); } );

                float storedVolumeValue = PlayerPrefs.GetFloat( sliderName );

                SetSliderValue( sliderName, DecibelToPercentage( storedVolumeValue, -60f, 10f ) );

            }

        }

        private void SetSliderValue( string sliderName, float value ) {
            musicSliders[ sliderName ].value = value;
        }

        // min + (value * (max - min)) = Decibel (-80db - 20db)
        private float PercentageToDecibel( float value, float min, float max ) {
            return Mathf.Clamp( min + value * ( max - min ), min, max );
        }

        // (value - min) / (max - min) = Percentage (0 - 1)
        private float DecibelToPercentage( float value, float min, float max ) {
            return Mathf.Clamp( Mathf.Abs( value - min ) / Mathf.Abs( max - min ), 0f, 1f );
        }

        private void OnSliderValueChanged( string sliderName ) {

            float newVolumeValue = PercentageToDecibel( musicSliders[ sliderName ].value, -60f, 10f );

            audioMixers[ sliderName ].SetFloat( sliderName, newVolumeValue );

        }

        // When the scene changes, save the volume settings.
        private void OnDestroy( ) {

            SaveVolumeSettings( );

        }

        private void SaveVolumeSettings( ) {

            foreach ( AudioMixer mixer in audioMixerList ) {

                mixer.GetFloat( mixer.name, out float currentValue );
                PlayerPrefs.SetFloat( mixer.name, currentValue );

            }

        }

    }

}