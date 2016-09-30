using UnityEngine;
using System.Collections;

public class OrbitGroup : MonoBehaviour {
	private bool hasConnected = false;

	private DistanceJoint2D planetConstraint;
	private Rigidbody2D playerRB;

	public GameObject prefab_glow;

	private GameObject glow_go;

	private int waveNumber;

	bool isGoingLeft = false;

	//set all randomized properties, will be called before Start()
	public void init(int waveNumber, float pos_x, float glow_scale){
		this.waveNumber = waveNumber;

		planetConstraint = GetComponent<DistanceJoint2D> ();
		playerRB = Game.GetInstance ().playerGo.GetComponent<Rigidbody2D> ();

		//set randomized values
		transform.position = new Vector2(pos_x, transform.position.y);
		planetConstraint.distance = glow_scale;
	}

	// Use this for initialization
	void Start () {
		glow_go = (GameObject)Instantiate (prefab_glow, new Vector3 (transform.position.x, transform.position.y, 0), Quaternion.Euler (new Vector3 (0, 0, 1)));
		glow_go.transform.parent = transform;

		float scale = glow_go.transform.localScale.x - planetConstraint.distance;
		glow_go.transform.localScale = new Vector3 (planetConstraint.distance, planetConstraint.distance, 0);
	}
	
	// Update is called once per frame
	void Update () {
		//if this is current orbit planet then remove glow
		if(gameObject == Game.GetInstance ().currentOrbitGroup){
			Destroy (glow_go);
		}

		movement ();

		if(Game.GetInstance().currentState == Game.State.Orbit){
			
		}

		if(Game.GetInstance().currentState == Game.State.Flying){
			if (!hasConnected) {
				if (Helper.CheckIfNearby (gameObject, Game.GetInstance ().playerGo, planetConstraint.distance + .2f)) {
					hasConnected = true;

					if (Game.GetInstance ().playerGo.transform.position.x < gameObject.transform.position.x) {
						playerScript.orbit_clockwise = true;
					} else {
						playerScript.orbit_clockwise = false;
					}

					Game.GetInstance ().currentState = Game.State.Orbit;
					Game.GetInstance ().currentOrbitGroup = gameObject;

					linkToPlayer ();

					WaveChef.GetInstance ().createNextStage ();

					Game.GetInstance().determineAccuracy (gameObject);

					Googledatahandler.PlanetReachAchievement (WaveChef.GetInstance().getCurrentWave().getWaveNumber());
				}
			} else {
				destroyLink ();
			}
		}
	}

	void movement(){
		WaveContainer currentWave = WaveChef.GetInstance ().getCurrentWave ();

		if(currentWave != null){
			//only move planet when current planet is not rotating planet
			if(waveNumber != currentWave.getWaveNumber()){
				WaveContainer thisWave = WaveChef.GetInstance ().getWave (waveNumber);
				WaveContainer.OrbitGroupMovement orbitGroupMovement = thisWave.getOrbitGroupMovement ();

				Vector2 waveStartPoint = thisWave.getStartPos ();

				float groupOrbitMovementRadius = thisWave.getGroupOrbitMovementRadius ();
				float groupOrbitMovementSpeed = thisWave.getOrbitGroupMovementSpeed ();

				//get current position of current orbit position
				GameObject currentOrbitGroup = Game.GetInstance().currentOrbitGroup;

				//update movement of this planet
				if(orbitGroupMovement == WaveContainer.OrbitGroupMovement.x_axis){

					float distFromStart = transform.position.x - waveStartPoint.x;

					//if planet is out off screen relative to current orbit planet
					float min_x = currentOrbitGroup.transform.position.x - groupOrbitMovementRadius;
					float max_x = currentOrbitGroup.transform.position.x + groupOrbitMovementRadius;

					if (isGoingLeft) {
						if (distFromStart > groupOrbitMovementRadius || transform.position.x > max_x) {
							isGoingLeft = !isGoingLeft;
						}

						transform.Translate (groupOrbitMovementSpeed * Time.deltaTime, 0, 0);
					} else {
						if (distFromStart < -groupOrbitMovementRadius || transform.position.x < min_x) {
							isGoingLeft = !isGoingLeft;
						}

						transform.Translate (-groupOrbitMovementSpeed * Time.deltaTime, 0, 0);
					}
				}
			}
		}
	}

	void linkToPlayer(){
		planetConstraint.connectedBody = playerRB;
	}

	void destroyLink(){
		planetConstraint.connectedBody = null;
	}

	public int getWaveNumber(){
		return waveNumber;
	}
}