﻿//--------------------------------------------------------------------------------------------------------------------------------
// Cartoon FX
// (c) 2012-2020 Jean Moreno
//--------------------------------------------------------------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

// GUI Styles and UI methods

namespace CartoonFX {
    public static class Styles {
        //================================================================================================================================
        // GUI Styles
        //================================================================================================================================

        //================================================================================================================================
        // (x) close button
        private static GUIStyle _closeCrossButton;
        public static GUIStyle CloseCrossButton {
            get {
                if ( _closeCrossButton == null ) {
                    //Try to load GUISkin according to its GUID
                    //Assumes that its .meta file should always stick with it!
                    string guiSkinPath = AssetDatabase.GUIDToAssetPath( "02d396fa782e5d7438e231ea9f8be23c" );
                    GUISkin gs = AssetDatabase.LoadAssetAtPath<GUISkin>( guiSkinPath );
                    if ( gs != null ) {
                        _closeCrossButton = System.Array.Find<GUIStyle>( gs.customStyles, x => x.name == "CloseCrossButton" );
                    }

                    //Else fall back to minibutton
                    _closeCrossButton ??= EditorStyles.miniButton;
                }
                return _closeCrossButton;
            }
        }

        //================================================================================================================================
        // Shuriken Toggle with label alignment fix
        private static GUIStyle _shurikenToggle;
        public static GUIStyle ShurikenToggle {
            get {
                if ( _shurikenToggle == null ) {
                    _shurikenToggle = new GUIStyle( "ShurikenToggle" ) {
                        fontSize = 9,
                        contentOffset = new Vector2( 16, -1 )
                    };
                    if ( EditorGUIUtility.isProSkin ) {
                        Color textColor = new( .8f, .8f, .8f );
                        _shurikenToggle.normal.textColor = textColor;
                        _shurikenToggle.active.textColor = textColor;
                        _shurikenToggle.focused.textColor = textColor;
                        _shurikenToggle.hover.textColor = textColor;
                        _shurikenToggle.onNormal.textColor = textColor;
                        _shurikenToggle.onActive.textColor = textColor;
                        _shurikenToggle.onFocused.textColor = textColor;
                        _shurikenToggle.onHover.textColor = textColor;
                    }
                }
                return _shurikenToggle;
            }
        }

        //================================================================================================================================
        // Bold mini-label (the one from EditorStyles isn't actually "mini")
        private static GUIStyle _miniBoldLabel;
        public static GUIStyle MiniBoldLabel {
            get {
                _miniBoldLabel ??= new GUIStyle( EditorStyles.boldLabel ) {
                        fontSize = 10,
                        margin = new RectOffset( 0, 0, 0, 0 )
                    };
                return _miniBoldLabel;
            }
        }

        //================================================================================================================================
        // Bold mini-foldout
        private static GUIStyle _miniBoldFoldout;
        public static GUIStyle MiniBoldFoldout {
            get {
                _miniBoldFoldout ??= new GUIStyle( EditorStyles.foldout ) {
                        fontSize = 10,
                        fontStyle = FontStyle.Bold,
                        margin = new RectOffset( 0, 0, 0, 0 )
                    };
                return _miniBoldFoldout;
            }
        }

        //================================================================================================================================
        // Gray right-aligned label for Orderable List (Material Animator)
        private static GUIStyle _PropertyTypeLabel;
        public static GUIStyle PropertyTypeLabel {
            get {
                if ( _PropertyTypeLabel == null ) {
                    _PropertyTypeLabel = new GUIStyle( EditorStyles.label ) {
                        alignment = TextAnchor.MiddleRight
                    };
                    _PropertyTypeLabel.normal.textColor = Color.gray;
                    _PropertyTypeLabel.fontSize = 9;
                }
                return _PropertyTypeLabel;
            }
        }

