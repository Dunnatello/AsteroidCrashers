/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Stopwatch
 * Script Objective: Keeps track of the player's time spent alive.
 * 
 */
using TMPro;
using UnityEngine;

namespace TeamBracket {
    public class Stopwatch : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI stopwatchText;

        private float currentTime = 0f;

        private bool isPlaying;
        private readonly TimeStringCreator timeStringCreator = new( );


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



        private void UpdateUI( ) {

            stopwatchText.text = "<mspace=mspace=25>" + timeStringCreator.GetNewTimeString( currentTime ) + "</mspace>";

        }

        // Update is called once per frame
        private void Update( ) {

            if ( !isPlaying )
                return;


            currentTime += Time.deltaTime * direction;

        }

        private void FixedUpdate( ) {

            UpdateUI( );

        }
    }

}