using UnityEngine;
using System.Collections;

public class CameraReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
    float verticalSize = camera.orthographicSize;
    float horizontalSize = (float) verticalSize * Screen.width / Screen.height;
    transform.position = new Vector3(horizontalSize, verticalSize, transform.position.z);
	}
}
