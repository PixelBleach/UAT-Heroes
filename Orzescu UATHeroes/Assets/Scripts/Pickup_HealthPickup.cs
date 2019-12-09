using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_HealthPickup : Pickup {

    [Header("Health Pickup Properties")]
    public float healthHealed = 10f;


	// Use this for initialization
	public override void Start () {
        if (lifespan >= 0)
        {
            Destroy(gameObject, lifespan);
        }
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
        Health otherHP = other.gameObject.GetComponent<Health>();

        if (otherHP != null)
        {
            if (otherHP.currentHealth < otherHP.maxHealth)
            {
                otherHP.TakeDamage(-healthHealed);
                OnPickup(gameObject);
            }
            //if full HP don't do anything
            return;
        }
    }

}
