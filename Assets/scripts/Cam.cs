using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {
	private Vector3 offset; 

	// Use this for initialization
	void Start () {
		offset = transform.position - Game.GetInstance().currentOrbitPlanet.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = Vector3.Lerp (new Vector3 (transform.position.x, transform.position.y, transform.position.z), new Vector3 (Game.GetInstance().currentOrbitPlanet.transform.position.x, Game.GetInstance().currentOrbitPlanet.transform.position.y + 3f, Game.GetInstance().currentOrbitPlanet.transform.position.z + offset.z), 5f * Time.deltaTime);
		transform.position = Vector3.Lerp (new Vector3 (transform.position.x, transform.position.y, transform.position.z), new Vector3 (Game.GetInstance().currentOrbitPlanet.transform.position.x, Game.GetInstance().currentOrbitPlanet.transform.position.y + 3f, Game.GetInstance().currentOrbitPlanet.transform.position.z + offset.z), 5f * Time.deltaTime);
	}
}
