using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [Header ("IK Points")]
    public Transform IK_RH_Point;
    public Transform IK_LH_Point;

    [Header("Weapon Properties")]
    [Tooltip("Higher #, slower fire rate")]
    public float fireRate;
    public float damagePerBullet = 10f;
    public int clipSize = 30;
    public int currentAmmo;
    public float reloadTime = 1f;
    protected bool isReloading;
    public Transform weaponFirePoint;

    [Header ("Assigned Prefabs")]
    [Tooltip("The prefab of the object the gun shoots.")]
    public GameObject bulletPrefab;
    [Tooltip("The prefab of the particle effect the bullet impact leaves")]
    public GameObject hitParticlePrefab;
    [Tooltip("The prefab of the trail the bullet leaves behind.")]
    public GameObject linePrefab;

    [Header("Cosmetic Weapon Attributes")]
    [Tooltip("The length that the impact of bullets stays around on the level.")]public float particleLifespan;
    public ParticleSystem muzzleFlash;
    public AudioClip gunFireSound;
    public AudioClip gunReloadSound;
    public AudioSource gunSound;


	// Use this for initialization
	public virtual void Start () {
        gunSound = gameObject.GetComponent<AudioSource>();

	}
	
    public void OnEnable()
    {
        isReloading = false;
    }

	// Update is called once per frame
	public virtual void Update () {
        //override w/ child classes

	}


    public virtual void Fire()
    {
        //Override w/ child classes
    }

    //Reload corouting
    protected IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Reloading...");;
        gunSound.PlayOneShot(gunReloadSound); 

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = clipSize;

        isReloading = false;
    }


}
