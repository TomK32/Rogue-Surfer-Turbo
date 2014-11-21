using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {
  public Transform target;

	void Update () {
    Debug.Log(target);
    if (!target)
      return;
    transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
	}
}
