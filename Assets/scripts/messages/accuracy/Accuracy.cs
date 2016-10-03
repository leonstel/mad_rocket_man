﻿using UnityEngine;
using System.Collections;

public class Accuracy : MonoBehaviour {
	public float minimum = 0.0f;
	public float maximum = 1f;
	private float startTime;
	public SpriteRenderer sr;

	float maxScale = .25f;

	// Use this for initialization
	void Start () {
		startTime = Time.time;

		sr = GetComponent<SpriteRenderer> ();

		transform.localScale = new Vector3 (maxScale,maxScale,0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, 2 * Time.deltaTime, 0);

		float t = (Time.time - startTime);
		sr.color = new Color(1f,1f,1f,Mathf.SmoothStep(maximum, minimum, t));    


		t = (Time.time - startTime) * 3f;
		float newScale = Mathf.SmoothStep (maxScale, .2f, t);
		transform.localScale = new Vector3 (newScale, newScale, 0 );

		if(sr.color.a <= 0){
			Destroy (gameObject);
		}
	}
}