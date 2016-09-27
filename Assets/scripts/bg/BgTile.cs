using UnityEngine;
using System.Collections;

public class BgTile : MonoBehaviour {
	GameObject playerGO;

	float tileWidth;
	float tileHeight;

	// Use this for initialization
	void Start () {
		playerGO = Game.GetInstance ().playerGo;
		tileWidth = GetComponent<SpriteRenderer> ().bounds.size.x;
		tileHeight = GetComponent<SpriteRenderer> ().bounds.size.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerGO != null){
			Vector2 myTileCoos = new Vector2 (Mathf.Floor ((gameObject.transform.position.x + tileWidth/2) / tileWidth) , Mathf.Floor ((gameObject.transform.position.y + tileHeight/2) / tileHeight));
			Vector2 playerTileCoos = new Vector2 (Mathf.Floor ((playerGO.transform.position.x + tileWidth/2) / tileWidth) , Mathf.Floor ((playerGO.transform.position.y + tileHeight/2) / tileHeight));

			if(Mathf.Abs(myTileCoos.x -playerTileCoos.x) > 1
				|| Mathf.Abs(myTileCoos.y -playerTileCoos.y) > 1){

				Destroy (gameObject);
			}
		}
	}
}
