using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;


public class Googledatahandler : MonoBehaviour {
	public static List<Achievement> Scorelist = new List<Achievement>();
	public static int newScore;
	public static int oldScore;
	// Use this for initialization
	void Start () {
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
	private static void UploadHighScore(){
		Social.ReportScore(PlayerPrefs.GetInt("HScore"), "CgkIs-r3kO4CEAIQAg", (bool success) => {
			// handle success or failure
		});
	}

	private static void RegisterHighScore(int planetcount){
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
		string code = "";
		foreach (Achievement achievement in Scorelist){
			if (planetcount >= achievement.getplanetcount()) {
				code = achievement.getId();
			}
		}
		Social.ReportProgress(code +"" , 100.0f, (bool success) => {
			// handle success or failure
		});
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
