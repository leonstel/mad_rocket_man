using UnityEngine;

public interface IWayPointListener {
	void PointReached (int index, string walkerName);
	void LastPointReached (string walkerName);
	GameObject getGameObject();
}
