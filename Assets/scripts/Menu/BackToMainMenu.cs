using UnityEngine;
using System.Collections;

public class BackToMainMenu : MonoBehaviour {
		void OnMouseDown(){
			Application.LoadLevel(0);
		}
}
