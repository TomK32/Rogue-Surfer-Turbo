﻿using UnityEngine;
using System.Collections;

public class EnforceBoundaries : MonoBehaviour {

  public GameObject map;

  float xMax, xMin, yMax, yMin;

  void Start() {
    xMax = map.GetComponent<MeshRenderer>().bounds.max.x;
    xMin = map.GetComponent<MeshRenderer>().bounds.min.x;
    yMax = map.GetComponent<MeshRenderer>().bounds.max.y;
    yMin = map.GetComponent<MeshRenderer>().bounds.min.y;
  }

	void Update() {
    Vector3 newPosition = transform.position;

    newPosition.x = Mathf.Clamp( newPosition.x, xMin, xMax );
    newPosition.y = Mathf.Clamp( newPosition.y, yMin, yMax );
    transform.position = newPosition;
  }
}
