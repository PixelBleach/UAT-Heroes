using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_newWeapon : Pickup {

    public Weapon newWeapon;

	// Use this for initialization
	public override void Start () {
		
	}
	
	// Update is called once per frame
	public override void Update () {
		
	}

    public override void OnPickup(GameObject target)
    {
        base.OnPickup(target);
    }

    public override void OnTriggerEnter(Collider other)
    {
        Transform weaponManagerChild = other.gameObject.transform.Find("WeaponHolder");
        if (weaponManagerChild)
        {
            WeaponManager WM = weaponManagerChild.gameObject.GetComponent<WeaponManager>();

            //if new weapon is already allowed for the player
            if (WM.allowedWeapons.Contains(newWeapon))
            {
                //return
                return;
            }
            else
            {
                //add the weapon to the allowed weapons list
                WM.allowedWeapons.Add(newWeapon);
                OnPickup(gameObject);
            }

        }
        else
        {
            return;
        }
    }
}
