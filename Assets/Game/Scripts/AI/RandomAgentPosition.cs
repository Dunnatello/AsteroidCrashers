using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class RandomAgentPosition : MonoBehaviour
{


    private NavMeshAgent agent;
    private bool lookingForDestination = false;

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer character;


    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent< NavMeshAgent >( );

        agent.updateRotation = false;

        Invoke( nameof( GoToRandomPosition ), 0.5f );

    }

    void GoToRandomPosition( ) {

        Vector3 newPosition = new( Random.Range( -15, 15 ), 1.5f, 0f );
        agent.SetDestination( newPosition );

        lookingForDestination = true;

    }
    void AnimateMovement( ) {

        int moveDirection = ( agent.velocity.x > 0 ) ? 1 : -1;

        if ( Mathf.Abs( agent.velocity.x ) < 0.1f )
            moveDirection = 0;

        bool isMoving = moveDirection != 0;
        animator.SetInteger( "MoveDirection", moveDirection );
        animator.SetBool( "IsMoving", isMoving );
        //animator.SetBool( "Grounded", grounded );

        character.flipX = ( moveDirection == 1 ) ? true : false;

    }
    // Update is called once per frame
    void Update()
    {

        AnimateMovement( );

        if ( lookingForDestination && agent.remainingDistance < agent.stoppingDistance ) {

            lookingForDestination = false;
            Invoke( nameof( GoToRandomPosition ), Random.Range( 0.1f, 2f ) );

        }

    }
}
