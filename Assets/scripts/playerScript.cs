using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

	public GameObject planet;
	public Rigidbody2D playerRB;


	// Use this for initialization
	void Start () {
		playerRB = Game.GetInstance ().playerGo.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0)){
			Game.GetInstance ().currentState = Game.State.Flying;
		}
		if (Game.GetInstance ().currentState == Game.State.Flying) {
			playerRB.freezeRotation = true;
		}
	}
}
