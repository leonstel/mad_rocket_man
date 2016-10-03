using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class UIManager : MonoBehaviour {

	//public string leaderboard;
	public static bool gameOver = false;
	public bool showGUI = false;
	public string leaderboard;
	public static GameObject gameovermenu;
	public static Text distance_score;
	public static Text highscore;

	void Start () {
		PlayGamesPlatform.Activate ();
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ().Build ();
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.InitializeInstance (config);
		if (Application.loadedLevel == 0) {
			if(PlayerPrefs.HasKey("Logintrue")){
				if(PlayerPrefs.GetInt("Logintrue")<1){ 
					LogIn ();
				}
			}else{
				PlayerPrefs.SetInt("Logintrue",0);
				LogIn ();
			}
		}
		Time.timeScale = 1;
		gameovermenu = GameObject.Find ("GameOverMenu");
		if(gameovermenu != null){
			distance_score = GameObject.FindGameObjectWithTag ("UI_distance_score").GetComponent<Text> ();
			highscore = GameObject.FindGameObjectWithTag ("UI_highscore").GetComponent<Text> ();
			gameovermenu.SetActive (false);
		}

	}
	public void LogIn(){
		Social.localUser.Authenticate ((bool success) => {
			if (success) {
				Googledatahandler.UploadHighScore();
				Googledatahandler.DownloadHighScore();
				Debug.Log ("Login success");
				PlayerPrefs.SetInt("Logintrue",0);
			} else {
				Debug.Log ("Login Failed");
				PlayerPrefs.SetInt("Logintrue",1);
			}
		});
	}

	public void Update(){

	}


	public static void setDistanceScore(int planetCount){
		distance_score.text = "Score: " + planetCount; 
		highscore.text = "Highscore: " + PlayerPrefs.GetInt ("HScore").ToString();

	}

	//Reloads the Level
	public void Reload(){
		Application.LoadLevel(Application.loadedLevel);
	}
	//shows the instruction page
	public void onClickPlay(){
		Application.LoadLevel (2);
	}
	//closes the app
	public void onClickQuit(){
		Application.Quit ();
	}
	//starts the first level
	public void onGo(){
		Application.LoadLevel (2);
	}
	//shows the menu screen 
	public void onBackToMenu(){
		Application.LoadLevel(0);
	}
	public void onCredits(){
		Googledatahandler.checkScene ();
		Application.LoadLevel(1);
	}

	public static void showGameOver(){
		gameovermenu.SetActive(true);
	}
	public void onLeaderbord(){
		LogIn ();
		Social.ShowLeaderboardUI();
	}
	public void onAchievement(){
		LogIn ();
			Social.ShowAchievementsUI ();
	}
}

