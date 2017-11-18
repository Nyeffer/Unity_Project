using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwing : MonoBehaviour {

	private float counter = 0;
	private float origRot;
	void Awake() {
		origRot = gameObject.transform.rotation.z;
	}
	void Update() {

		if (counter <= 1) {
			Debug.Log(origRot);
			gameObject.transform.Rotate(new Vector3(0, 0, -2f));
			counter += Time.deltaTime;
		} else {
			gameObject.transform.Rotate(new Vector3 (0, 0, origRot));
			counter = 0;
			Destroy(this.gameObject);
		}
	}
}
