using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ragdoll : MonoBehaviour {

    public bool isRagdoll;
    
    //OFF ragdoll
    private Animator animator;
    private Collider mainCollider;
    private Rigidbody mainRigidbody;

    //ON ragdoll
    public List<Collider> ragdollColliders;
    public List<Rigidbody> ragdollRigidbodies;

	void Start () {

        animator = GetComponent<Animator>();
        mainCollider = GetComponent<Collider>();
        mainRigidbody = GetComponent<Rigidbody>();

        //creates a list of colliders in the game object, all childed to the main game object
        ragdollColliders = GetComponentsInChildren<Collider>().ToList();
        //removes the main gameobject collider from the list
        ragdollColliders.Remove(mainCollider);
        //do the same for rigid bodies
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>().ToList();
        ragdollRigidbodies.Remove(mainRigidbody);

	}
	
	// Update is called once per frame
	void Update () {

        if (isRagdoll)
            RagdollOn();
        else
            RagdollOff();
	}

    public void RagdollOn()
    {
        //Turn off the main stuff
        mainCollider.enabled = false;
        mainRigidbody.isKinematic = true;
        animator.enabled = false;

        //enable the rest of the ragdoll col's and rb's
        foreach (Rigidbody rb in ragdollRigidbodies)
            rb.isKinematic = false;

        foreach (Collider col in ragdollColliders)
            col.enabled = true;
    }

    public void RagdollOff()
    {
        //Turn on the main stuff
        mainCollider.enabled = true;
        mainRigidbody.isKinematic = false;
        animator.enabled = true;

        //Turn off the rest of the ragdoll col's and rb's
        foreach (Rigidbody rb in ragdollRigidbodies)
            rb.isKinematic = true;

        foreach (Collider col in ragdollColliders)
            col.enabled = false;
    }
}
