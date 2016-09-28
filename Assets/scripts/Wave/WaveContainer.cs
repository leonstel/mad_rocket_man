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

	private float orbitForce = 0f;
	private float spinningSpeed = 0f;

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
		setOrbitProps ();
		initWaveObjects ();
	}

	public int getWaveNumber(){
		return wave_number;
	}

	public Vector2 getStartPos(){
		return orbitGroup.transform.position;
	}

	public OrbitMovement getOrbitMovement(){
		return orbitMovement;
	}

	public float getOrbitForce(){
		return orbitForce;
	}

	public float getSpinningSpeed(){
		return spinningSpeed;
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
			float min_distance = 4f;

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

	public void setOrbitProps(){
		float orbitForce_random = Random.Range (10f,30f);
		orbitForce = orbitForce_random;

		float spinningSpeed_random = Random.Range (2f,6f);
		spinningSpeed = spinningSpeed_random;

		float OM_random = Random.Range (0,10);

		if(OM_random < 5){
			orbitMovement = OrbitMovement.steady;
		}else{
			orbitMovement = OrbitMovement.spinning;
		}
	}

	public void initWaveObjects(){
		orbitGroup = WF.instantiateGo (WF.getOrbitGroupPrefab(), new Vector2(0, wave_start_pos_y));

		float glowScale_random = Random.Range (1f,2f);

		Debug.Log (glowScale_random);

		orbitGroup.GetComponent<OrbitGroup> ().init (getWaveNumber(), glowScale_random);

		gameObjects.Add (orbitGroup);
	}
}