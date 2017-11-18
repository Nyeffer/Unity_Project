using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float moveSpeed;
	private Vector3 direction;
	float counter;
	Transform pos;
	void Awake() {
		direction = new Vector3();
	}

	void Start() {
		counter = 0;
		pos = gameObject.transform;
	}

	void Update() {
		
		if(counter >= 3) {
			Destroy(gameObject);
		}
		pos.position += direction * Time.deltaTime * moveSpeed;
		counter += Time.deltaTime;
	}

	public void ChangeDirection(Vector3 newDirection) {
		direction = newDirection.normalized;
	}
}
