using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Game.GetInstance().currentState == Game.State.Orbit){
			sr.enabled = false;
		}else if(Game.GetInstance().currentState == Game.State.Flying){
			sr.enabled = true;
		}
	}
}
