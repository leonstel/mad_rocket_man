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

			if(currentWave != null){
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

		if(!willHitPlanet ()){
			if(playerToScreen.x < 0 || playerToScreen.x > Screen.width || playerToScreen.y > Screen.height || playerToScreen.y < -150){
				die ();
			}
		}
	}

	bool willHitPlanet(){
		WaveContainer nextWave = WaveChef.GetInstance ().getNextWave ();

		if(nextWave != null){
			WaveContainer nextAfterWave = WaveChef.GetInstance ().getNextAfterWave ();

			//RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 2), Vector2.up, Mathf.Infinity, LayerMask.NameToLayer("orbitgroups"));
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 2), gameObject.transform.up);
			if (hit.collider != null) {
				//float distance = Mathf.Abs(hit.point.y - transform.position.y);
				//float heightError = floatHeight - distance;
				//float force = liftForce * heightError - rb2D.velocity.y * damping;
				//rb2D.AddForce(Vector3.up * force);
				GameObject hitGO = hit.collider.gameObject;

				if (hitGO == nextWave.getOrbitGroup ()) {
					return true;
				}

				if(hitGO == nextAfterWave.getOrbitGroup()){
					return true;
				}
			}
		}

		return false;
	}

	void die(){
		Game.GetInstance ().currentState = Game.State.GameOver;

		Destroy (gameObject);
		ExplosionWorker.GetInstance ().explode (gameObject.transform.position);

		WaveContainer currentWave = WaveChef.GetInstance ().getCurrentWave ();

		int waveNumber = currentWave.getWaveNumber ();

		Googledatahandler.RegisterDeath (waveNumber);
		UIManager.setDistanceScore (waveNumber);
	}
}