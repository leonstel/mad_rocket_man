using UnityEngine;
using System.Collections;

public class AudioChef : MonoBehaviour 
{
	//background music
	public AudioSource backgroundmusic_layer_1;		
	public AudioClip dnb_1;
	public AudioClip dnb_2;
	public AudioClip dnb_3;
	public AudioClip dnb_4;
	public AudioClip dnb_5;
	public AudioClip dnb_6;

	public AudioSource fx_layer_1;					
	             
	public static AudioChef context = null;

	void Awake ()
	{
		context = this;

		playDnbSection (dnb_1);
	}

	void Update(){

		//if(Game.GetInstance().currentStage == Game.Stage.GameOver){
			//fx_layer_1.Stop ();
		//}
	}

	public void playBackgroundMusic(){
		WaveContainer currentWave = WaveChef.GetInstance ().getCurrentWave ();

		if(currentWave != null){
			int currentWaveNumber = currentWave.getWaveNumber ();

			if(currentWaveNumber > 3 && currentWaveNumber <= 5){
				playDnbSection (dnb_2);
			}else if(currentWaveNumber > 5 && currentWaveNumber <= 7){
				playDnbSection (dnb_3);
			}else if(currentWaveNumber > 7 && currentWaveNumber <= 10){
				playDnbSection (dnb_4);
			}else if(currentWaveNumber > 10 && currentWaveNumber <= 12){
				playDnbSection (dnb_5);
			}else if(currentWaveNumber > 12){
				playDnbSection (dnb_6);
			}
		}
	}

	void playDnbSection(AudioClip clip){
		if(backgroundmusic_layer_1.clip != clip){
			backgroundmusic_layer_1.clip = clip;
			backgroundmusic_layer_1.Play ();
		}
	}

	public static AudioChef getInstance(){
		return context;
	}
}