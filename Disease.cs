using UnityEngine;

// ********************************
//	Copyright 2018 @Mirco Baalmans
//	mircobaalmans@live.nl
// ********************************

public class Disease : MonoBehaviour {

	public Animator anim;
	private float chanceToBeImmune;
	private Color immuneColor;

	//status ennum
	public enum status {
		Infected,
		Normal,
		Immune
	};
	//set start status
	status stat = status.Normal;

	public void Start() {
		PlayerPrefs.SetInt("hasClicked", 0);
		this.anim = gameObject.GetComponentInChildren<Animator>();
		this.tag = "Human";
		this.immuneColor = new Color(0.0f, 1.0f, 1.0f);
		this.immuneChance();
	}
	// A human could be immune to the virus, and also cure the furries
	public void immuneChance() {
		this.chanceToBeImmune = Random.Range(0.0f, 1.0f);
		if(this.chanceToBeImmune <= 0.15f) {
			this.stat = status.Immune;
			this.gameObject.GetComponentInChildren<SpriteRenderer>().color = this.immuneColor;
		}
	}
	//select the first furry
	public void OnMouseDown() {
		if(this.stat != status.Immune && PlayerPrefs.GetInt("hasClicked") == 0) {
			this.stat = status.Infected;
			this.tag = "Furry";
			this.anim.SetBool("IsInfected", true);
			PlayerPrefs.SetInt("hasClicked", 1);
		}
	}
	//infect humans with the furry
	public void OnTriggerEnter2D(Collider2D collision) {

		if(this.stat == status.Infected) {

			if(collision.gameObject.GetComponentInChildren<Disease>().stat == status.Immune) {
				this.GetComponentInChildren<Disease>().stat = status.Normal;
				this.GetComponentInChildren<Animator>().SetBool("IsInfected", false);
				this.tag = "Human";
			}
			else if(collision.GetComponentInChildren<Disease>().stat != status.Infected) {
				collision.GetComponentInChildren<Disease>().stat = status.Infected;
				collision.GetComponentInChildren<Animator>().SetBool("IsInfected", true);
				collision.tag = "Furry";
			}
		}
	}
}
