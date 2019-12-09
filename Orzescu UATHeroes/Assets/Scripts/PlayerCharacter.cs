using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character {

    [Header("Data to be Assigned in Inspector")]
    public Animator animator;

    private CapsuleCollider coll;
    private Rigidbody rb;
    private float distanceToGround; //Check for grounding function
    [Header("Read Only Variables"), SerializeField]
    private bool isJumping = false; //Bool for jumping functionallity
    [SerializeField]
    private bool isGrounded;

    [Header("Player Character Data"), Tooltip("The speed at which your character turns to face the intended camera direction")]
    public float turnSpeed = 5f;
    [Tooltip("Scales the jump force applied on the rigid body as the character jumps by a set multiplier.")]
    public float jumpScaler = 5f;
    [Tooltip("Scales the x & y directional forces so if you run in a direction in jump, you'll continue in that direction. Increase to long jump stupid far.")]
    public float preJumpVelocityScaler = 0.1f;

    [Header("Weapon Data")]
    public WeaponManager weaponManager;
    public List<Weapon> currentWeapons;
    private Weapon previousWeapon;

    [Header("Physics Controllers")]
    public Ragdoll ragdoll;

    [Header("AI Options")]
    public bool isAI;

	void Start () {
        animator = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        distanceToGround = coll.bounds.extents.y;
        animator.SetBool("isJumping", isJumping);
        ragdoll = GetComponent<Ragdoll>();
	}

    void FixedUpdate()
    {
        isGrounded = IsGrounded();

        if (currentWeapons != weaponManager.allowedWeapons)
        {
            currentWeapons = weaponManager.allowedWeapons;
        }

    }

    public override void Move(Vector2 moveVector)
    {
        animator.SetFloat("Right", moveVector.x);
        animator.SetFloat("Forward", moveVector.y);
    }

    public override void Die()
    {
        isDead = true;

        ragdoll.isRagdoll = true;
    }

    public override void Revive()
    {
        isDead = false;

        ragdoll.isRagdoll = false;
    }

    public override void Jump()
    {
        if (IsGrounded())
        {
            animator.SetTrigger("JumpStart");
            isJumping = true;
            animator.SetBool("isJumping", true);
            animator.SetBool("isGrounded", false);
            animator.applyRootMotion = false;
            Vector3 velVector = animator.velocity * preJumpVelocityScaler;
            rb.AddForce(new Vector3(velVector.x, 1, velVector.z) * jumpScaler, ForceMode.Impulse);
        }

        
    }

    public override void RotateTowards(Vector3 target)
    {
        Vector3 targetVector = target - tf.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetVector);
        tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, turnSpeed);
    }

    public override void Crouch()
    {
        isCrouching = !isCrouching;
        animator.SetBool("isCrouching", isCrouching);
    }

    public override void Walk()
    {
        isWalking = !isWalking;
        animator.SetBool("isWalking", isWalking);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }

    void OnAnimatorIK()
    {

            animator.SetIKPosition(AvatarIKGoal.RightHand, weaponManager.currentWeapon.IK_RH_Point.position);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
            animator.SetIKRotation(AvatarIKGoal.RightHand, weaponManager.currentWeapon.IK_RH_Point.rotation);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

            animator.SetIKPosition(AvatarIKGoal.LeftHand, weaponManager.currentWeapon.IK_LH_Point.position);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, weaponManager.currentWeapon.IK_LH_Point.rotation);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

            //previousWeapon = weaponManager.currentWeapon;
    }

    void OnCollisionEnter(Collision col)
    {
        if (isJumping)
        {
            isJumping = false;
            animator.ResetTrigger("JumpStart");
            animator.SetTrigger("JumpLand");
            animator.SetBool("isGrounded", true);
            animator.SetBool("isJumping", false);
            animator.applyRootMotion = true;
            animator.ResetTrigger("JumpLand");
        }
        
    }

}
