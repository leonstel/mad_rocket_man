using UnityEngine;
using System.Collections;

public class Offlinedatahandler : MonoBehaviour {
	public static int oldScore;
	public static int newScore;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public static void UpdateHighScore(int newScore){
		if(PlayerPrefs.HasKey("HScore")){
			if(PlayerPrefs.GetInt("HScore")<newScore){ 
				// new score is higher than the stored score
				oldScore = PlayerPrefs.GetInt("HScore");
				PlayerPrefs.SetInt("HScore",newScore);
				newScore = oldScore;
			}
		}else{
			PlayerPrefs.SetInt("HScore",newScore);
			newScore = 0;
		}
	}
}