        // Dark Gray right-aligned label for Orderable List (Material Animator)
        private static GUIStyle _PropertyTypeLabelFocused;
        public static GUIStyle PropertyTypeLabelFocused {
            get {
                if ( _PropertyTypeLabelFocused == null ) {
                    _PropertyTypeLabelFocused = new GUIStyle( EditorStyles.label ) {
                        alignment = TextAnchor.MiddleRight
                    };
                    _PropertyTypeLabelFocused.normal.textColor = new Color( .2f, .2f, .2f );
                    _PropertyTypeLabelFocused.fontSize = 9;
                }
                return _PropertyTypeLabelFocused;
            }
        }

        //================================================================================================================================
        // Rounded Box
        private static GUIStyle _roundedBox;
        public static GUIStyle RoundedBox {
            get {
                _roundedBox ??= new GUIStyle( EditorStyles.helpBox );
                return _roundedBox;
            }
        }

        //================================================================================================================================
        // Center White Label ("Editing Spline" label in Scene View)
        private static GUIStyle _CenteredWhiteLabel;
        public static GUIStyle CenteredWhiteLabel {
            get {
                if ( _CenteredWhiteLabel == null ) {
                    _CenteredWhiteLabel = new GUIStyle( EditorStyles.centeredGreyMiniLabel ) {
                        fontSize = 20
                    };
                    _CenteredWhiteLabel.normal.textColor = Color.white;
                }
                return _CenteredWhiteLabel;
            }
        }

        //================================================================================================================================
        // Used to draw lines for separators
        static public GUIStyle _LineStyle;
        static public GUIStyle LineStyle {
            get {
                if ( _LineStyle == null ) {
                    _LineStyle = new GUIStyle( );
                    _LineStyle.normal.background = EditorGUIUtility.whiteTexture;
                    _LineStyle.stretchWidth = true;
                }

                return _LineStyle;
            }
        }

        //================================================================================================================================
        // HelpBox with rich text formatting support
        private static GUIStyle _HelpBoxRichTextStyle;
        static public GUIStyle HelpBoxRichTextStyle {
            get {
                _HelpBoxRichTextStyle ??= new GUIStyle( "HelpBox" ) {
                        richText = true
                    };
                return _HelpBoxRichTextStyle;
            }
        }

        //================================================================================================================================
        // Material Blue Header
        static public GUIStyle _MaterialHeaderStyle;
        static public GUIStyle MaterialHeaderStyle {
            get {
                if ( _MaterialHeaderStyle == null ) {
                    _MaterialHeaderStyle = new GUIStyle( EditorStyles.label ) {
                        fontStyle = FontStyle.Bold,
                        fontSize = 11
                    };
                    _MaterialHeaderStyle.padding.top = 0;
                    _MaterialHeaderStyle.padding.bottom = 0;
                    _MaterialHeaderStyle.normal.textColor = EditorGUIUtility.isProSkin ? new Color32( 75, 128, 255, 255 ) : new Color32( 0, 50, 230, 255 );
                    _MaterialHeaderStyle.stretchWidth = true;
                }

                return _MaterialHeaderStyle;
            }
        }

        //================================================================================================================================
        // Material Header emboss effect
        static public GUIStyle _MaterialHeaderStyleHighlight;
        static public GUIStyle MaterialHeaderStyleHighlight {
            get {
                if ( _MaterialHeaderStyleHighlight == null ) {
                    _MaterialHeaderStyleHighlight = new GUIStyle( MaterialHeaderStyle ) {
                        contentOffset = new Vector2( 1, 1 )
                    };
                    _MaterialHeaderStyleHighlight.normal.textColor = EditorGUIUtility.isProSkin ? new Color32( 255, 255, 255, 16 ) : new Color32( 255, 255, 255, 32 );
                }

                return _MaterialHeaderStyleHighlight;
            }
        }

        //================================================================================================================================
        // Filled rectangle

        static private GUIStyle _WhiteRectangleStyle;

