using UnityEngine;
using System.Collections;

public class planet : MonoBehaviour {

	private bool hasConnected = false;

	private DistanceJoint2D planetConstraint;
	private Rigidbody2D playerRB;

	// Use this for initialization
	void Start () {
		planetConstraint = GetComponent<DistanceJoint2D> ();
		playerRB = Game.GetInstance ().playerGo.GetComponent<Rigidbody2D> ();

		transform.localScale = new Vector2 (.5f,.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if(Game.GetInstance().currentState == Game.State.Orbit){
			
		}

		if(Game.GetInstance().currentState == Game.State.Flying){
			if (!hasConnected) {
				if (Helper.CheckIfNearby (gameObject, Game.GetInstance ().playerGo, 2.0f)) {
					hasConnected = true;

					if (Game.GetInstance ().playerGo.transform.position.x < gameObject.transform.position.x) {
						Helper.LookAt90 (Game.GetInstance().playerGo, gameObject);
					} else {
						Helper.LookAt270(Game.GetInstance().playerGo, gameObject);
					}

					Game.GetInstance ().currentState = Game.State.Orbit;

					linkToPlayer ();
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