﻿using UnityEngine;
using System.Collections;

public class SurferPlayercontroller : MonoBehaviour {
  public Map map;
  private float speed;
  private float rotation;

  // only the sprites are dealt with in the animations, all other values are set this way
  private enum States:int { Walking, Paddling, Surfing };
  private float[] speedFactors = new float[3] {10.0f, 0.75f, 0.05f};
  private float[] rotationFactors = new float[3] { 1.0f, 0.5f, 2.0f };
  private float[] linearDrag = new float[3] { 2.0f, 0.1f, 0.05f };

  public int state;
  private float last_speed;

	// Use this for initialization
	void Start () {
    rotation = 0.0f;
    SetState((int)States.Walking);

	  float verticalSize = Camera.main.orthographicSize;
    float horizontalSize = (float) verticalSize * Screen.width / Screen.height;
    transform.Translate(new Vector3(horizontalSize * 0.8f, verticalSize * 0.2f, 0.0f));
	}

  float RotationFactor() {
    return rotationFactors[state];
  }

  float SpeedFactor() {
    return speedFactors[state];
  }

  void OnTriggerEnter2D (Collider2D collider) {
    if(collider.gameObject.tag == "wave") {
      GameObject text = (GameObject) Instantiate(Resources.Load("eventtext"), transform.position, Quaternion.identity);
      text.GetComponent<TextMesh>().text = "Wave";
      text.transform.parent = transform;
    }
  }

  void OnTriggerStay2D (Collider2D collider) {
    //collision.gameObject.collider2D.enabled = false;
    rigidbody2D.AddForce(collider.gameObject.GetComponent<WaveController>().ForceOnPlayer() * 10);
    if (state == (int)States.Surfing) {
      speed += Time.fixedDeltaTime;
    }
  }

	// Update is called once per frame
	void FixedUpdate () {
    float input_speed = Input.GetAxis("Vertical") * SpeedFactor();
    if (last_speed > input_speed)
      input_speed = 0;
    last_speed = input_speed;

    rigidbody2D.AddForce(gameObject.transform.up * input_speed);

    rotation = Input.GetAxis("Horizontal") * RotationFactor();
    transform.Rotate(0, 0, -rotation * RotationFactor());

	}


  void ChangeState() {
    if(state == (int)States.Walking) {
      GetComponent<Animator>().Play("Paddling");
      SetState((int)States.Paddling);
    } else if (state == (int)States.Paddling) {
      GetComponent<Animator>().Play("Surfing");
      SetState((int)States.Surfing);
    } else if (state == (int)States.Surfing) {
      GetComponent<Animator>().Play("Paddling");
      SetState((int)States.Paddling);
    }
  }

  void SetState(int state) {
    this.state = state;
    gameObject.rigidbody2D.drag = this.linearDrag[state];
  }
}
