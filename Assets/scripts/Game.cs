using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	private static Game context;

	//PUBLIC GAMEOBJECTS
	public GameObject playerGo;

	public GameObject[] accuracy_prefabs;
	public GameObject lineCutter_prefab;
	//

	public enum State {Orbit, Flying, GameOver};
	public State currentState;

	//
	public GameObject currentOrbitGroup;
	private int previousOrbitGroupWaveNumber;

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
		if(currentState != State.GameOver){
			lineCutterCheck ();
		}
	}
		
	bool hasAlreadyCutLine = false;
	public void lineCutterCheck(){
		if(!hasAlreadyCutLine){
			if(currentOrbitGroup != null){
				OrbitGroup currentOrbitGroupScript = currentOrbitGroup.GetComponent<OrbitGroup> ();

				int currentOrbitGroupWaveNumber = currentOrbitGroupScript.getWaveNumber ();

				if(currentOrbitGroupWaveNumber != previousOrbitGroupWaveNumber){
					hasAlreadyCutLine = true;

					if(currentOrbitGroupWaveNumber - previousOrbitGroupWaveNumber >= 2){
						Instantiate (lineCutter_prefab, new Vector2 (playerGo.transform.position.x, playerGo.transform.position.y - 1.2f), Quaternion.Euler (new Vector3 (0, 0, 1)));

						//when player has cut the line, create a new wave/stage on top
						WaveChef.GetInstance().createNextStage();
					}
				}
			}
		}
	}

	public void screenShake(){
		WaveContainer currentWave = WaveChef.GetInstance ().getCurrentWave ();
		if(currentWave != null){
			if(currentWave.getWaveNumber() > 0){
				Camera.main.GetComponent<Cam> ().shake ();
			}
		}
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

				GameObject accuracy_prefab = null;
				//determine acceracy
				if(accuracyAngle > 0 && accuracyAngle <= 25){
					accuracy_prefab = accuracy_prefabs [0];
				}else if(accuracyAngle > 25 && accuracyAngle <= 50){
					accuracy_prefab = accuracy_prefabs [1];
				}else if(accuracyAngle > 50){
					accuracy_prefab = accuracy_prefabs [2];
				}

				showAccuracy (accuracy_prefab);
			}
		}
	}

	void showAccuracy(GameObject accuracy_prefab){
		Instantiate (accuracy_prefab, new Vector2 (playerGo.transform.position.x, playerGo.transform.position.y + 1.2f), Quaternion.Euler (new Vector3 (0, 0, 1)));
	}

	public void setCurrentOrbitGroup(GameObject orbitGroup){
		hasAlreadyCutLine = false;

		if(currentOrbitGroup != null){
			previousOrbitGroupWaveNumber = currentOrbitGroup.GetComponent<OrbitGroup>().getWaveNumber();
		}
		currentOrbitGroup = orbitGroup;

		AudioChef.getInstance().playBackgroundMusic ();
	}
}