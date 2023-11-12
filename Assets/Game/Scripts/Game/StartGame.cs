/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Start Game
 * Script Objective: Starts the game and handles the countdown sequence.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using TeamBracket.Movement;
using TMPro;
using UnityEngine;

namespace TeamBracket {
    public class StartGame : MonoBehaviour {

        [SerializeField] private PlayerController movementScript;

        [SerializeField] private GameObject CountdownUI;
        [SerializeField] private TextMeshProUGUI countdownText;
        [SerializeField] private Animator animator;

        [SerializeField] private Stopwatch stopwatch;

        [SerializeField] private List<string> countdownSequence = new( );

        [SerializeField] private GameObject asteroidSpawner;

        private int currentIndex = 0;

        private bool hasCountdownStarted;

        // Start is called before the first frame update
        private void Start( ) {

            CountdownUI.SetActive( false );

            StartCountdown( );
            movementScript.enabled = false;

        }

        private IEnumerator CountdownSequence( ) {

            yield return new WaitForSeconds( 0.25f );

            CountdownUI.SetActive( true );

            while ( currentIndex < countdownSequence.Count ) {

                countdownText.text = "<mspace=mspace=35>" + countdownSequence[ currentIndex ] + "</mspace>";
                animator.SetBool( "IsPlaying", true );

                currentIndex++;
                yield return new WaitForSeconds( 0.75f );
                animator.SetBool( "IsPlaying", false );

            }

            CountdownUI.SetActive( false );

            movementScript.enabled = true;

            stopwatch.SetStopwatchType( "Stopwatch" );
            stopwatch.SetStopwatchTime( 0f );
            stopwatch.ToggleStopwatch( true );

            asteroidSpawner.SetActive( true );

        }

        public void StartCountdown( ) {

            if ( hasCountdownStarted )
                return;

            hasCountdownStarted = true;

            _ = StartCoroutine( CountdownSequence( ) );

        }

    }

}