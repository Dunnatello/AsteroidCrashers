using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] private Transform character;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject currentWeapon;

    private Transform weaponTransform;
    private SpriteRenderer currentWeaponSprite;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float orbitRotationSpeed = 5f;
    //[SerializeField] private float minZRotation = -15f;
    //[SerializeField] private float maxZRotation = 195f;

    // Weapon Attributes
    [SerializeField] private int ammo;
    [SerializeField] private int remainingBullets;
    [SerializeField] private int maxBulletsPerClip;
    [SerializeField] private int bulletsPerTap;
    [SerializeField] private float fireDelay;
    [SerializeField] private float timeBetweenBullets;

    [SerializeField] private float bulletForce;

    private float currentDelay;
    private bool canFire;

    void SetWeaponTransform( ) {

        weaponTransform = currentWeapon.transform;
        currentWeaponSprite = currentWeapon.GetComponent< SpriteRenderer >( );

    }

    // Start is called before the first frame update
    void Start()
    {

        SetWeaponTransform( );

        ammo = maxBulletsPerClip * 3;
        remainingBullets = maxBulletsPerClip;

        canFire = true;

    }

    void LookAtMousePosition( ) { // Using this for Rotating Weapon Angle: https://www.youtube.com/watch?v=Geb_PnF1wOk

        Vector3 characterPosition = character.position;
        transform.position = characterPosition;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        Vector3 direction = mousePosition - characterPosition;

        //Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint( weaponTransform.position );

        float pointAngle = Mathf.Atan2( direction.y, direction.x ) * Mathf.Rad2Deg;
        //float orbitAngle = Mathf.Clamp( pointAngle, minZRotation, maxZRotation );


        //Debug.Log( pointAngle + " " + orbitAngle + ( pointAngle == orbitAngle ).ToString( ) );

        //transform.position = characterPosition + offsetFromCharacter;

        // Point Weapon Towards Mouse
        weaponTransform.rotation = Quaternion.AngleAxis( pointAngle, Vector3.forward );

        // Orbit Weapon Around Player
        transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.Euler( 0, 0, pointAngle ), orbitRotationSpeed * Time.deltaTime );

/*        // Clamp Z Axis Movement
        Vector3 currentEulerAngles = transform.localEulerAngles;
        currentEulerAngles.z = Mathf.Clamp( currentEulerAngles.z, minZRotation, maxZRotation );

        // Apply Clamp Correction
        transform.localEulerAngles = currentEulerAngles;*/

        currentWeaponSprite.flipY = ( transform.localEulerAngles.z > 90f && transform.localEulerAngles.z < 270 );


    }
    
    void ShootBullet( ) {

        GameObject newBullet = Instantiate( bulletPrefab );
        newBullet.transform.position = firePoint.position;

        Vector3 shootDirection = ( firePoint.position - transform.position ).normalized;
        float shootAngle = Mathf.Atan2( shootDirection.y, shootDirection.x ) * Mathf.Rad2Deg;


        newBullet.transform.rotation = Quaternion.Euler( 0, 0, shootAngle - 90f );

        Rigidbody rigidbody = newBullet.GetComponent< Rigidbody >();

        Vector3 bulletDirection = firePoint.right * bulletForce;
        rigidbody.AddForce( bulletDirection, ForceMode.Force );

    }

    void Fire( ) {


        remainingBullets -= bulletsPerTap;

        if ( remainingBullets < 0 ) {

            canFire = false;
            remainingBullets = 0;

        }

        if ( bulletsPerTap > 1 ) {

            for ( int i = 0; i < bulletsPerTap; i++ ) {

                Invoke( nameof( ShootBullet ), timeBetweenBullets * i );

            }

        } else {

            ShootBullet( );

        }

        
        currentDelay = fireDelay;

    }

    // Update is called once per frame
    void Update()
    {
        
        if ( weaponTransform ) {

            LookAtMousePosition( );

        }

        if ( currentDelay > 0f ) {

            currentDelay -= Time.deltaTime;
            return;
        }

        if ( Input.GetMouseButtonDown( 0 ) ) {

            Fire( );

        }
    }
}
