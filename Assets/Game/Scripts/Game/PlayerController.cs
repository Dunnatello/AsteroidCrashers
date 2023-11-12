/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Player Controller
 * Script Objective: Allows for player movement in X/Y directions.
 * 
 */

using UnityEngine;
namespace TeamBracket.Movement {
    public class PlayerController : MonoBehaviour {
        // Start is called before the first frame update

        [Header( "Movement" )]
        [SerializeField] private float walkSpeed;
        [SerializeField] private float currentSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float deceleration;

        [SerializeField] private LayerMask groundLayers;
        [SerializeField] private float playerHeight;

        [Header( "Jumping" )]
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpCooldown;

        [Header( "References" )]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Animator anim;
        [SerializeField] private SpriteRenderer character;

        private bool canJump;
        [SerializeField] private bool grounded;

        private int moveDirection;

        private void Start( ) {

            canJump = true;

        }

        private void MovePlayer( ) {

            rb.velocity = new Vector3( currentSpeed, rb.velocity.y, 0 );

        }

        private void Jump( ) {

            anim.SetTrigger( "Jump" );
            rb.AddForce( new Vector3( 0, jumpForce, 0 ), ForceMode.Impulse );

            Invoke( nameof( ResetJump ), jumpCooldown );

        }

        private void ResetJump( ) {

            canJump = true;

        }

        private void UpdateAnimationState( ) {

            bool isMoving = moveDirection != 0;
            anim.SetInteger( "MoveDirection", moveDirection );
            anim.SetBool( "IsMoving", isMoving );
            anim.SetBool( "Grounded", grounded );

            character.flipX = moveDirection == 1;

        }

        private void GetUserInput( ) {

            float horizontalInput = Input.GetAxis( "Horizontal" );

            moveDirection = 0;

            grounded = Physics.Raycast( transform.position, Vector3.down, ( playerHeight * 0.5f ) + 0.2f, groundLayers );

            // Move Right
            if ( horizontalInput > 0 ) {

                currentSpeed += acceleration * Time.deltaTime;
                moveDirection = 1;

            }
            // Move Left
            else if ( horizontalInput < 0 ) {

                currentSpeed -= acceleration * Time.deltaTime;
                moveDirection = -1;

            }
            // Decelerate
            else if ( currentSpeed != 0 ) {

                int decelerateDirection = ( currentSpeed > 0 ) ? 1 : -1;

                currentSpeed -= deceleration * decelerateDirection * Time.deltaTime;

                // Stop Player Movement
                if ( Mathf.Abs( currentSpeed ) < 0.1f )
                    currentSpeed = 0;


            }

            // Player Presses Jump Key
            if ( Input.GetKeyDown( KeyCode.Space ) && canJump && grounded ) {

                canJump = false;
                Jump( );

            }

        }

        private void ClampSpeed( ) {

            // Clamp Walk Speed
            if ( currentSpeed > walkSpeed )
                currentSpeed = walkSpeed;
            else if ( currentSpeed < -walkSpeed )
                currentSpeed = -walkSpeed;

        }
        // Update is called once per frame
        private void Update( ) {

            GetUserInput( );
            ClampSpeed( );
            UpdateAnimationState( );

        }

        private void FixedUpdate( ) {

            MovePlayer( );

        }

    }

}