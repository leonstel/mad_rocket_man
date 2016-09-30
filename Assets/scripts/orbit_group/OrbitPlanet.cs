using UnityEngine;
using System.Collections;

public class OrbitPlanet : MonoBehaviour {
	public Texture2D[] planetTextures;

	private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();

		Texture2D planetTexture = planetTextures [Random.Range (0, planetTextures.Length)];
		sr.sprite = Sprite.Create(planetTexture,new Rect(0, 0, planetTexture.width, planetTexture.height),new Vector2(0.5f,0.5f));

		float scale = .2f;
		transform.localScale = new Vector3 (scale,scale,0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
