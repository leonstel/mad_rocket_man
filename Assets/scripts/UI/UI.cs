﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UI : MonoBehaviour {
	private static UI context;

	private Text UI_wave_text;

	public static UI GetInstance(){
		return context;
	}

	void Awake(){
		context = this;
	}

	void Start(){
		UI_wave_text = GameObject.FindGameObjectWithTag ("UI_wave_text").GetComponent<Text>();
	}

	void Update(){

		if (Game.GetInstance ().currentState == Game.State.GameOver) {
			StartCoroutine(showGameOver());

		} else {
			WaveContainer currentWave = WaveChef.GetInstance ().getCurrentWave ();
			if(currentWave != null){
				if(currentWave.getWaveNumber() > 0){
					int currentWaveNumber = currentWave.getWaveNumber ();
					UI_wave_text.text = currentWaveNumber.ToString();
				}
			}
		}
	}

	IEnumerator showGameOver(){
		yield return new WaitForSeconds(.5f);
		UIManager.showGameOver ();
	}
}