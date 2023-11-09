/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Game Manager
 * Script Objective: Handles game conditions and transitions the player to the high score scene/menu when the game is over.
 * 
 */

using TeamBracket.HighScores;
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
        [SerializeField] private HighScoreManager highScoreManager;

        [SerializeField] private TextMeshProUGUI finalScoreText;

        // Grayscale Settings
        [SerializeField] private Volume globalSettings;
        private ColorCurves grayScale;

        private int asteroidsDestroyed;

        private readonly TimeStringCreator timeStringCreator = new( );
        
        private void Start( ) {

            foreach ( VolumeComponent component in globalSettings.profile.components ) {

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

        private void UpdateUI( ) {

            scoreText.text = "<b>Hit</b>\n<mspace=25>" + asteroidsDestroyed + "</mspace>";
        }

        private void ToggleGrayScale( bool isActive ) {

            grayScale.active = isActive;

        }
        public void AddScore( ) {

            asteroidsDestroyed += scoreIncrement;
            UpdateUI( );

        }

        public void GameOver( ) {

            ToggleGrayScale( true );

            Debug.Log( "GAME OVER" );
            stopwatch.ToggleStopwatch( false );

            float timeSurvived = stopwatch.GetStopwatchTime( );

            // Add Score to List
            highScoreManager.AddScore( asteroidsDestroyed, timeSurvived );

            // Get Overall Score
            HighScore currentScore = highScoreManager.GetScore( asteroidsDestroyed, timeSurvived );

            // Display Final Score

            finalScoreText.text = string.Format( "<b>Final Score:</b>\n<size=60>{0:0.00}</size>\n\n<b>Asteroids Destroyed:</b>\n{1}\n<b>Time Survived:</b>\n{2}",
                                                 currentScore.overallScore, currentScore.asteroidsDestroyed, timeStringCreator.GetNewTimeString( currentScore.timeSurvived ) );
            // Show Game Over Screen
            gameOverScreen.SetActive( true );

            // Slow Down Time
            Time.timeScale = 0.4f;



        }

        private void OnApplicationQuit( ) {

            ToggleGrayScale( false );

        }

    }

}