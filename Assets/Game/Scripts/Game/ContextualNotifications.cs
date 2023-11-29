/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Contextual Notifications
 * Script Objective: Shows/hides on screen text based on the current state of the game. Used to show "Reloading..." and "Respawning.." text to the player.
 * 
 */

using UnityEngine;

namespace TeamBracket {
    public class ContextualNotifications : MonoBehaviour {

        [SerializeField] private GameObject animatedTextContainer;
        [SerializeField] private GameObject respawnText;
        [SerializeField] private GameObject reloadText;

        void ToggleVisibility( bool isVisible ) {

            animatedTextContainer.SetActive( isVisible );

        }

        public void ToggleTextVisiblity( string textName, bool isVisible ) {

            if ( ( isVisible && !animatedTextContainer.activeInHierarchy ) || ( !isVisible && animatedTextContainer.activeInHierarchy ) ) {

                ToggleVisibility( isVisible );

            }

            switch ( textName ) {

                case "Respawn":

                    respawnText.SetActive( isVisible );
                    reloadText.SetActive( false );
                    break;

                case "Reload":
                    reloadText.SetActive( isVisible );
                    break;

            }

        }

        // Start is called before the first frame update
        void Start( ) {

            ToggleVisibility( false );

        }

    }

}
