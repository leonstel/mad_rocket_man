using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveContainer {
	WaveChef WF = WaveChef.GetInstance();

	int wave_number;

	private Vector2 startPos;

	GameObject orbitGroup;

	WaveContainer previousWave;

	float wave_start_pos_y;

	List<GameObject> gameObjects = new List<GameObject> ();

	private float orbitForce = 0f;
	private float spinningSpeed = 0f;
	public static float launchForce = 40f;

	public enum PlayerInOrbitMovement
	{
		steady,
		spinning
	};

	private PlayerInOrbitMovement playerInOrbitMovement = PlayerInOrbitMovement.steady;

	public enum OrbitGroupMovement
	{
		steady,
		x_axis
	};

	private float orbitGroupMovementRadius = 0f;
	private float orbitGroupMovementSpeed = 0f;

	private OrbitGroupMovement orbitGroupMovement = OrbitGroupMovement.steady;

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
		return startPos;
	}

	public PlayerInOrbitMovement getPlayerInOrbitMovement(){
		return playerInOrbitMovement;
	}

	public OrbitGroupMovement getOrbitGroupMovement(){
		return orbitGroupMovement;
	}

	public float getOrbitForce(){
		return orbitForce;
	}

	public float getSpinningSpeed(){
		return spinningSpeed;
	}

	public float getGroupOrbitMovementRadius(){
		return orbitGroupMovementRadius;
	}

	public float getOrbitGroupMovementSpeed(){
		return orbitGroupMovementSpeed;
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

			wave_start_pos_y = previousWave.getStartPos().y + final_distance;
		}
	}

	public void setOrbitProps(){
		//PLAYER ORBIT FORCE
			float orbitForce_random = Random.Range (10f,30f);
			orbitForce = orbitForce_random;

			float spinningSpeed_random = Random.Range (2f,6f);
			spinningSpeed = spinningSpeed_random;

			float PIOM_random = Random.Range (0,10);
			if(PIOM_random < 5){
				playerInOrbitMovement = PlayerInOrbitMovement.steady;
			}else{
				playerInOrbitMovement = PlayerInOrbitMovement.spinning;
			}

		//ORBIT GROUP MOVEMENT
			orbitGroupMovementSpeed = .3f;
			orbitGroupMovementRadius = 2f;

			float OGM_random = Random.Range (0,10);
			if(OGM_random < 5){
				orbitGroupMovement = OrbitGroupMovement.steady;
			}else{
				orbitGroupMovement = OrbitGroupMovement.x_axis;
			}
	}

	public void initWaveObjects(){
		orbitGroup = WF.instantiateGo (WF.getOrbitGroupPrefab (), new Vector2 (0, wave_start_pos_y));

		//float posX_random = Random.Range (-1f, 1f);
		float posX_random = 0;
		if (previousWave != null) {
			float previousStartX = previousWave.getStartPos ().x;
			float orthoWidth = Camera.main.orthographicSize - 3f;
			float leftBounds = previousStartX - orthoWidth;
			posX_random = Random.Range(leftBounds, leftBounds + (orthoWidth * 2f));
		}

		float glowScale_random = Random.Range (1f, 2f);

		orbitGroup.GetComponent<OrbitGroup> ().init (getWaveNumber (), posX_random, glowScale_random);

		//set first start pos
		startPos = new Vector2 (posX_random, wave_start_pos_y);

		//add to list with alll gameobejct of this wave
		gameObjects.Add (orbitGroup);
	}
}