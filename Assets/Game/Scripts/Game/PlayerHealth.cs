/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Player Health
 * Script Objective: Handle the player lives system by respawning the player if they die as well as updating the UI of the player's life count.
 * 
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeamBracket {
    public class PlayerHealth : MonoBehaviour {
        // Start is called before the first frame update

        [Header( "Player Settings" )]
        [SerializeField] private int totalLives = 3;
        private int livesLeft;

        [SerializeField] private float playerInvincibilityTime;
        private float invincibilityTimeLeft;

        [SerializeField] private Vector3 respawnPoint = Vector3.zero;
        [SerializeField] private float respawnTime = 3f;

        [Header( "References" )]
        [SerializeField] private List<GameObject> UILifeCounter = new( );
        [SerializeField] private Transform player;
        private Rigidbody playerRb;
        private Animator playerAnimator;

        [SerializeField] private AudioSource playerDeathSound;

        [SerializeField] private GameManager gameManager;

        [SerializeField] private ContextualNotifications notificationScript;

        private void EndImmortality( ) {

            playerAnimator.SetBool( "IsFading", false );

        }

        public void RespawnPlayer( ) {

            player.gameObject.SetActive( true );

            // Add Immortality
            playerAnimator.SetBool( "IsFading", true );
            Invoke( nameof( EndImmortality ), playerInvincibilityTime );

            invincibilityTimeLeft = playerInvincibilityTime;

            // Respawn Player
            player.transform.position = respawnPoint;
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;

            if ( notificationScript != null )
                notificationScript.ToggleTextVisiblity( "Respawn", false );


        }
        public void PlayerDied( bool playerFell ) {

            if ( !playerFell && invincibilityTimeLeft > 0 ) // Player is currently immortal, no need to subtract lives.
                return;

            player.gameObject.SetActive( false );
            playerDeathSound.Play( );

            // Make sure that the current scene is not the main menu scene. That scene features an AI agent, but it has infinite lives.
            if ( SceneManager.GetActiveScene( ).name != "Menu" ) {

                UpdateLifeCountUI( );

                livesLeft--;

                if ( livesLeft < 0 ) { // No lives remaining, send player to the game over screen.

                    gameManager.GameOver( );
                    return;

                }

                // Show "Respawning..." Text on UI
                if ( notificationScript != null )
                    notificationScript.ToggleTextVisiblity( "Respawn", true );

            }

            Invoke( nameof( RespawnPlayer ), respawnTime );

        }

        public void UpdateLifeCountUI( ) {

            if ( livesLeft - 1 < 0 )
                return;

            UILifeCounter.ElementAt( livesLeft - 1 ).SetActive( false );


        }

        private void Start( ) {

            livesLeft = totalLives;

            playerRb = player.gameObject.GetComponent<Rigidbody>( );
            playerAnimator = player.gameObject.GetComponent<Animator>( );

        }

        // Update is called once per frame
        private void Update( ) {

            if ( invincibilityTimeLeft > 0 ) {

                invincibilityTimeLeft -= Time.deltaTime;

            }

        }
    }

}