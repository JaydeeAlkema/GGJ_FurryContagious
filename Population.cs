using UnityEngine;
using UnityEngine.UI;

// ********************************
//	Copyright 2018 @Jaydee Alkema
//	dev.jaydeealkema@gmail.com
// ********************************

public class Population : MonoBehaviour {

	public Text totalFurriesText;       // UI Text Ellement for TotalFurries
	public Text totalHumansText;        // UI Text Ellement for TotalHumans
	public Text highestInfectedText;	// UI Text Ellement for highest amount of people infected
	public int totalFurriesCounter;     // The counter that keeps count if the TotalFurries
	public int totalHumansCounter;      // The counter that keeps count if the TotalHumans
	private int highScore;

	[Space]

	public Sprite[] furryMeterSprites;	// all the images used to animate the Population UI 
	public Image furryMeterImageComp;	// This gets the sprite component from the UI image
	[Range(0, 4)]
	public int furryMeterIndex;			// The current image displayed from the array

	private GameObject[] tF;            // Keeps count of the TotalFurries (tF = TotalFurries)
	private GameObject[] tH;            // Keeps count of the TotalHumans (tH = TotalHumans)

	private void Start() {
		furryMeterImageComp.GetComponent<Image>();
	}

	// Update is called once per frame
	void Update() {
		tF = GameObject.FindGameObjectsWithTag("Furry");
		tH = GameObject.FindGameObjectsWithTag("Human");
		furryMeterImageComp.sprite = furryMeterSprites[furryMeterIndex];

		totalFurriesCounter = tF.Length;
		totalHumansCounter = tH.Length;

		totalFurriesText.text = totalFurriesCounter.ToString();
		totalHumansText.text = totalHumansCounter.ToString();
		highestInfectedText.text = highScore.ToString();

		transitionPopulationSprite();

		highScore = Mathf.Max(totalFurriesCounter);
		Debug.Log(highScore);
	}

	void transitionPopulationSprite() {
		if(totalHumansCounter <= 215) {
			furryMeterIndex = 0;
		}
		if(totalHumansCounter <= 172) {
			furryMeterIndex = 1;
		}
		if(totalHumansCounter <= 129) {
			furryMeterIndex = 2;
		}
		if(totalHumansCounter <= 86) {
			furryMeterIndex = 3;
		}
		if(totalHumansCounter <= 0) {
			furryMeterIndex = 4;
		}

	}
}
