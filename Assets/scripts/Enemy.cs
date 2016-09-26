using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IWayPointListener {

	private WaypointWalker enemyWalker;

	void Start () {
		enemyWalker = new WaypointWalker (this, "enemy");
		enemyWalker.CreateWayPoint (new Vector2(1,1), 18);
		enemyWalker.CreateWayPoint (new Vector2(10,10), 18);
	}

	void Update () {
		enemyWalker.Update ();
	}

	void IWayPointListener.PointReached (int i, string walkerName){
		
	}

	GameObject IWayPointListener.getGameObject(){
		//return ship instead of this.destinationplanet
		return gameObject;
	}

	public virtual void LastPointReached (string walkerName){

	}
}
