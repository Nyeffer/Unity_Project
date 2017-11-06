using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTag : MonoBehaviour {

	public string myTag;

	void Awake() {
		setTag(myTag);
	}
	
	void setTag (string myTag) {
		gameObject.tag = myTag;
	}
}
