using UnityEngine;
using System.Collections;


public class Satelite : MonoBehaviour {

	public int RotationTime;

	// Use this for initialization
	void Start () {
		if (RotationTime == 0){
			RotationTime = 4;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward * RotationTime);
	}
}
