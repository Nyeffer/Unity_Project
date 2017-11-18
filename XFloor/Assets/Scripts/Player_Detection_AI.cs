using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Detection_AI : MonoBehaviour {

	public Player target;
	private Rigidbody rb;
	public Vector3 velocity;
	public Vector3 steering;
	public Vector3 desiredVelocity;

	public bool isSeeking;
	private Animator anim;

	public float moveSpeed;
	public float counter = 0;

	void Awake() {
		rb = GetComponent<Rigidbody>();
		velocity = rb.velocity;
		anim = GetComponent<Animator>();
	}

	void Start() {
		moveSpeed = 1;
	}

	void Update() {

	}

	void FixedUpdate() {
		if(isSeeking) {
			Vector3 pos = gameObject.transform.position;
			if(target.transform.position.x < transform.position.x) {
			pos.x -= moveSpeed * Time.deltaTime;
			gameObject.transform.position = pos;
			} else if (target.transform.position.x > transform.position.x) {
				pos.x += moveSpeed * Time.deltaTime;
				gameObject.transform.position = pos;
			}
		} 
	}

	void OnTriggerEnter(Collider location) {
		if(location.gameObject.tag == "Player") {
		  isSeeking = true;
		  anim.SetBool("Walk", true);
		}

		if(location.gameObject.tag == "Fire") {
			Destroy(location.gameObject);
			Destroy(this.gameObject);
		}
	}

	void OnTriggerExit(Collider location) {
		if(location.gameObject.tag == "Player") {
			isSeeking = false;
			anim.SetBool("Walk", false);
		}
	}

	void OnCollisionStay(Collision other) {
		if(other.gameObject.tag == "Player") {
			if (counter <= 1) {
				anim.SetBool("Atk", true);
			} else {
				target.UpdateHealth(target.TakeDamage(10));
				counter = 0;
			}
			counter += Time.deltaTime; 
		}
	}

	void OnCollisionExit(Collision other) {
		if(other.gameObject.tag == "Player") {
			anim.SetBool("Atk", false);
		}
	}
}
