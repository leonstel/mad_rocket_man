using UnityEngine;
using System.Collections;

public class OrbitGroup : MonoBehaviour {

	private bool hasConnected = false;

	private DistanceJoint2D planetConstraint;
	private Rigidbody2D playerRB;

	public GameObject glow;

	// Use this for initialization
	void Start () {
		planetConstraint = GetComponent<DistanceJoint2D> ();
		playerRB = Game.GetInstance ().playerGo.GetComponent<Rigidbody2D> ();

		GameObject glow_go = (GameObject)Instantiate (glow, new Vector3 (transform.position.x, transform.position.y, 1), Quaternion.Euler (new Vector3 (0, 0, 1)));

		float scale = glow_go.transform.localScale.x - planetConstraint.distance;
		glow_go.transform.localScale = new Vector3 (planetConstraint.distance, planetConstraint.distance, 0);

		transform.localScale = new Vector2 (.5f,.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if(Game.GetInstance().currentState == Game.State.Orbit){
			
		}

		if(Game.GetInstance().currentState == Game.State.Flying){
			if (!hasConnected) {
				if (Helper.CheckIfNearby (gameObject, Game.GetInstance ().playerGo, planetConstraint.distance + .2f)) {
					hasConnected = true;

					Game.GetInstance().determineAccuracy (gameObject);

					if (Game.GetInstance ().playerGo.transform.position.x < gameObject.transform.position.x) {
						Helper.LookAt90 (Game.GetInstance().playerGo.gameObject, gameObject);
						playerScript.orbit_clockwise = true;
					} else {
						Helper.LookAt270(Game.GetInstance().playerGo.gameObject, gameObject);
						playerScript.orbit_clockwise = false;
					}

					Game.GetInstance ().currentState = Game.State.Orbit;
					Game.GetInstance ().currentOrbitPlanet = gameObject;

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
}