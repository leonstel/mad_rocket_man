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

	public GameObject currentOrbitGroup;

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
		WaveContainer currentWave = WaveChef.GetInstance ().getCurrentWave ();

		if(currentWave != null){
			if(currentWave.getWaveNumber() > 0){
				float z_euler_angle = playerGo.transform.localRotation.eulerAngles.z;

				float angle_alpha = (90 + z_euler_angle) % 360;

				Vector2 arrivalPlanetPos = arrivalPlanet.transform.position;
				Vector2 playerPos = playerGo.transform.position;

				float calculatedAngle = 0;
				if (arrivalPlanetPos.x == playerPos.x) {
					calculatedAngle = 90;
				} else {
					calculatedAngle = (Mathf.Atan ((arrivalPlanetPos.y - playerPos.y) / (arrivalPlanetPos.x - playerPos.x))) * Mathf.Rad2Deg;
				}

				float accuracyAngle = 0;
				if (calculatedAngle < 0) {
					accuracyAngle = Mathf.Abs( angle_alpha - calculatedAngle - 180 );
				} else {
					accuracyAngle = Mathf.Abs( angle_alpha - calculatedAngle );
				}

				string accuracy = "";
				//determine acceracy
				if(accuracyAngle > 0 && accuracyAngle <= 25){
					accuracy = "GOOD";
				}else if(accuracyAngle > 25 && accuracyAngle <= 50){
					accuracy = "GREAT";
				}else if(accuracyAngle > 50){
					accuracy = "PERFECT";
				}

				UI.GetInstance ().setAccuracy (accuracy);
			}
		}
	}
}