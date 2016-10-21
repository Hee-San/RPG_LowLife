using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 6.0F;
	public float destinationMargin = 8f;
	private Vector3 moveDirection = Vector3.zero;
	private Transform Destination;

	void Start(){
		Destination = transform.Find ("Destination");
	}
	void Update() {
		CharacterController controller = GetComponent<CharacterController>();
		moveDirection = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"), 0);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		controller.Move(moveDirection * Time.deltaTime);
		Destination.transform.position = transform.position + moveDirection;
	}
}
