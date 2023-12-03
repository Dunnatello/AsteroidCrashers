/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Agent
 * Script Objective: Emulates the player in the main menu to make the menu stand out more.
 * 
 */

using UnityEngine;
using UnityEngine.AI;


namespace TeamBracket.AI {
    public class Agent : MonoBehaviour {


        private NavMeshAgent agent;
        private bool lookingForDestination = false;

        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer character;


        // Start is called before the first frame update
        private void Start( ) {

            agent = GetComponent<NavMeshAgent>( );

            agent.updateRotation = false;

            Invoke( nameof( GoToRandomPosition ), 0.5f );

        }

        private void GoToRandomPosition( ) {

            if ( gameObject.activeInHierarchy ) {

                Vector3 newPosition = new( Random.Range( -15, 15 ), 1.5f, 0f );
                _ = agent.SetDestination( newPosition );

                lookingForDestination = true;

            }

        }

        private void AnimateMovement( ) {

            int moveDirection = ( agent.velocity.x > 0 ) ? 1 : -1;

            if ( Mathf.Abs( agent.velocity.x ) < 0.1f )
                moveDirection = 0;

            bool isMoving = moveDirection != 0;
            animator.SetInteger( "MoveDirection", moveDirection );
            animator.SetBool( "IsMoving", isMoving );

            character.flipX = moveDirection == 1;

        }

        // Update is called once per frame
        private void Update( ) {

            AnimateMovement( );

            if ( lookingForDestination && agent.remainingDistance < agent.stoppingDistance ) {

                lookingForDestination = false;
                Invoke( nameof( GoToRandomPosition ), Random.Range( 0.1f, 2f ) );

            }

        }
    }

}