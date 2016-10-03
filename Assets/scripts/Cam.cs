using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {
	private Vector3 offset; 

	public static float cam_y_offset = 3.2f;

	private Vector3 originPosition;
	private Quaternion originRotation;
	public float shake_decay;
	public float shake_intensity;

	private bool isShaking = false;

	// Use this for initialization
	void Start () {
		offset = transform.position - Game.GetInstance().playerGo.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		checkShake ();

		if(!isShaking){
			if(Game.GetInstance().currentOrbitGroup != null){
				transform.position = Vector3.Lerp (new Vector3 (transform.position.x, transform.position.y, transform.position.z), new Vector3 (Game.GetInstance().currentOrbitGroup.transform.position.x, Game.GetInstance().currentOrbitGroup.transform.position.y + cam_y_offset, Game.GetInstance().currentOrbitGroup.transform.position.z + offset.z), 5f * Time.deltaTime);
				//transform.position = new Vector3 (Game.GetInstance().currentOrbitPlanet.transform.position.x, Game.GetInstance().currentOrbitPlanet.transform.position.y + 3f, Game.GetInstance().currentOrbitPlanet.transform.position.z + offset.z	);
			}
		}
	}

	void checkShake(){
		if (shake_intensity > 0) {
			isShaking = true;
			transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
			//transform.rotation = new Quaternion(
			//	originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,
			//	originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .2f,
			//	originRotation.z + Random.Range (-shake_intensity,shake_intensity) * .2f,
			//	originRotation.w + Random.Range (-shake_intensity,shake_intensity) * .2f);
			shake_intensity -= shake_decay;
		} else {
			isShaking = false;
		}
	}

	public void shake(){
		originPosition = transform.position;
		originRotation = transform.rotation;
		shake_intensity = .25f;
		shake_decay = 0.05f;
	}
}