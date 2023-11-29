/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: In-Game Menu Handler
 * Script Objective: Toggles the visibility of the in-game menu.
 * 
 */

using UnityEngine;

namespace TeamBracket.UI {

    public class InGameMenuHandler : MonoBehaviour {

        [SerializeField] private GameObject menu;

        private void Start( ) {

            menu.SetActive( false );

        }

        private void Update( ) {

            if ( Input.GetKeyDown( KeyCode.Escape ) ) {

                menu.SetActive( !menu.activeInHierarchy );

            }

        }

    }

}
