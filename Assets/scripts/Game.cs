using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	private static Game context;

	//PUBLIC GAMEOBJECTS
	public GameObject playerGo;
	//

	public enum State {Orbit, Flying, GameOver};
	public State currentState;

	//

	public GameObject currentOrbitPlanet;

	public static Game GetInstance(){
		return context;
	}

	void Awake(){
		context = this;

		currentState = State.Flying;

		WaveChef.GetInstance ().createNextStage ();
	}

	void Start(){
		
	}

	void Update(){
		
	}

	public void determineAccuracy(GameObject arrivalPlanet){
		Debug.Log ("DETERMINE ACCURACY HERE : perfect - moderate - weak");
	}
}