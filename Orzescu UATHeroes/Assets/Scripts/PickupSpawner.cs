using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour {

    public GameObject currentPickup;
    public float timeBetweenSpawns = 20f;

    public GameObject PickupPrefab;

    private bool hasPickupSpawned;
    private float timer;

	// Use this for initialization
	void Start () {
        currentPickup = Instantiate(PickupPrefab, transform.position, Quaternion.identity, transform) as GameObject;
        hasPickupSpawned = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (currentPickup)
        {
            return;
        } else
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenSpawns)
            {
                SpawnPickup();
                timer = 0;
            }
        }

	}

    void SpawnPickup()
    {
        GameObject newPickup;
        newPickup = Instantiate(PickupPrefab, transform.position, Quaternion.identity, transform) as GameObject;
        currentPickup = newPickup;
    }

}
