using System.Collections;
using System.Collections.Generic;
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