using UnityEngine;
using System.Collections;

public class OrbitPlanet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float scale = .5f;
		transform.localScale = new Vector3 (scale,scale,0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
