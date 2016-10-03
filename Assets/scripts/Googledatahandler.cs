using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;


public class Googledatahandler : MonoBehaviour {
	public static int newScore;
	public static int oldScore;
	// Use this for initialization


	// Update is called once per frame
	void Update () {
		
	}
	public static void UploadHighScore(){
		Social.ReportScore(PlayerPrefs.GetInt("HScore"), "CgkIs-r3kO4CEAIQAg", (bool success) => {
			// handle success or failure
		});
	}

	public static void RegisterHighScore(int planetcount){
	newScore = planetcount;
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
		Social.ReportScore(planetcount, "CgkIs-r3kO4CEAIQAg", (bool success) => {
			// handle success or failure
		});
				
	}
	private static void DeathAchievement(int planetcount){
		if (planetcount >= 50) {
			Social.ReportProgress ("CgkIs-r3kO4CEAIQCw", 100.0f, (bool success) => {
			});
		}
	}

	public static void RegisterDeath(int planetcount){
		RegisterHighScore (planetcount);
		DeathAchievement(planetcount);
	}

	public void checkScene(){
		if (Application.loadedLevel == 1) {
			Social.ReportProgress ("CgkIs-r3kO4CEAIQDA", 100.0f, (bool success) => {
			});
		}
	}

	public static void FirstSpin(){
		Social.ReportProgress ("CgkIs-r3kO4CEAIQCg", 100.0f, (bool success) => {
			// handle success or failure
		});
	}
	public static void PlanetReachAchievement(int planetcount){
		if (planetcount > 0) {
			Social.ReportProgress("CgkIs-r3kO4CEAIQAQ" , 100.0f, (bool success) => {
				// handle success or failure
			});
		}
		if (planetcount > 9) {
			Social.ReportProgress("CgkIs-r3kO4CEAIQAw" , 100.0f, (bool success) => {
				// handle success or failure
			});
		}
		if (planetcount > 19) {
			Social.ReportProgress("CgkIs-r3kO4CEAIQBA" , 100.0f, (bool success) => {
				// handle success or failure
			});
		}
		if (planetcount > 29) {
			Social.ReportProgress("CgkIs-r3kO4CEAIQBQ" , 100.0f, (bool success) => {
				// handle success or failure
			});
		}
		if (planetcount > 49) {
			Social.ReportProgress("CgkIs-r3kO4CEAIQBg" , 100.0f, (bool success) => {
				// handle success or failure
			});
		}
		if (planetcount > 99) {
			Social.ReportProgress("CgkIs-r3kO4CEAIQBw" , 100.0f, (bool success) => {
				// handle success or failure
			});
		}
		if (planetcount > 149) {
			Social.ReportProgress("CgkIs-r3kO4CEAIQCA" , 100.0f, (bool success) => {
				// handle success or failure
			});
		}
		if (planetcount > 199) {
			Social.ReportProgress("CgkIs-r3kO4CEAIQCQ" , 100.0f, (bool success) => {
				// handle success or failure
			});
		}

	}


}
