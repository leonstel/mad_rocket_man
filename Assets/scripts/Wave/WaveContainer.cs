using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveContainer {
	WaveChef WF = WaveChef.GetInstance();

	int wave_number;

	GameObject orbitGroup;

	WaveContainer previousWave;

	float wave_start_pos_y;

	List<GameObject> gameObjects = new List<GameObject> ();

	public enum OrbitMovement
	{
		steady,
		spinning
	};

	private OrbitMovement orbitMovement = OrbitMovement.steady;

	public WaveContainer(int wave_number){
		this.wave_number = wave_number;

		previousWave = WF.getPreviousWave ();

		setWaveStartingPoint ();
		setOrbitMovement ();
		initWaveObjects ();
	}

	public int getWaveNumber(){
		return wave_number;
	}

	public Vector2 getStartPos(){
		return orbitGroup.transform.position;
	}

	public OrbitMovement getOrbitForce(){
		return orbitMovement;
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

	public void setOrbitMovement(){
		float OM_random = Random.Range (0,10);

		if(OM_random < 5){
			orbitMovement = OrbitMovement.steady;
		}else{
			orbitMovement = OrbitMovement.spinning;
		}
	}

	public void initWaveObjects(){
		orbitGroup = WF.instantiateGo (WF.getOrbitGroupPrefab(), new Vector2(0, wave_start_pos_y));

		orbitGroup.GetComponent<OrbitGroup> ().init (getWaveNumber());

		gameObjects.Add (orbitGroup);
	}
}