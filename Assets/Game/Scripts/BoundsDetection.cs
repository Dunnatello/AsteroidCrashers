/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Bounds Detection
 * Script Objective: Destroy out-of-bounds objects as well as prevent the player from falling forever if they fall off the platform.
 * 
 */

using UnityEngine;

namespace TeamBracket {
    public class BoundsDetection : MonoBehaviour {

        [SerializeField] private PlayerHealth healthScript;

        private void OnTriggerEnter( Collider other ) {

            if ( other.gameObject.CompareTag( "Player" ) ) {

                Debug.Log( "CAUGHT PLAYER" );
                healthScript.PlayerDied( true );

            }
            else {

                Destroy( other.gameObject );

            }

        }

    }

}