using System.Collections;
using System.Collections.Generic;
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

            if ( isAsteroid )
                if ( collision.collider.CompareTag( "Asteroid" ) || collision.collider.CompareTag( "Missile" ) )
                    return;

            if ( hasHitTarget )
                return;

            hasHitTarget = true;

            if ( collision.collider.CompareTag( "Asteroid" ) ) {

                Debug.Log( "HIT ASTEROID" );
                gameManager.AddScore( );
                Destroy( collision.gameObject );

            }
            else if ( collision.collider.CompareTag( "Player" ) ) {

                if ( collision.gameObject.TryGetComponent<PlayerHealth>( out var playerHealth ) ) {

                    playerHealth.PlayerDied( false );

                }

            }

            Explode( );


        }

        private void Start( ) {

            isAsteroid = this.gameObject.CompareTag( "Asteroid" );

            
            if ( SceneManager.GetActiveScene( ).name != "Menu" )
                gameManager = GameObject.Find( "GameManager" ).GetComponent< GameManager >( );
            
            spriteRenderer = GetComponent< SpriteRenderer >( );
            explosionSound = GetComponent< AudioSource >( );
            rb = GetComponent< Rigidbody >( );

            rb.sleepThreshold = 0;

        }

    }

}
