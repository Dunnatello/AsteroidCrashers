/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Game Manager
 * Script Objective: Handles game conditions and transitions the player to the high score scene/menu when the game is over.
 * 
 */

using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace TeamBracket {

    public class GameManager : MonoBehaviour {


        [Header( "Settings" )]
        [SerializeField] private int scoreIncrement;

        [Header( "References" )]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Stopwatch stopwatch;
        [SerializeField] private GameObject gameOverScreen;

        // Grayscale Settings
        [SerializeField] private Volume globalSettings;
        private ColorCurves grayScale;

        private int playerScore;
        private bool isGameComplete = false; // FIXME: Utilize this to end coroutines in Spawner.

        private void Start( ) {

            foreach ( var component in globalSettings.profile.components ) {

                if ( component is ColorCurves ) {

                    grayScale = component as ColorCurves;

                }

            }

        }
        public void GoToScene( string sceneName ) {

            ToggleGrayScale( false );
            Time.timeScale = 1.0f;

            SceneManager.LoadScene( sceneName );

        }

        void UpdateUI( ) {

            scoreText.text = "<b>Score</b>\n<mspace=25>" + playerScore + "</mspace>";
        }

        void ToggleGrayScale( bool isActive ) {

            grayScale.active = isActive;

        }
        public void AddScore( ) {

            playerScore += scoreIncrement;
            UpdateUI( );

        }

        public void GameOver( ) {

            ToggleGrayScale( true );

            Debug.Log( "GAME OVER" );
            stopwatch.ToggleStopwatch( false );
            gameOverScreen.SetActive( true );

            Time.timeScale = 0.4f;

        }

        private void OnApplicationQuit( ) {

            ToggleGrayScale( false );

        }

    }

}