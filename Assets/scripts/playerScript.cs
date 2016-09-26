using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {
	private Rigidbody2D playerRB;

	// Use this for initialization
	void Start () {
		playerRB = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0)){
			Game.GetInstance ().currentState = Game.State.Flying;
		}

		if(Game.GetInstance ().currentState == Game.State.Orbit){
			playerRB.freezeRotation = false;
		}else if (Game.GetInstance ().currentState == Game.State.Flying) {
			playerRB.freezeRotation = true;
		}
	}
}