using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointWalker {
	List<WayPoint> waypoints = new List<WayPoint>();

	private int currentIndex = 0;
	private WayPoint previousWayPoint = null;

	IWayPointListener listener;

	public string name = "";

	public WaypointWalker(IWayPointListener listener, string name){
		this.name = name;
		this.listener = listener;
	}

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	public void Update () {
		if(waypoints.Count > 0){
			WayPoint currentWayPoint = waypoints [currentIndex];

			Vector2 destinationPos = currentWayPoint.getDestination();
			Vector2 arrivalPos = listener.getGameObject ().transform.position;

			Vector2 dir = destinationPos - arrivalPos;

			if (dir.magnitude > 1) {
				Debug.Log ("TEST");

				dir = dir.normalized * currentWayPoint.getSpeed();

				listener.getGameObject ().GetComponent<Rigidbody2D>().velocity = dir;
			} else {
				if (currentIndex+1 >= waypoints.Count) {
					Debug.Log ("lastpoint reached");
					listener.LastPointReached (name);
				} else {
					Debug.Log ("PointReached");
					listener.PointReached (currentIndex, name);
					currentIndex++;
				}
			}
		}
	}

	public void CreateWayPoint(Vector2 destination, int speed){
		WayPoint waypoint = new WayPoint (destination, speed);
		waypoints.Add (waypoint);
	}

	class WayPoint{

		private Vector2 destination;
		private int speed;
		public WayPoint(Vector2 destination, int speed){
			this.destination = destination;
			this.speed = speed;
		}

		public Vector2 getDestination(){
			return destination;
		}

		public int getSpeed(){
			return speed;
		}
	}
}
