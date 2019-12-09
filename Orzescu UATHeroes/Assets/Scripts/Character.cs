using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [Header("Character Data")]
    public Transform tf;
    public bool isDead = false;
    public bool isCrouching = false;
    public bool isWalking = false;

    void Awake()
    {
        tf = GetComponent<Transform>();
    }

    public virtual void Move(Vector2 moveVector) { }
    public virtual void Die() { }
    public virtual void Revive() { }
    public virtual void RotateTowards(Vector3 target) { }
    public virtual void Crouch() { }
    public virtual void Jump() { }
    public virtual void Walk() { }

}
