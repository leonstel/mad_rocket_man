using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	
	private static Game context;
	public GameObject playerGo;

	public enum State {Orbit, Flying, GameOver};
	public State currentState;

	public static Game GetInstance(){
		return context;
	}



	void Start(){
		context = this;
		currentState = State.Orbit;
	}

	void Update(){
		
	}
}