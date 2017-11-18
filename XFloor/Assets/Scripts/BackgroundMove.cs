using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour {


	public Player playerCheck;
	public float m_moveSpeed;
	private Rigidbody rb;

	void Awake() {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = gameObject.transform.position;
		// Player Move Left and Right
		pos.x = playerCheck.transform.position.x;
		gameObject.transform.position = pos;
	}
}
