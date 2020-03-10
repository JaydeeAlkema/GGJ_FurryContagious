using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

	// Update is called once per frame
	void Update() {
		this.gameObject.transform.Rotate(0.0f, 0.0f, -1.0f);
	}
}
