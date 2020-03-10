using UnityEngine;
using System.Collections;

// ********************************
//	Copyright 2018 @Jaydee Alkema
//	dev.jaydeealkema@gmail.com
// ********************************

public class Npc : MonoBehaviour {
	public Waypoint[] wayPoints;        // These are all the Waypoints that the NPC can walk to.
	public bool isCircular = true;      // This makes it so that the waypoints Start & End connect.
										// Always true at the beginning because the moving object will always move towards the first waypoint.
	public bool inReverse = false;      // If the NPC goes to the next or previous checkpoint.
	public float randSpeed, randDirection, randStayTime, randLeaveSpeed;
	private float randChar;             // Gives the NPC a random Character Sprite.
	public Waypoint assigned = null;    // The current Waypoint at which the NPC spawns.
	private Waypoint currentWaypoint;   // Current Waypoint pointer.
	private int currentIndex;           // CurrentIndex in the Waypoints Array.
	private bool isWaiting = false;     // If the NPC is waiting at a Waypoint.
	private float speedStorage = 0;     // The last known speed at which the NPC moved.

	/**
	 * Initialisation
	 * 
	 */
	void Start() {
		this.randomizeValues();
		if(this.randChar > 0.0f && this.randChar < 1.0f) {
			this.GetComponentInChildren<Animator>().SetBool("Character1", true);
		}
		else if(this.randChar > 1.0f && this.randChar < 2.0f) {
			this.GetComponentInChildren<Animator>().SetBool("Character2", true);
		}
		else if(this.randChar > 2.0f && this.randChar < 3.0f) {
			this.GetComponentInChildren<Animator>().SetBool("Character3", true);
		}
		//if(this.wayPoints.Length <= 0) return;					************************************************
		//this.currentIndex = 0;											This Breaks the Waypoint System!		
		//this.currentWaypoint = this.wayPoints[this.currentIndex]; ************************************************
	}

	public void init() {
		if(this.assigned != null) {
			for(int i = 0; i < this.wayPoints.Length; i++) {
				if(this.wayPoints[i] == this.assigned) {
					this.currentIndex = i;
					break;
				}
			}
		}
		print(this.currentIndex);
		this.currentWaypoint = this.wayPoints[this.currentIndex];
	}

	/**
	 * Update is called once per frame
	 * 
	 */
	void Update() {
		if(this.randDirection > 0.5f) { this.inReverse = false; }   // Make it random when the NPC should walk towards the next Waypoint
		else { this.inReverse = true; }                             // Or if it wants to walk to the previous Waypoint
		if(this.currentWaypoint != null && !this.isWaiting) {
			this.MoveTowardsWaypoint();
		}
	}

	/**
	 * Pause the mover
	 * 
	 */
	void Pause() {
		this.isWaiting = !this.isWaiting;
	}

	/**
	 * Move the object towards the selected waypoint
	 * 
	 */
	private void MoveTowardsWaypoint() {
		// Get the moving objects current position
		Vector3 currentPosition = this.transform.position;

		// Get the target waypoints position
		Vector3 targetPosition = this.currentWaypoint.transform.position;

		// If the moving object isn't that close to the waypoint
		if(Vector3.Distance(currentPosition, targetPosition) > .1f) {

			// Get the direction and normalize
			Vector3 directionOfTravel = targetPosition - currentPosition;
			directionOfTravel.Normalize();
			if(targetPosition.x > currentPosition.x) {
				this.GetComponentInChildren<SpriteRenderer>().flipX = false;
			}
			else {
				this.GetComponentInChildren<SpriteRenderer>().flipX = true;
			}

			//scale the movement on each axis by the directionOfTravel vector components
			this.transform.Translate(
				directionOfTravel.x * this.randSpeed * Time.deltaTime,
				directionOfTravel.y * this.randSpeed * Time.deltaTime,
				0,
				Space.World
			);
		}
		else {

			// If the waypoint has a pause amount then wait a bit
			if(this.currentWaypoint.waitSeconds > 0) {
				this.Pause();
				Invoke("Pause", this.currentWaypoint.waitSeconds);
				this.randomizeValues();
			}

			// If the current waypoint has a speed change then change to it
			if(this.currentWaypoint.speedOut > 0) {
				this.speedStorage = this.randLeaveSpeed;
				this.randLeaveSpeed = this.currentWaypoint.speedOut;
				this.randomizeValues();
			}
			else if(this.speedStorage != 0) {
				this.randLeaveSpeed = this.speedStorage;
				this.speedStorage = 0;
				this.randomizeValues();
			}

			this.NextWaypoint();
		}
	}

	/**
	 * Work out what the next waypoint is going to be
	 * 
	 */
	private void NextWaypoint() {
		if(this.isCircular) {

			if(!this.inReverse && this.currentIndex > 0) {
				this.currentIndex = (this.currentIndex + 1 >= this.wayPoints.Length) ? 0 : this.currentIndex + 1;
				this.randomizeValues();
			}
			else {
				this.currentIndex = (this.currentIndex == 0) ? this.wayPoints.Length - 1 : this.currentIndex - 1;
				this.randomizeValues();
			}

		}
		else {

			// If at the start or the end then reverse
			if((!this.inReverse && this.currentIndex + 1 >= this.wayPoints.Length) || (this.inReverse && this.currentIndex == 0)) {
				this.inReverse = !this.inReverse;
				this.randomizeValues();
			}
			this.currentIndex = (!this.inReverse) ? this.currentIndex + 1 : this.currentIndex - 1;

		}
		this.randomizeValues();
		this.currentWaypoint = this.wayPoints[currentIndex];
	}

	/**
	 * Randomize the speeds and booleans of the npc. 
	 * This makes it look a little bit more alive.
	 * 
	 */
	private void randomizeValues() {
		this.randChar = Random.Range(0.0f, 3.0f);
		this.randSpeed = Random.Range(0.5f, 1.0f);
		this.randDirection = Random.Range(0.0f, 1.0f);
		this.randStayTime = Random.Range(0.0f, 0.10f);
		this.randLeaveSpeed = Random.Range(1.5f, 3.0f);
	}
}