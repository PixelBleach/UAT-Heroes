using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public Weapon currentWeapon;

    public int selectedWeapon = 0;
    public int previousSelectedWeapon;

    public List<Weapon> allowedWeapons;
    public List<Weapon> allWeapons;

	// Use this for initialization
	void Start () {
        SelectWeapon();
	}
	
	// Update is called once per frame
	void Update () {

        previousSelectedWeapon = selectedWeapon;

        #region MouseScroll Wheel Controls

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedWeapon >= allowedWeapons.Count - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = allowedWeapons.Count - 1;
            else
                selectedWeapon--;
        }

        #endregion

        #region Number Button Controls

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && allowedWeapons.Count >= 2)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && allowedWeapons.Count >= 3)
        {
            selectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && allowedWeapons.Count >= 4)
        {
            selectedWeapon = 3;
        }

        #endregion

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

	}

    void SelectWeapon()
    {
        int i = 0;
        foreach (Weapon weapon in allowedWeapons)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                currentWeapon = weapon;
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
