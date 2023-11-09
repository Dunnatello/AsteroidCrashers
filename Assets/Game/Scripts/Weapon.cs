/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Weapon
 * Script Objective: Handles weapon interactions and movement. Allows for firing projectiles with customizable settings.
 * 
 */
using TMPro;
using UnityEngine;

namespace TeamBracket.WeaponSystem {
    public class Weapon : MonoBehaviour {

        // References
        [Header( "References" )]
        [SerializeField] private Transform character;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject currentWeapon;

        // Internal References
        private Transform weaponTransform;
        private SpriteRenderer currentWeaponSprite;

        // Prefabs
        [SerializeField] private GameObject bulletPrefab;

        // UI
        [SerializeField] private TextMeshProUGUI remainingBulletsText;
        [SerializeField] private TextMeshProUGUI ammoText;

        [Header( "Weapon Settings" )]
        [SerializeField] private float orbitRotationSpeed = 5f;


        // Weapon Attributes
        private int ammo;
        private int remainingBullets;

        [SerializeField] private bool infiniteAmmo;



        [SerializeField] private int maxBulletsPerClip;

        [SerializeField] private int bulletsPerTap;
        [SerializeField] private float fireDelay;
        [SerializeField] private float timeBetweenBullets;
        [SerializeField] private float bulletForce;

        [SerializeField] private float reloadTime;

        private float currentDelay;
        private bool canFire;
        private bool isReloading = false;

        public float FireDelay => fireDelay;

        private void SetWeaponTransform( ) {

            weaponTransform = currentWeapon.transform;
            currentWeaponSprite = currentWeapon.GetComponent<SpriteRenderer>( );

        } // End of SetWeaponTransform( )

        // Start is called before the first frame update
        private void Start( ) {

            SetWeaponTransform( );

            Reload( );

            canFire = true;

            UpdateWeaponUI( );

        } // Endof Start( )

        private void LookAtMousePosition( ) { // Using this for Rotating Weapon Angle: https://www.youtube.com/watch?v=Geb_PnF1wOk

            // Set Weapon Position
            Vector3 characterPosition = character.position;
            transform.position = characterPosition;

            // Calculate Facing Direction
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
            Vector3 direction = mousePosition - characterPosition;

            float pointAngle = Mathf.Atan2( direction.y, direction.x ) * Mathf.Rad2Deg;


            // Point Weapon Towards Mouse
            weaponTransform.rotation = Quaternion.AngleAxis( pointAngle, Vector3.forward );

            // Orbit Weapon Around Player
            transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.Euler( 0, 0, pointAngle ), orbitRotationSpeed * Time.deltaTime );

            // Flip Weapon Based on Orientation
            currentWeaponSprite.flipY = transform.localEulerAngles.z > 90f && transform.localEulerAngles.z < 270;


        } // Endof LookAtMousePosition( )

        private void ShootBullet( ) {

            GameObject newBullet = Instantiate( bulletPrefab );
            newBullet.transform.position = firePoint.position;

            Vector3 shootDirection = ( firePoint.position - transform.position ).normalized;
            float shootAngle = Mathf.Atan2( shootDirection.y, shootDirection.x ) * Mathf.Rad2Deg;


            newBullet.transform.rotation = Quaternion.Euler( 0, 0, shootAngle - 90f );

            Rigidbody rigidbody = newBullet.GetComponent<Rigidbody>( );

            Vector3 bulletDirection = firePoint.right * bulletForce;
            rigidbody.AddForce( bulletDirection, ForceMode.Force );

        } // Endof ShootBullet( )

        private void UpdateWeaponUI( ) {

            remainingBulletsText.text = isReloading ? "<b>-</b>" : "<b>" + remainingBullets.ToString( ) + "</b>";
            ammoText.text = infiniteAmmo ? "∞" : ammo.ToString( );

        }

        private void Reload( ) {


            if ( infiniteAmmo ) {

                remainingBullets = maxBulletsPerClip;

            }
            else {

                // If current ammo exceeds the required bullets, subtract maxBulletsPerClip from ammo.
                if ( ammo > maxBulletsPerClip ) {

                    int bulletsToSubtract = maxBulletsPerClip - remainingBullets;

                    remainingBullets = maxBulletsPerClip;
                    ammo -= bulletsToSubtract;

                }
                else { // Set remaining bullets equal to the rest of the ammo left.

                    remainingBullets = ammo;
                    ammo = 0;

                }

            }

            isReloading = false;
            UpdateWeaponUI( );

        } // Endof Reload( )

        private void StartReload( ) {

            // Weapon does not need to be reloaded.
            if ( remainingBullets == maxBulletsPerClip )
                return;

            isReloading = true;
            UpdateWeaponUI( );

            Invoke( nameof( Reload ), reloadTime );

        }

        private void Fire( ) {

            if ( isReloading ) {

                Debug.Log( "FIXME: Play Error Sound" );
                return;

            }

            int bulletsToFire = bulletsPerTap;

            if ( remainingBullets < bulletsPerTap )
                bulletsToFire = remainingBullets;



            remainingBullets -= bulletsToFire;


            if ( remainingBullets < 0 ) {

                canFire = false;
                remainingBullets = 0;

            }

            UpdateWeaponUI( );


            if ( !canFire ) {

                // FIXME: Play Weapon Empty Sound
                Debug.Log( "FIXME: Weapon Empty Sound" );
                return;

            }

            if ( bulletsToFire > 1 ) {

                for ( int i = 0; i < bulletsToFire; i++ ) {

                    Invoke( nameof( ShootBullet ), timeBetweenBullets * i );

                }

            }
            else {

                ShootBullet( );

            }

            currentDelay = fireDelay;

        } // Endof Fire( )


        // Update is called once per frame
        private void Update( ) {

            // If weapon is attached to the player, allow weapon movement.
            if ( weaponTransform ) {

                LookAtMousePosition( );

            }

            // Cannot fire gun, so do an early return here.
            if ( currentDelay > 0f ) {

                currentDelay -= Time.deltaTime;
                return;
            }

            // Fire Button
            if ( Input.GetMouseButtonDown( 0 ) )
                Fire( );

            // Reload Button
            if ( Input.GetKeyDown( KeyCode.R ) )
                StartReload( );

        } // Endof Update()

    } // Endof Weapon

} // End of namespace