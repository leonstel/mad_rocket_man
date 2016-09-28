using UnityEngine;
using System.Collections;

public class OrbitGroup : MonoBehaviour {
	private bool hasConnected = false;

	private DistanceJoint2D planetConstraint;
	private Rigidbody2D playerRB;

	public GameObject prefab_glow;

	private GameObject glow_go;

	private int waveNumber;


	//set all randomized properties, will be called before Start()
	public void init(int waveNumber, float glow_scale){
		this.waveNumber = waveNumber;

		planetConstraint = GetComponent<DistanceJoint2D> ();
		playerRB = Game.GetInstance ().playerGo.GetComponent<Rigidbody2D> ();

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

		if(Game.GetInstance().currentState == Game.State.Orbit){
			
		}

		if(Game.GetInstance().currentState == Game.State.Flying){
			if (!hasConnected) {
				if (Helper.CheckIfNearby (gameObject, Game.GetInstance ().playerGo, planetConstraint.distance + .2f)) {
					hasConnected = true;

					Game.GetInstance().determineAccuracy (gameObject);

					if (Game.GetInstance ().playerGo.transform.position.x < gameObject.transform.position.x) {
						playerScript.orbit_clockwise = true;
					} else {
						playerScript.orbit_clockwise = false;
					}

					Game.GetInstance ().currentState = Game.State.Orbit;
					Game.GetInstance ().currentOrbitGroup = gameObject;

					linkToPlayer ();

					WaveChef.GetInstance ().createNextStage ();
				}
			} else {
				destroyLink ();
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