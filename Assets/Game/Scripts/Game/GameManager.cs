/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Game Manager
 * Script Objective: Handles game conditions and transitions the player to the high score scene/menu when the game is over.
 * 
 */

using TeamBracket.HighScores;
using TeamBracket.WeaponSystem;
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

        [SerializeField] private Weapon weaponScript;
        [SerializeField] private Spawner spawnerScript;

        // Grayscale Settings
        [SerializeField] private Volume globalSettings;
        private ColorCurves grayScale;

        private int asteroidsDestroyed;
        private int totalAsteroidsSpawned;

        private int prevAsteroidsDestroyed;

        private float timeSinceLastScoreModification = 0f;
        private readonly TimeStringCreator timeStringCreator = new( );

        private const float ToleranceEpsilon = 0.0001f;

        private void Start( ) {

            foreach ( VolumeComponent component in globalSettings.profile.components ) {

                if ( component is ColorCurves ) {

                    grayScale = component as ColorCurves;

                }

            }

            asteroidsDestroyed = 0;
            totalAsteroidsSpawned = 0;

        }

        private void Update( ) {

            timeSinceLastScoreModification += Time.deltaTime;

/*  FIXME: Rework this:
            if ( asteroidsDestroyed > prevAsteroidsDestroyed + 1 ) {

                SceneManager.LoadScene( "Error" );

            }*/


            if ( asteroidsDestroyed > totalAsteroidsSpawned || asteroidsDestroyed > prevAsteroidsDestroyed + 3 ) {

                SceneManager.LoadScene( "Error" );

            }

        }

        public void GoToScene( string sceneName ) {

            ToggleGrayScale( false );
            Time.timeScale = 1.0f;

            SceneManager.LoadScene( sceneName );

        }

        public void AsteroidsSpawned( int asteroidsSpawned ) {

            totalAsteroidsSpawned += asteroidsSpawned;

        }

        private void UpdateUI( ) {

            scoreText.text = "<b>Hit</b>\n<mspace=25>" + asteroidsDestroyed + "</mspace>";

        }

        private void ToggleGrayScale( bool isActive ) {

            grayScale.active = isActive;

        }
        public void AddScore( ) {

            // Impossible condition detected, end game.
            if ( Mathf.Abs( asteroidsDestroyed - prevAsteroidsDestroyed ) > 2 ) {

                SceneManager.LoadScene( "Error" );

            }

            timeSinceLastScoreModification = 0f;

            asteroidsDestroyed += scoreIncrement;
            prevAsteroidsDestroyed = asteroidsDestroyed;

            UpdateUI( );

        }

        private bool ScoreValidation( float timeSurvived ) {

            bool isScoreValid = true;

            // If the number of destroyed asteroids is greater than the amount spawned, invalidate the score.
            if ( ( asteroidsDestroyed > 0 ) && ( asteroidsDestroyed > totalAsteroidsSpawned ) )
                isScoreValid = false;


            // If the time between asteroids destroyed is less than the weapon's actual fire delay, invalidate the score.
            if ( ( Mathf.Abs( timeSurvived / Mathf.Max( 1, asteroidsDestroyed ) ) - weaponScript.FireDelay ) < ToleranceEpsilon )
                isScoreValid = false;


            // If the total number of asteroids spawned exceeds the amount possible per interval, invalide the score.
            if ( ( ( totalAsteroidsSpawned / timeSurvived ) * spawnerScript.SpawnInterval ) > spawnerScript.MaxObjectsPerSpawn )
                isScoreValid = false;



            return isScoreValid;

        }

        public void GameOver( ) {

            ToggleGrayScale( true );

            Debug.Log( "GAME OVER" );
            stopwatch.ToggleStopwatch( false );

            float timeSurvived = stopwatch.GetStopwatchTime( );

            bool isScoreValid = ScoreValidation( timeSurvived );

            if ( !isScoreValid ) {

                SceneManager.LoadScene( "Error" );
                return;
            }

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