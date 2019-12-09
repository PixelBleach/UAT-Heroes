using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    private NavMeshAgent agent;
    private Animator animator;
    private Character character;

    public Character target;
    public Weapon currentWeapon;
    [Range(0, 90)] public float firingAngle = 10f;

    private bool hasShot = false;

	// Use this for initialization
	void Start () {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        character = GetComponent<PlayerCharacter>();

	}
	
	// Update is called once per frame
	void Update () {

        if (!character.isDead)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

            if (target != null)
            {
                SeekTarget(target);

                Vector3 targetDirection = target.transform.position - transform.position;
                float angle = Vector3.Angle(targetDirection, transform.forward);
                if (angle <= firingAngle && hasShot == false)
                {
                    StartCoroutine(ShotDelayer());
                }
            }
        }

	}


    void SeekTarget(Character target)
    {
        agent.SetDestination(target.tf.position);
        Vector3 desiredVelocity = agent.desiredVelocity;
        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, 1.0f);
        desiredVelocity = character.tf.InverseTransformDirection(desiredVelocity);
        character.Move(new Vector2(desiredVelocity.x / 2, desiredVelocity.z / 2));
    }

    public void OnAnimatorMove()
    {
        agent.velocity = animator.velocity;
    }

    protected IEnumerator ShotDelayer()
    {
        hasShot = true;

        currentWeapon.Fire();

        yield return new WaitForSeconds(3);

        hasShot = false;
    }
}
