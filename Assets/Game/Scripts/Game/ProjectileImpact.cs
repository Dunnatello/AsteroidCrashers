/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Projectile Impact
 * Script Objective: Handles projectile interactions and special effects. Asteroids and rockets both use this script to handle collisions.
 * 
 */

using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeamBracket {
    public class ProjectileImpact : MonoBehaviour {

        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private GameObject specialEffects;
        private GameManager gameManager;

        private AudioSource explosionSound;
        private SpriteRenderer spriteRenderer;

        private bool hasHitTarget = false;
        private bool isAsteroid;

        private Rigidbody rb;

        private void DestroyObject( ) {

            Destroy( this.gameObject );

        }
        private void Explode( ) {

            explosionSound.pitch = Random.Range( 0.9f, 1.3f );
            explosionSound.Play( );

            // Hide Missle
            spriteRenderer.enabled = false;
            specialEffects.SetActive( false );

            // Create Explosion Effect
            GameObject newExplosion = Instantiate( explosionPrefab );
            newExplosion.transform.position = transform.position;

            // Schedule Destruction of Projectile After Explosion Plays
            Invoke( nameof( DestroyObject ), 4f );

        }

        private void OnCollisionEnter( Collision collision ) {

            // Current Object is Asteroid. If the other object is an asteroid or a missile, early return here.
            if ( isAsteroid )
                if ( collision.collider.CompareTag( "Asteroid" ) || collision.collider.CompareTag( "Missile" ) )
                    return;

            // If this collision has already been handled, early return.
            if ( hasHitTarget )
                return;

            hasHitTarget = true;

            // Hit Asteroid
            if ( collision.collider.CompareTag( "Asteroid" ) ) {

                gameManager.AddScore( );
                Destroy( collision.gameObject );

            }
            // Hit Player
            else if ( collision.collider.CompareTag( "Player" ) ) {

                if ( collision.gameObject.TryGetComponent<PlayerHealth>( out PlayerHealth playerHealth ) ) {

                    playerHealth.PlayerDied( false );

                }

            }

            // Explode Asteroid/Missile
            Explode( );


        }

        private void Start( ) {

            isAsteroid = this.gameObject.CompareTag( "Asteroid" );


            if ( SceneManager.GetActiveScene( ).name != "Menu" )
                gameManager = GameObject.Find( "GameManager" ).GetComponent<GameManager>( );

            spriteRenderer = GetComponent<SpriteRenderer>( );
            explosionSound = GetComponent<AudioSource>( );
            rb = GetComponent<Rigidbody>( );

            rb.sleepThreshold = 0;

        }

    }

}
