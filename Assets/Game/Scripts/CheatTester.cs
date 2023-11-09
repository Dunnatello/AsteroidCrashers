using TeamBracket;
using UnityEngine;

public class CheatTester : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        
        if ( Input.GetKeyDown( KeyCode.Backspace ) ) {

            Debug.Log( "Activating Score Cheat" );

            for ( int i = 0; i < 10; i++ ) {

                gameManager.AddScore( );

            }

        }


    }
}
