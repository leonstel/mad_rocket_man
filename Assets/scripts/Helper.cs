using UnityEngine;
using System;

public class Helper : MonoBehaviour {
	public static bool CheckIfNearby(string tag, GameObject targetPos, float distance){
		GameObject go = GameObject.FindGameObjectWithTag (tag);

		if(go){
			if(Vector2.Distance(targetPos.transform.position, go.transform.position) < distance){
				return true;		
			}
		}

		return false;
	}

	public static bool CheckIfNearby(GameObject go, GameObject targetPos, float distance){
		if(go && targetPos){
			if(Vector2.Distance(targetPos.transform.position, go.transform.position) < distance){
				return true;		
			}
		}

		return false;
	}

	public static void MoveToPosition(){

	}

	public static void LookAt(GameObject source, GameObject target){
		float rot_z = getRotZ (source, target);
		source.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
	}

	public static void LookAt90(GameObject source, GameObject target){
		float rot_z = getRotZ (source, target);
		source.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 180);
	}

	public static void LookAt180(GameObject source, GameObject target){
		float rot_z = getRotZ (source, target);
		source.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
	}

	public static void LookAt270(GameObject source, GameObject target){
		float rot_z = getRotZ (source, target);
		source.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
	}

	private static float getRotZ(GameObject source, GameObject target){
		Vector3 diff = source.transform.position - target.transform.position;

		diff.Normalize();

		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		return rot_z;
	}

	public static Quaternion getLookAtRot(GameObject source, GameObject target){
		Vector3 diff = source.transform.position - target.transform.position;

		diff.Normalize();

		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

		return Quaternion.Euler(0f, 0f, rot_z + 90);
	}

	public static IDisposable SetTimeout(Action method, int delayInMilliseconds)
	{
		System.Timers.Timer timer = new System.Timers.Timer(delayInMilliseconds);
		timer.Elapsed += (source, e) =>
		{
			method();
		};

		timer.AutoReset = false;
		timer.Enabled = true;
		timer.Start();

		// Returns a stop handle which can be used for stopping
		// the timer, if required
		return timer as IDisposable;
	}

	public static bool press(){
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
			return true;
		}

		if (Input.GetMouseButtonDown (0)) {
			return true;
		}
		return false;
	}

	public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle){
		return angle * (point - pivot) + pivot;
	}

}
