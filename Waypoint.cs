using UnityEngine;

// ********************************
//	Copyright 2018 @Jaydee Alkema
//	dev.jaydeealkema@gmail.com
// ********************************

public class Waypoint : MonoBehaviour {

	public float waitSeconds;	// This is the amount of seconds the NPC will wait at the Waypoint before moving to the next.
	public float speedOut;		// The speed at which the NPC's will walk away from the Waypoint.

	private void Start() {
		waitSeconds = 0;
		speedOut = 0;
	}
}
