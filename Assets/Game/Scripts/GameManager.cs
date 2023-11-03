using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace TeamBracket {

    public class GameManager : MonoBehaviour {
        private int playerScore;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private int scoreIncrement;

        [Header( "References" )]
        [SerializeField] private Stopwatch stopwatch;

        [SerializeField] private GameObject gameOverScreen;


        // Grayscale Settings
        [SerializeField] private Volume globalSettings;
        private ColorCurves grayScale;


        private bool isGameComplete = false;

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