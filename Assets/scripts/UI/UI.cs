using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UI : MonoBehaviour {
	private static UI context;

	private Text UI_accuracy;

	public static UI GetInstance(){
		return context;
	}

	void Awake(){
		context = this;
	}

	void Start(){
		UI_accuracy = GameObject.FindGameObjectWithTag ("UI_accuracy").GetComponent<Text>();
	}

	void Update(){

		if (Game.GetInstance ().currentState == Game.State.GameOver) {
			StartCoroutine(showGameOver());

		}
	}

	IEnumerator showGameOver(){
		yield return new WaitForSeconds(.5f);
		UIManager.showGameOver ();
	}

	public void setAccuracy(string accuracy){
		UI_accuracy.text = accuracy;
	}
}