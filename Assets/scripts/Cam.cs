using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {
	private Vector3 offset; 

	public static float cam_y_offset = 3.2f;

	// Use this for initialization
	void Start () {
		offset = transform.position - Game.GetInstance().playerGo.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Game.GetInstance().currentOrbitPlanet != null){
			transform.position = Vector3.Lerp (new Vector3 (transform.position.x, transform.position.y, transform.position.z), new Vector3 (Game.GetInstance().currentOrbitPlanet.transform.position.x, Game.GetInstance().currentOrbitPlanet.transform.position.y + cam_y_offset, Game.GetInstance().currentOrbitPlanet.transform.position.z + offset.z), 5f * Time.deltaTime);
			//transform.position = new Vector3 (Game.GetInstance().currentOrbitPlanet.transform.position.x, Game.GetInstance().currentOrbitPlanet.transform.position.y + 3f, Game.GetInstance().currentOrbitPlanet.transform.position.z + offset.z	);
		}
	}
}
