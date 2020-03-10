using UnityEngine;

// ********************************
//	Copyright 2018 @Jaydee Alkema
//	dev.jaydeealkema@gmail.com
// ********************************

public class NPC_Spawner : MonoBehaviour {

	public GameObject NPC;		// The NPC object that will spawn here.
	public int amountOfNPCs;	// The amount of NPC's that will spawn.

	// Use this for initialization
	void Start() {
		spawnNPCs(amountOfNPCs);
	}


	private void spawnNPCs(int amountOfNPCs) {
		for(int i = 0; i < amountOfNPCs; i++) {
			GameObject obj = Instantiate(NPC, this.transform.position, NPC.gameObject.transform.rotation);
			Npc npc = obj.GetComponent<Npc>();
			npc.assigned = this.gameObject.GetComponent<Waypoint>();
			npc.init();

		}
	}
}
