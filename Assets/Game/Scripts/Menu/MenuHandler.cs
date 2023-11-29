/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Menu Handler
 * Script Objective: Handles common methods between scenes.
 * 
 * Source(s): https://stackoverflow.com/questions/56145437/how-to-make-textmesh-pro-input-field-deselect-on-enter-key
 * 
 */

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace TeamBracket.UI {
    
    public class MenuHandler : MonoBehaviour {

        public void GoToScene( string sceneName ) {

            SceneManager.LoadScene( sceneName );

        }

        public void QuitGame( ) {

            Application.Quit( );

        }

        public void DeselectButton( ) {

            var eventSystem = EventSystem.current;
            if ( eventSystem.alreadySelecting )
                eventSystem.SetSelectedGameObject( null );

        }

    }

}