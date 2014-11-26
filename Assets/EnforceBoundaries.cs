using UnityEngine;
using System.Collections;

public class EnforceBoundaries : MonoBehaviour {

  public GameObject map;

  float xMax, xMin, yMax, yMin;

  void Start() {
    Vector3 cameraPosition = Camera.main.transform.position;
    float dist = Camera.main.aspect * Camera.main.orthographicSize;
    float sizeX = map.GetComponent<MeshRenderer>().bounds.min.x / 2;
    float sizeY = map.GetComponent<MeshRenderer>().bounds.min.y / 2;
    xMax = map.GetComponent<MeshRenderer>().bounds.max.x;
    xMin = map.GetComponent<MeshRenderer>().bounds.min.x;
    yMax = map.GetComponent<MeshRenderer>().bounds.max.y;
    yMin = map.GetComponent<MeshRenderer>().bounds.min.y;

    Debug.Log(xMax);
    Debug.Log(yMax);
    Debug.Log(xMin);
    Debug.Log(yMin);
  }

	void Update() {
    Vector3 newPosition = transform.position;

    newPosition.x = Mathf.Clamp( newPosition.x, xMin, xMax );
    newPosition.y = Mathf.Clamp( newPosition.y, yMin, yMax );
    transform.position = newPosition;
  }
}
