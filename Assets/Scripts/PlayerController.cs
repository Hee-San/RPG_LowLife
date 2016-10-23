using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
	public float speed = 6.0F;
	public float destinationMargin = 8f;
	private Vector3 moveDirection = Vector3.zero;
	public Transform Joint;
	private Transform Destination;

	public enum Direction
	{
		U,
		UR,
		R,
		DR,
		D,
		DL,
		L,
		UL,
		Idle
	}

	public Direction direction;
	private Animator m_Anim;
	private SpriteRenderer spriteRen;
	private Rigidbody2D rb;
	void Awake(){
		Destination = transform.Find ("Destination");
		spriteRen = GetComponent<SpriteRenderer> ();
		m_Anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D> ();
	}
	void Update() {
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis ("Vertical");
		rb.velocity = new Vector2 (0, 0);
		CharacterController controller = GetComponent<CharacterController>();
		moveDirection = new Vector3(h,v, 0);
		moveDirection = transform.TransformDirection(moveDirection);
		Destination.transform.position = transform.position + moveDirection;
		moveDirection *= speed;
		//controller.Move(moveDirection * Time.deltaTime);
		Joint.transform.position = transform.position + moveDirection;
		m_Anim.SetFloat("X_Direction", h);
		m_Anim.SetFloat("Y_Direction", v);
		AnimationUpdate (h, v);
		/*if (rb.velocity.y*v<=0) {
			rb.velocity = new Vector2(rb.velocity.x, 0);
		}
		if (rb.velocity.x*h<=0) {
			rb.velocity = new Vector2 (0, rb.velocity.y);
		}*/
	}
	void AnimationUpdate(float h, float v){
		if (h == 0f && v == 0f) {
			return;
		}
		int n = 0;
		Direction Dir = directionUpdate (h, v);
		switch (Dir) {
		case Direction.D:
			n = 0;
			break;
		case Direction.DR:
			n = 1;
			break;
		case Direction.R:
			n = 2;
			break;
		case Direction.UR:
			n = 3;
			break;
		case Direction.U:
			n = 4;
			break;
		case Direction.UL:
			n = 5;
			break;
		case Direction.L:
			n = 6;
			break;
		case Direction.DL:
			n = 7;
			break;
		}
		direction = Dir;
		m_Anim.SetFloat("Direction", (float) n/7f);
	}

	public Direction directionUpdate(float Hor, float Ver){
		Direction Dir;
		if (Hor > 0) {
			if (Ver > 0) {
				return Direction.UR;
			} else if (Ver < 0) {
				return Direction.DR;
			} else {
				return Direction.R;
			}
		} else if (Hor < 0) {
			if (Ver > 0) {
				return Direction.UL;
			} else if (Ver < 0) {
				return Direction.DL;
			} else {
				return Direction.L;
			}
		} else {
			if (Ver > 0) {
				return Direction.U;
			} else if (Ver < 0) {
				return Direction.D;
			} else {
				return Direction.Idle;
			}
		}
	}
		
}
