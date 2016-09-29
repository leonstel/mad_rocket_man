using UnityEngine;
using System.Collections;

public class AudioChef : MonoBehaviour 
{
	//background music
	public AudioSource backgroundmusic_layer_1;		
	public AudioClip background_music;

	public AudioSource fx_layer_1;					
	             
	public static AudioChef context = null;

	void Awake ()
	{
		context = this;

		playBackgroundMusic ();
	}

	void Update(){
		//if(Game.GetInstance().currentStage == Game.Stage.GameOver){
			//fx_layer_1.Stop ();
		//}
	}

	void playBackgroundMusic(){
		backgroundmusic_layer_1.clip = background_music;
		backgroundmusic_layer_1.Play ();
	}

	public static AudioChef getInstance(){
		return context;
	}
}