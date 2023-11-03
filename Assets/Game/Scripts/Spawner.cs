using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private float spawnInterval;
    [SerializeField] private int maxObjectsPerSpawn;
    [SerializeField] private GameObject spawnObjectPrefab;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float spawnHorizontalSpread;

    private IEnumerator SpawnObject( ) {

        while ( this.enabled ) {

            yield return new WaitForSeconds( spawnInterval );
            Debug.Log( "SPAWNING" );

            int numObjectsToSpawn = Random.Range( 1, maxObjectsPerSpawn );

            for ( int i = 0; i < numObjectsToSpawn; i++ ) {

                GameObject newSpawnedObject = Instantiate( spawnObjectPrefab );
                Rigidbody rb = newSpawnedObject.GetComponent< Rigidbody >( );

                newSpawnedObject.transform.position = transform.position + new Vector3( Random.Range( -spawnHorizontalSpread, spawnHorizontalSpread ), 0, 0 );
                rb.velocity = new( Random.Range( -maxSpeed, maxSpeed ), rb.velocity.y, Random.Range( -maxSpeed, maxSpeed ) );

            }
        
        }
    
    }

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine( nameof( SpawnObject ) );

    }

}
