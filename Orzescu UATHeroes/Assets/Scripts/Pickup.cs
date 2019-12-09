using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    [Tooltip("Seconds that the pickup exists in the world. Negative value = infinite lifespan.")]
    public float lifespan = -1;


	// Base class Start(). By default, all pickups are infinite duration in world. 
	public virtual void Start () {

	}
	
	// Base class Update. Can be overridden by children 
	public virtual void Update () {

	}

    public virtual void OnPickup(GameObject target)
    {
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        OnPickup(other.gameObject);
    }
}
