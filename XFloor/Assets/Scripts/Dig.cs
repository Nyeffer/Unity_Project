using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dig : MonoBehaviour {

	void OnTriggerEnter(Collider ground) {
		if(ground.tag == "Ground") {
			Destroy(ground.gameObject);
		}
	}
}
