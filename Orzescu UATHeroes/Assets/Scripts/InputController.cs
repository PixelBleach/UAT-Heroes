using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	public Character character;

    public Transform LookAtTransform;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (character != null)
        {
            Vector2 controllerPosition = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            controllerPosition = Vector2.ClampMagnitude(controllerPosition, 1.0f);

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                character.Walk();
            }

            if (Input.GetButtonDown("Jump"))
            {
                character.Jump();
            }

            character.Move(controllerPosition);
            character.RotateTowards(LookAtTransform.position);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                character.Die();
                gameObject.BroadcastMessage("DisableCamera");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                character.Revive();
                gameObject.BroadcastMessage("EnableCamera");
            }

        }

	}
}
