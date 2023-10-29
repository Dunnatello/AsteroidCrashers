using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TeamBracket {
    public class Stopwatch : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI stopwatchText;

        private float currentTime = 0f;

        private bool isPlaying;

        private readonly float sixtyDivisor = 1f / 60f;

        private int direction = 1;

        public void ToggleStopwatch( bool newState ) {

            isPlaying = newState;

        }

        public void SetStopwatchType( string stopwatchType ) {

            direction = stopwatchType == "Stopwatch" ? 1 : -1;


        }

        public void SetStopwatchTime( float newTime ) {

            currentTime = newTime;

        }

        public float GetStopwatchTime( ) {

            return currentTime;

        }

        void UpdateUI( ) {

            int hours = Mathf.FloorToInt( currentTime * sixtyDivisor * sixtyDivisor );
            int minutes = Mathf.FloorToInt( currentTime * sixtyDivisor );
            int seconds = Mathf.FloorToInt( currentTime % 60 );
            int milliseconds = Mathf.FloorToInt( ( currentTime * 100f ) % 100 );

            string newTimeString = string.Format( "<mspace=mspace=25>{0:00}:{1:00}:{2:00}.{3:00}</mspace>", hours, minutes, seconds, milliseconds % 100 );

            stopwatchText.text = newTimeString;

        }

        // Update is called once per frame
        void Update( ) {

            if ( !isPlaying )
                return;


            currentTime += Time.deltaTime * direction;

        }

        private void FixedUpdate( ) {

            UpdateUI( );

        }
    }

}