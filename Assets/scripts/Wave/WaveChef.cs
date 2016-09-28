using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WaveChef : MonoBehaviour {
	private static WaveChef context;

	//PUBLIC GAMEOBJECTS
	public GameObject prefab_orbitGroup;

	//

	public int currentWaveInt = 1;

	private WaveContainer previous_wave;
	private WaveContainer next_wave;

	private List<WaveContainer> waves = new List<WaveContainer>();

	//

	public static WaveChef GetInstance(){
		return context;
	}

	void Awake(){
		context = this;
	
	}

	void Update(){

	}

	void removePreviousWave(){
		foreach(WaveContainer wave in waves){
			//get previous wave, the one that the user has succeeded in
			if ((currentWaveInt - 2) == wave.getWaveNumber ()) {
				wave.remove ();
			}
		}
	}

	public void createNextStage (){
		//DESTROY currentWave
		removePreviousWave();

		if (previous_wave == null) {
			previous_wave = new WaveContainer (currentWaveInt);
			waves.Add (previous_wave);
		} else {
			previous_wave = next_wave;
		}

		next_wave = new WaveContainer (currentWaveInt + 1);
		waves.Add (next_wave);

		currentWaveInt++;
	}

	public GameObject instantiateGo(GameObject prefab, Vector2 pos){
		return (GameObject)Instantiate (prefab, new Vector2 (pos.x, pos.y), Quaternion.Euler (new Vector3 (0, 0, 1)));
	}

	public void destroyGOs(List<GameObject> GOs){
		foreach(GameObject go in GOs){
			Destroy(go);
		}
	}

	//GETTERS
	public WaveContainer getPreviousWave(){
		return previous_wave;
	}

	public GameObject getOrbitGroupPrefab(){
		return prefab_orbitGroup;
	}

	public WaveContainer getCurrentWave(){
		if(Game.GetInstance().currentOrbitGroup != null){
			int currentWaveNumber = Game.GetInstance().currentOrbitGroup.GetComponent<OrbitGroup> ().getWaveNumber ();

			foreach(WaveContainer waveContainer in waves){
				if(currentWaveNumber == waveContainer.getWaveNumber()){
					return waveContainer;
				}
			}
		}
		return null;
	}

	public WaveContainer getWave(int waveNumber){
		foreach(WaveContainer waveContainer in waves){
			if(waveNumber == waveContainer.getWaveNumber()){
				return waveContainer;
			}
		}
		return null;
	}
}