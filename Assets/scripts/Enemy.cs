using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private Vector3 screenCenter;
	public GameObject prefab;
	private GameObject enemyGo;
	public float Xmin;
	public float Xmax;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if (transform.position.x < Xmin + 0.1) {
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 180));
		} else if(transform.position.x > Xmax - 0.1){
			transform.rotation = Quaternion.Euler (new Vector3(0, 0, 0));
		}

		transform.position =new Vector3(Mathf.PingPong(Time.time*2,Xmax-Xmin)+Xmin, transform.position.y, transform.position.z);
	}
}
