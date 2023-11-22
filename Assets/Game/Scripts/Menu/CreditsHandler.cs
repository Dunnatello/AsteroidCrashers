/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Credits Handler
 * Script Objective: Shows all game credits and then returns to the main menu.
 *  
 */

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace TeamBracket.Credits {

    [System.Serializable]
    public class Credit {

        public string title;
        public string source;

    }

    public class CreditsHandler : MonoBehaviour {

        [SerializeField] private Credit[ ] creditsList;
        [SerializeField] private float fadeDuration = 1f;
        [SerializeField] private float displayTextDuration = 1.5f;

        private bool completed = false;
        private int currentIndex = 0;

        private float m_Timer;


        public TextMeshProUGUI titleText;
        public TextMeshProUGUI sourceText;

        private void GoToMenu( ) {

            SceneManager.LoadScene( "Menu" );

        }

        private void SetText( Credit currentElement ) {

            titleText.text = currentElement.title;
            sourceText.text = currentElement.source;

        }

        // Start is called before the first frame update
        private void Start( ) {

            SetText( creditsList[ currentIndex ] );

        }

        // Update is called once per frame
        private void Update( ) {

            if ( Input.GetKeyDown( KeyCode.Escape ) || Input.GetKeyDown( KeyCode.Space ) ) {

                if ( !completed ) {

                    completed = true;
                    GoToMenu( );
                    return;

                }

            }

            m_Timer += Time.deltaTime;

            if ( completed )
                return;

            // Fade Text
            titleText.alpha = m_Timer / fadeDuration;
            sourceText.alpha = m_Timer / fadeDuration;

            if ( m_Timer < fadeDuration + displayTextDuration ) {
                return;
            }

            // Reset Credit Display
            titleText.alpha = 0;
            sourceText.alpha = 0;
            m_Timer = 0;

            // Credits Finished. Go to Main Menu
            if ( currentIndex >= creditsList.Length - 1 ) {

                completed = true;
                titleText.alpha = 1;
                sourceText.alpha = 1;

                GoToMenu( );
                return;

            }

            // Credits Not Finished. Go to Next Credit
            currentIndex++;
            SetText( creditsList[ currentIndex ] );

        }

    }

}