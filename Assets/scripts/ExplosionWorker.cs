using UnityEngine;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ExplosionWorker : MonoBehaviour {
	private static ExplosionWorker context;

	//PUBLIC GAMEOBJECTS
	public GameObject prefab_explosion;

	//

	public static ExplosionWorker GetInstance(){
		return context;
	}

	void Awake(){
		context = this;

	}

	void Update(){

	}

	public void explode(Vector2 pos){
		GameObject explosion_go = (GameObject)Instantiate(prefab_explosion, pos, prefab_explosion.transform.rotation);
		explosion_go.transform.localScale = new Vector3 (5,5,0);
	}
}