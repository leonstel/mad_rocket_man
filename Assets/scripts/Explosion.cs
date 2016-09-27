using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> () as Animator;
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo (0);

		//destroy gameobject when animation has finished
		if(asi.normalizedTime > .9f && !animator.IsInTransition(0)){
			Destroy (gameObject);
		}
	}
}
