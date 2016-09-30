using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlanetCount : MonoBehaviour {
	public Texture2D[] number_textures;

	float canvasWidth;
	float canvasHeight;

	int previousWaveNumber;
	int currentWaveNumber;

	List<GameObject> waveNumbers = new List<GameObject> ();

	// Use this for initialization
	void Start () {
		RectTransform canvas = transform.parent.gameObject.GetComponent<RectTransform> ();
		canvasWidth = canvas.rect.width;
		canvasHeight = canvas.rect.height;
	}
	
	// Update is called once per frame
	void Update () {
		WaveContainer currentWave = WaveChef.GetInstance ().getCurrentWave ();

		if(currentWave != null){
			currentWaveNumber = currentWave.getWaveNumber ();
			if(currentWaveNumber != previousWaveNumber){
				clearNumbers ();

				String waveNumberStr = currentWaveNumber.ToString();
				int[] parsedInts = new int[waveNumberStr.Length];

				for(int i = 0; i<parsedInts.Length; i++){
					parsedInts [i] = Int32.Parse(""+waveNumberStr[i]);

					createNumber (parsedInts [i], i, parsedInts.Length);
				}

				previousWaveNumber = currentWaveNumber;
			}
		}
	}

	void createNumber(int number, int column, int columnsLength){
		GameObject newObj = new GameObject ();
		newObj.transform.localScale = new Vector3 (2,2,0);

		Image newImage = newObj.AddComponent<Image>();

		Texture2D numberTexture = number_textures [number];

		newImage.sprite = Sprite.Create(numberTexture,new Rect(0, 0, numberTexture.width, numberTexture.height),new Vector2(0.5f,0.5f));

		float number_offset = 0;
		if (columnsLength == 2) {
			number_offset = 60f;
		} else if(columnsLength == 2) {
			number_offset = 110f;
		}
		newObj.transform.position = new Vector2 (canvasWidth / 2 - number_offset + (column * 90f ), canvasHeight * .8f);

		newObj.transform.parent = transform;

		waveNumbers.Add (newObj);
	}

	void clearNumbers(){
		foreach(GameObject number in waveNumbers){
			Destroy (number);
		}

		waveNumbers.Clear ();
	}
}
