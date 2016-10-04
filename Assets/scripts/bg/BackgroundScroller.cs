using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour {
	public GameObject tile;

	GameObject bg_scroller_container;
	GameObject playerGo;

	float tileHeight;
	float tileWidth;

	Vector2 previousTileCoos;
	// Use this for initialization
	void Start () {
		previousTileCoos = new Vector2 (0, -1);

		bg_scroller_container = (GameObject)GameObject.FindGameObjectWithTag ("bg_scroller_container");
		playerGo = (GameObject)GameObject.FindGameObjectWithTag ("Player");

		tileHeight = tile.GetComponent<SpriteRenderer> ().bounds.size.y;
		tileWidth = tile.GetComponent<SpriteRenderer> ().bounds.size.x;
	}

	// Update is called once per frame
	void Update () {
		//Y UP
		if(Game.GetInstance().currentState != Game.State.GameOver){
			Vector2 currentTileCoos = new Vector2 (Mathf.Floor ((playerGo.transform.position.x + tileWidth/2) / tileWidth) , Mathf.Floor ((playerGo.transform.position.y + tileHeight/2) / tileHeight));

			if(currentTileCoos.y > previousTileCoos.y){
				float newY = tileHeight * (currentTileCoos.y + 1);

				for(int i = -1; i <= 1; i++){
					float newX = tileWidth * (currentTileCoos.x + i);
					GameObject newTile = (GameObject)Instantiate(tile, new Vector3(newX, newY, 0), Quaternion.identity);
					newTile.transform.parent = bg_scroller_container.transform;
				}			
			}

			//X right
			if (currentTileCoos.x > previousTileCoos.x) {
				float newX = tileWidth * (currentTileCoos.x + 1);

				for(int i = 0; i <= 1; i++){
					float newY = tileHeight * (currentTileCoos.y + i);
					GameObject newTile = (GameObject)Instantiate(tile, new Vector3(newX, newY, 0), Quaternion.identity);
					newTile.transform.parent = bg_scroller_container.transform;
				}	
			}

			//X left
			if (currentTileCoos.x < previousTileCoos.x) {
				float newX = tileWidth * (currentTileCoos.x - 1);

				for(int i = 0; i <= 1; i++){
					float newY = tileHeight * (currentTileCoos.y + i);
					GameObject newTile = (GameObject)Instantiate(tile, new Vector3(newX, newY, 0), Quaternion.identity);
					newTile.transform.parent = bg_scroller_container.transform;
				}	
			}

			previousTileCoos = currentTileCoos;
		}
	}
}