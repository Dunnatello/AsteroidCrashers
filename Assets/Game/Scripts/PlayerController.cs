using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [Header( "Movement" )]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    [Header( "Jumping" )]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;

    [Header( "References" )]
    [SerializeField] private Rigidbody rb;

    private bool canJump;
    private bool grounded = true;

    void Start( ) {

        canJump = true;

    }

    void MovePlayer( ) {

        rb.velocity = new Vector3( currentSpeed, rb.velocity.y, 0 );

    }

    void Jump( ) {


        rb.AddForce( new Vector3( 0, jumpForce, 0 ), ForceMode.Impulse );

        Invoke( nameof( ResetJump ), jumpCooldown );

    }

    void ResetJump( ) {

        canJump = true;

    }

    // Update is called once per frame
    void Update( ) {

        float horizontalInput = Input.GetAxis( "Horizontal" );
        
        // Move Right
        if ( horizontalInput > 0 ) {

            currentSpeed += acceleration * Time.deltaTime;
    
        // Move Left
        } else if ( horizontalInput < 0 ) {

            currentSpeed -= acceleration * Time.deltaTime;

        // Decelerate
        } else {

            if ( currentSpeed == 0 )
                return;

            int decelerateDirection = ( currentSpeed > 0 ) ? 1 : -1;

            currentSpeed -= deceleration * decelerateDirection * Time.deltaTime;

            if ( Mathf.Abs( currentSpeed ) < 0.1f )
                currentSpeed = 0;

           
        }

        // Clamp Walk Speed
        if ( currentSpeed > walkSpeed )
            currentSpeed = walkSpeed;
        else if ( currentSpeed < -walkSpeed )
            currentSpeed = -walkSpeed;

        // FIXME: Add Grounded Check
        if ( Input.GetKeyDown( KeyCode.Space ) && canJump && grounded ) {
            
            canJump = false;
            Jump( );

        }
     
    }

    void FixedUpdate( ) {

        MovePlayer( );

    }//

}
