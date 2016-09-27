using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveContainer {
	WaveChef WF = WaveChef.GetInstance();

	int wave_number;

	GameObject arrivelPlanet;

	WaveContainer previousWave;

	float wave_start_pos_y;

	List<GameObject> gameObjects = new List<GameObject> ();

	public WaveContainer(int wave_number){
		this.wave_number = wave_number;

		previousWave = WF.getPreviousWave ();

		setWaveStartingPoint ();
		initWaveObjects ();
	}

	public int getWaveNumber(){
		return wave_number;
	}

	public Vector2 getStartPos(){
		return arrivelPlanet.transform.position;
	}

	public void remove(){
		WF.destroyGOs (gameObjects);
	}

	public void setWaveStartingPoint(){
		//if null first wave
		if(previousWave == null){
			wave_start_pos_y = 0;
		}else{
			//DO SOME STUFF TO CHANGE DISTANCE

			//Vector3 screenToWorld = Camera.main.ScreenToWorldPoint (new Vector3(0,0,0));
			//Debug.Log (screenToWorld.y);

			//Vector2 topLeftCorner = new Vector2(0, 0);
			//Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topLeftCorner);

			//Vector2 pos = new Vector2 (0, screenToWorld.y + Camera.main.orthographicSize);

			float max_distance = Camera.main.orthographicSize * 2f - Cam.cam_y_offset;
			float min_distance = 3f;

			//determine distance with wave count
			float distance = 0;
			if(wave_number > 0 && wave_number <= 5){
				distance = min_distance;
			}else if(wave_number > 5 && wave_number <= 10){
				distance = min_distance + 1f;
			}else if(wave_number > 10 && wave_number <= 15){
				distance = min_distance + 1f;
			}else if(wave_number > 15 && wave_number <= 20){
				distance = max_distance;
			}

			float final_distance = Mathf.Clamp (distance, min_distance, max_distance);

			wave_start_pos_y = WF.getPreviousWave().getStartPos().y + final_distance;
		}
	}

	public void initWaveObjects(){
		arrivelPlanet = WF.instantiateGo (WF.getOrbitPlanetPrefab(), new Vector2(0, wave_start_pos_y));
		gameObjects.Add (arrivelPlanet);
	}
}