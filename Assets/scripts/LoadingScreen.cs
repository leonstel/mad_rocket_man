using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {
	IEnumerator Start(){
		AsyncOperation async = Application.LoadLevelAsync("core");
		async.allowSceneActivation = true;
		yield return async;
	}
}
