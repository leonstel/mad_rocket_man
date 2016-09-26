using UnityEngine;
using System.Collections;

public class planet : MonoBehaviour {
	
	private DistanceJoint2D planetConstraint;
	private Rigidbody2D playerRB;

	// Use this for initialization
	void Start () {
		planetConstraint = GetComponent<DistanceJoint2D> ();
		playerRB = Game.GetInstance ().playerGo.GetComponent<Rigidbody2D> ();

		planetConstraint.connectedBody = playerRB;
	}
	
	// Update is called once per frame
	void Update () {
		if (Game.GetInstance ().currentState != Game.State.Orbit){
			planetConstraint.connectedBody = null;
		} 
	}


}


