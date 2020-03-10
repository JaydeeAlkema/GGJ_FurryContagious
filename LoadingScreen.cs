using UnityEngine;
using UnityEngine.UI;

// ********************************
//	Copyright 2018 @Jaydee Alkema
//	dev.jaydeealkema@gmail.com
// ********************************

public class LoadingScreen : MonoBehaviour {
	
	public string[] quotes;

	private Image BG;
	public GameObject loadingThing;
	private Text randomLoadingQuotes;
	private Color BGAlphaDecreaser;

	private float timeLeftToLoadGame = 10.0f;
	private float timeToGetNewQuote = 3.33f;
	private int randomQuoteIndex;
		 
	// Use this for initialization
	void Start () {
		randomizeQuotes();
		randomLoadingQuotes = this.GetComponentInChildren<Text>();
		BG = this.GetComponentInChildren<Image>();
		BGAlphaDecreaser = new Color(0.0f,0.0f,0.0f,0.01f);
	}
	
	// Update is called once per frame
	void Update () {
		timeLeftToLoadGame -= Time.deltaTime;
		timeToGetNewQuote -= Time.deltaTime;
		if(timeToGetNewQuote <= 0.0f) {
			randomizeQuotes();
		}
		randomLoadingQuotes.text = quotes[randomQuoteIndex];
		if(timeLeftToLoadGame <= 0.0f) {
			BG.color -= BGAlphaDecreaser;
			loadingThing.GetComponent<Image>().color -= BGAlphaDecreaser;
			randomLoadingQuotes.enabled = false;
			if(BG.color.a <= 0.0f) {				
				Destroy(this.gameObject);
			}
		}
	}

	public void randomizeQuotes() {
		randomQuoteIndex = Random.Range(0, quotes.Length);
		timeToGetNewQuote = 3.33f;
	}
}
