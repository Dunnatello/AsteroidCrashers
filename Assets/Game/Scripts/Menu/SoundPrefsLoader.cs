/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Sound Prefs Loader
 * Script Objective: Allows the game to save and load audio settings.
 * 
 * Source(s): https://stackoverflow.com/questions/68902203/dont-destroy-on-load-creates-multiple-objects
 *            https://discussions.unity.com/t/how-do-i-use-audiomixer-getfloat/141433
 * 
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace TeamBracket.Audio {
    public class SoundPrefsLoader : MonoBehaviour {

        [SerializeField] private List<AudioMixer> audioMixers = new( );

        public static SoundPrefsLoader instance;

        void Start( ) {

            // Load volume settings once the game starts.
            LoadVolumeSettings( );

        }

        private void Awake( ) {

            // Tell Unity to not destroy this object as we'll need it to save the volume settings once the application quits.
            if ( instance == null ) {

                instance = this;
                DontDestroyOnLoad( this.gameObject );

            }
            else {

                Destroy( this.gameObject );

            }

        }
        
        // When the game closes, save the settings in case they haven't been saved earlier.
        private void OnApplicationQuit( ) {

            SaveVolumeSettings( );

        }

        private void LoadVolumeSettings( ) {

            foreach ( AudioMixer mixer in audioMixers ) {

                // If no value is found, it defaults to 0 db.
                float savedValue = PlayerPrefs.GetFloat( mixer.name );
                mixer.SetFloat( mixer.name, savedValue );

            }

        }

        private void SaveVolumeSettings( ) {

            foreach ( AudioMixer mixer in audioMixers ) {

                mixer.GetFloat( mixer.name, out float currentValue );
                PlayerPrefs.SetFloat( mixer.name, currentValue );

            }

        }

    }

}