using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;


public class Googledatahandler : MonoBehaviour {

	public static List<Achievement> Scorelist = new List<Achievement>();
	// Use this for initialization
	void Start () {
		Debug.Log (Application.loadedLevel);
		checkScene ();
		Scorelist.Add (new Achievement(1, "CgkIs-r3kO4CEAIQAQ"));
		Scorelist.Add (new Achievement(10, "CgkIs-r3kO4CEAIQAw"));
		Scorelist.Add (new Achievement(20, "CgkIs-r3kO4CEAIQBA"));
		Scorelist.Add (new Achievement(30, "CgkIs-r3kO4CEAIQBQ"));
		Scorelist.Add (new Achievement(50, "CgkIs-r3kO4CEAIQBg"));
		Scorelist.Add (new Achievement(100, "CgkIs-r3kO4CEAIQBw"));
		Scorelist.Add (new Achievement(150, "CgkIs-r3kO4CEAIQCA"));
		Scorelist.Add (new Achievement(200, "CgkIs-r3kO4CEAIQCQ"));

	}

	// Update is called once per frame
	void Update () {
	}

	public static void RegisterHighScore(int planetcount){
		Social.ReportScore(planetcount, "CgkIs-r3kO4CEAIQAg", (bool success) => {
			// handle success or failure
		});
				
	}
	public static void RegisterDeath(){
		PlayGamesPlatform.Instance.IncrementAchievement(
			"CgkIs-r3kO4CEAIQCw", 1, (bool success) => {
				// handle success or failure
			});
	}
	public void checkScene(){
		if (Application.loadedLevel == 1) {
			Social.ReportProgress ("CgkIs-r3kO4CEAIQDA", 100.0f, (bool success) => {
				if (success) {
					Debug.Log ("Credits achievement unlocked");
				} else {
					Debug.Log ("credits unlocking Failed");
				}
			});
		}
	}

	public static void FirstSpin(){
		Social.ReportProgress ("CgkIs-r3kO4CEAIQCg", 100.0f, (bool success) => {
			// handle success or failure
		});
	}
	public static void PlanetReachAchievement(int planetcount){

		foreach (Achievement achievement in Scorelist){
			if (achievement.getplanetcount()<= planetcount) {
				Social.ReportProgress (achievement.getId(), 100.0f, (bool success) => {
					// handle success or failure
				});
			}
		}
	}

	public class Achievement{
		private int planetcount;
		private string id;

		public Achievement(int PlanetCount,string Id){
			planetcount = PlanetCount;
			id = Id;
		}

		public int getplanetcount(){
			return planetcount;
		}
		public string getId(){
			return id;
		}
	}
}
