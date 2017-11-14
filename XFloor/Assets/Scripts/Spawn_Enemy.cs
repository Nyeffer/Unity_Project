using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour {

	public GameObject Spawnable;

	void OnTriggerEnter() {
		Instantiate(Spawnable, gameObject.transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
