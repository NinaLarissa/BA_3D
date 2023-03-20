using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] public Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    RaycastHit hit;

    public AudioSource source;
    public AudioClip fireSound;
    public AudioClip reload;
    public AudioClip noAmmo;

    public Magazine magazine;

    //Blood Effect
    public GameObject bloodEffect;

    // Used to damage enemy
    [SerializeField]
    float damageEnemy = 10f;



    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
    }


    public void Update()
    {
        //If you want a different input, change it here
        if (Input.GetButtonDown("Fire1"))
        {
            if (magazine && magazine.numberOfBullet > 0)
            {
                //Calls animation on the gun that has the relevant animation events that will fire
                gunAnimator.SetTrigger("Fire");
            }
            else
            {
                source.PlayOneShot(noAmmo);
            }
        }
        }


    //This function creates the bullet behavior
    void Shoot()
    {
        magazine.numberOfBullet--;

        source.PlayOneShot(fireSound);

        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

        if (Physics.Raycast(barrelLocation.position, barrelLocation.forward, out hit, shotPower))
        {
            if (hit.transform.tag == "Enemy")
            {
                Debug.Log("HitEnemy");
                EnemyHealth enemyHealthScript = hit.transform.GetComponent<EnemyHealth>();
                enemyHealthScript.DeductHealth(damageEnemy);
                Instantiate(bloodEffect, hit.point, Quaternion.identity, hit.transform);

            }

            if (hit.transform.tag == "Target")
            {
                Debug.Log("HitTartget");
                TargetHealth targetDestroyedScript = hit.transform.GetComponent<TargetHealth>();
                targetDestroyedScript.DeductHealth(damageEnemy);

            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Magazine")
        {
            if (magazine && magazine.numberOfBullet <= 0)
            {
                Debug.Log("Entered");
            
                magazine.numberOfBullet = 8;
                Destroy(other.gameObject);
            }

        }
    }


    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

}
