using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace TeamBracket {
    public class StartGame : MonoBehaviour {

        [SerializeField] private PlayerController movementScript;

        [SerializeField] private GameObject crosshair;
        [SerializeField] private GameObject CountdownUI;
        [SerializeField] private TextMeshProUGUI countdownText;
        [SerializeField] private Animator animator;

        [SerializeField] private Stopwatch stopwatch;

        [SerializeField] private List< string > countdownSequence = new List< string >( );
        private int currentIndex = 0;

        private bool hasCountdownStarted;

        // Start is called before the first frame update
        void Start( ) {

            CountdownUI.SetActive( false );

        }

        void Countdown( string text ) {

            countdownText.text = text;

            countdownText.DOFade( 1f, 0.5f );

        }

        void ResetAnimator( ) {

            animator.SetBool( "IsPlaying", false );

        }
        private IEnumerator CountdownSequence( ) {

            crosshair.SetActive( false );
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
            crosshair.SetActive( true );

            Debug.Log( "Sequence Completed" );

            movementScript.enabled = true;

            // FIXME: Change this to take level requirements.
            stopwatch.SetStopwatchType( "Stopwatch" );
            stopwatch.SetStopwatchTime( 0f );
            stopwatch.ToggleStopwatch( true );

        }

        public void StartCountdown( ) {

            if ( hasCountdownStarted )
                return;


            StartCoroutine( CountdownSequence( ) );

        }

    }

}