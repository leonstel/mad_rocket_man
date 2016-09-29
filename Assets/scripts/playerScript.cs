using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {
	Rigidbody2D playerRB;
	BoxCollider2D playerBC;
	ConstantForce2D constF;

	public static bool orbit_clockwise;

	// Use this for initialization
	void Start () {
		playerBC = GetComponent<BoxCollider2D> ();
		playerRB = GetComponent<Rigidbody2D> ();
		constF = GetComponent<ConstantForce2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Game.GetInstance ().currentState = Game.State.Flying;
		}

		if(Game.GetInstance ().currentState == Game.State.Orbit){
			playerRB.freezeRotation = false;

			WaveContainer currentWave = WaveChef.GetInstance ().getCurrentWave ();
			WaveContainer.PlayerInOrbitMovement playerInOrbitMovement = currentWave.getPlayerInOrbitMovement ();
			float orbitForce = currentWave.getOrbitForce ();
			float spinningSpeed = currentWave.getSpinningSpeed();
		
			//x = -f * (delta y / l)

			float x_force = 0;
			float y_force = 0;

			if (orbit_clockwise) {
				x_force = orbitForce * ( (gameObject.transform.position.y - Game.GetInstance().currentOrbitGroup.transform.position.y) / Vector2.Distance(gameObject.transform.position, Game.GetInstance().currentOrbitGroup.transform.position));
				y_force = -orbitForce * ( (gameObject.transform.position.x - Game.GetInstance().currentOrbitGroup.transform.position.x) / Vector2.Distance(gameObject.transform.position, Game.GetInstance().currentOrbitGroup.transform.position));
			} else {
				x_force = -orbitForce * ( (gameObject.transform.position.y - Game.GetInstance().currentOrbitGroup.transform.position.y) / Vector2.Distance(gameObject.transform.position, Game.GetInstance().currentOrbitGroup.transform.position));
				y_force = orbitForce * ( (gameObject.transform.position.x - Game.GetInstance().currentOrbitGroup.transform.position.x) / Vector2.Distance(gameObject.transform.position, Game.GetInstance().currentOrbitGroup.transform.position));
			}

			constF.force = new Vector2 (x_force, y_force);
			constF.relativeForce = new Vector2 (0,0); 

			if(playerInOrbitMovement == WaveContainer.PlayerInOrbitMovement.steady){
				if (orbit_clockwise) {
					Helper.LookAt90 (gameObject, Game.GetInstance().currentOrbitGroup);
				} else {
					Helper.LookAt270(gameObject, Game.GetInstance().currentOrbitGroup);
				}
			}else if(playerInOrbitMovement == WaveContainer.PlayerInOrbitMovement.spinning){

				if (orbit_clockwise) {
					transform.Rotate (new Vector3(0,0,transform.localRotation.z + spinningSpeed));
				} else {
					transform.Rotate (new Vector3(0,0,transform.localRotation.z - spinningSpeed));
				}
			}
		}else if (Game.GetInstance ().currentState == Game.State.Flying) {
			playerRB.freezeRotation = true;

			CheckIfOutOfBounds ();

			constF.force = Vector2.zero;
			constF.relativeForce = new Vector2 (0,WaveContainer.launchForce); 
		}
	}

	void CheckIfOutOfBounds(){
		Vector3 playerToScreen = Camera.main.WorldToScreenPoint (Game.GetInstance ().playerGo.transform.position);

		if(playerToScreen.x < 0 || playerToScreen.x > Screen.width || playerToScreen.y > Screen.height || playerToScreen.y < 0){
			die ();
		}
	}

	void die(){
		Game.GetInstance ().currentState = Game.State.GameOver;

		Destroy (gameObject);
		ExplosionWorker.GetInstance ().explode (gameObject.transform.position);

		WaveContainer currentWave = WaveChef.GetInstance ().getCurrentWave ();
		Googledatahandler.RegistrateDeath (currentWave.getWaveNumber());
	}
}