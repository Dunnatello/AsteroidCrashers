/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Spawner
 * Script Objective: Spawns a specified object with a list of modifiable parameters. These parameters are set in the Unity Inspector and will not be modified during runtime.
 * 
 */

using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private float spawnInterval;
    [SerializeField] private int maxObjectsPerSpawn;
    [SerializeField] private GameObject spawnObjectPrefab;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float spawnHorizontalSpread;

    private IEnumerator spawnCoroutine;

    // Spawn objects forever until the current game ends.
    private IEnumerator SpawnObject( ) {

        while ( this.enabled ) {

            yield return new WaitForSeconds( spawnInterval );
            Debug.Log( "SPAWNING" );

            int numObjectsToSpawn = Random.Range( 1, maxObjectsPerSpawn );

            // Create each object based on numObjectsToSpawn's value.
            for ( int i = 0; i < numObjectsToSpawn; i++ ) {

                GameObject newSpawnedObject = Instantiate( spawnObjectPrefab );
                Rigidbody rb = newSpawnedObject.GetComponent< Rigidbody >( );

                // Set random position based on horizontal spread and a random velocity based on the maxSpeed.
                newSpawnedObject.transform.position = transform.position + new Vector3( Random.Range( -spawnHorizontalSpread, spawnHorizontalSpread ), 0, 0 );
                rb.velocity = new( Random.Range( -maxSpeed, maxSpeed ), rb.velocity.y, Random.Range( -maxSpeed, maxSpeed ) );
                
                rb.angularVelocity = new( 0f, 0f, Random.Range( -1f, 1f ) );

            }
        
        }
    
    }

    // Start is called before the first frame update
    void Awake( )
    {

        spawnCoroutine = SpawnObject( );
        StartCoroutine( spawnCoroutine );


    }


}