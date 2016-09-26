using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	private static Game context;

	//PUBLIC GAMEOBJECTS
	public GameObject playerGo;
	public GameObject prefab_orbitPlanet;

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

		setFirstOrbitPlanet ();
	}

	void Update(){
		
	}

	void setFirstOrbitPlanet (){
		GameObject orbitPlanet = (GameObject)Instantiate (prefab_orbitPlanet, new Vector2(0,0), Quaternion.Euler( new Vector3(0,0,1)));
		currentOrbitPlanet = orbitPlanet;
	}
}