        static public void DrawRectangle( Rect position, Color color ) {
            Color col = GUI.color;
            GUI.color *= color;
            DrawRectangle( position );
            GUI.color = col;
        }
        static public void DrawRectangle( Rect position ) {
            if ( _WhiteRectangleStyle == null ) {
                _WhiteRectangleStyle = new GUIStyle( );
                _WhiteRectangleStyle.normal.background = EditorGUIUtility.whiteTexture;
            }

            if ( Event.current != null && Event.current.type == EventType.Repaint ) {
                _WhiteRectangleStyle.Draw( position, false, false, false, false );
            }
        }

        //================================================================================================================================
        // Methods
        //================================================================================================================================

        static public void DrawLine( float height = 2f ) {
            DrawLine( Color.black, height );
        }
        static public void DrawLine( Color color, float height = 1f ) {
            Rect position = GUILayoutUtility.GetRect( 0f, float.MaxValue, height, height, LineStyle );
            DrawLine( position, color );
        }
        static public void DrawLine( Rect position, Color color ) {
            if ( Event.current.type == EventType.Repaint ) {
                Color orgColor = GUI.color;
                GUI.color = orgColor * color;
                LineStyle.Draw( position, false, false, false, false );
                GUI.color = orgColor;
            }
        }

        static public void MaterialDrawHeader( GUIContent guiContent ) {
            Rect rect = GUILayoutUtility.GetRect( guiContent, MaterialHeaderStyle );
            GUI.Label( rect, guiContent, MaterialHeaderStyleHighlight );
            GUI.Label( rect, guiContent, MaterialHeaderStyle );
        }

        static public void MaterialDrawSeparator( ) {
            GUILayout.Space( 4 );
            if ( EditorGUIUtility.isProSkin )
                DrawLine( new Color( .3f, .3f, .3f, 1f ), 1 );
            else
                DrawLine( new Color( .6f, .6f, .6f, 1f ), 1 );
            GUILayout.Space( 4 );
        }

        static public void MaterialDrawSeparatorDouble( ) {
            GUILayout.Space( 6 );
            if ( EditorGUIUtility.isProSkin ) {
                DrawLine( new Color( .1f, .1f, .1f, 1f ), 1 );
                DrawLine( new Color( .4f, .4f, .4f, 1f ), 1 );
            }
            else {
                DrawLine( new Color( .3f, .3f, .3f, 1f ), 1 );
                DrawLine( new Color( .9f, .9f, .9f, 1f ), 1 );
            }
            GUILayout.Space( 6 );
        }

        //built-in console icons, also used in help box
        private static Texture2D warnIcon;
        private static Texture2D infoIcon;
        private static Texture2D errorIcon;

        static public void HelpBoxRichText( Rect position, string message, MessageType msgType ) {
            Texture2D icon = null;
            switch ( msgType ) {
                case MessageType.Warning: icon = warnIcon != null ? warnIcon : ( warnIcon = EditorGUIUtility.Load( "console.warnicon" ) as Texture2D ); break;
                case MessageType.Info: icon = infoIcon != null ? infoIcon : ( infoIcon = EditorGUIUtility.Load( "console.infoicon" ) as Texture2D ); break;
                case MessageType.Error: icon = errorIcon != null ? errorIcon : ( errorIcon = EditorGUIUtility.Load( "console.erroricon" ) as Texture2D ); break;
            }
            EditorGUI.LabelField( position, GUIContent.none, new GUIContent( message, icon ), HelpBoxRichTextStyle );
        }

        static public void HelpBoxRichText( string message, MessageType msgType ) {
            Texture2D icon = null;
            switch ( msgType ) {
                case MessageType.Warning: icon = warnIcon != null ? warnIcon : ( warnIcon = EditorGUIUtility.Load( "console.warnicon" ) as Texture2D ); break;
                case MessageType.Info: icon = infoIcon != null ? infoIcon : ( infoIcon = EditorGUIUtility.Load( "console.infoicon" ) as Texture2D ); break;
                case MessageType.Error: icon = errorIcon != null ? errorIcon : ( errorIcon = EditorGUIUtility.Load( "console.erroricon" ) as Texture2D ); break;
            }
            EditorGUILayout.LabelField( GUIContent.none, new GUIContent( message, icon ), HelpBoxRichTextStyle );
        }
    }
}
