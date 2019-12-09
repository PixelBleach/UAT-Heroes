using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Raycast : Weapon {

    [Header("Raycast Properties")]
    public float fireDistance = 100f; //Max distance for raycast gun to shoot
    public bool isPlayer;
    private float nextTimeToFire = 0f; //Timer lockout for inputed fire command

    // Use this for initialization
    public override void Start () {
        currentAmmo = clipSize;
    }

	// Update is called once per frame
	public override void Update () {

        if (currentAmmo <= 0)
        {
            if (Input.GetKeyDown(KeyCode.R) && isReloading == false && isPlayer == true)
            {
                StartCoroutine(Reload());
            }
            return;
        }


        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && isPlayer == true)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }

    }

    public override void Fire()
    {
        muzzleFlash.Play();
        gunSound.PlayOneShot(gunFireSound);

        currentAmmo--;

        RaycastHit hitInfo;
        if ((Physics.Raycast(weaponFirePoint.transform.position, weaponFirePoint.forward, out hitInfo, fireDistance)))
        {
            Health dmgThis;
            dmgThis = hitInfo.transform.gameObject.GetComponent<Health>();
            if (dmgThis != null)
            {
                dmgThis.TakeDamage(damagePerBullet);
            }
        }

        GameObject impactEffect = Instantiate(hitParticlePrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
        Destroy(impactEffect, 1.0f);
    }



}
