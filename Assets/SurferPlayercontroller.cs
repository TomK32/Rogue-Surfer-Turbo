using UnityEngine;
using System.Collections;

public class SurferPlayercontroller : MonoBehaviour {
  public Map map;
  private float speed;
  public float[] speedFactors = new float[3] {1.0f, 0.25f, 0.05f};
  private float maxWalkSpeed;
  private float rotation;
  private float[] rotationFactors = new float[3] { 1.0f, 0.5f, 2.0f };
  private enum States:int { Walking, Paddling, Surfing };
  public Sprite[] stateSprites = new Sprite[3];
  public int state;
  private float last_speed;

	// Use this for initialization
	void Start () {
    maxWalkSpeed = Time.fixedDeltaTime * 10;
    rotation = 0.0f;
    state = (int)States.Walking;
    speed = speedFactors[state];

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
    Debug.Log(input_speed);
    if (last_speed > input_speed)
      input_speed = 0;
    last_speed = input_speed;

    rigidbody2D.AddForce(gameObject.transform.up * input_speed);

    rotation = Input.GetAxis("Horizontal") * RotationFactor();
    transform.Rotate(0, 0, -rotation * RotationFactor());

    if(Input.GetButtonDown("Stand")) {
      if(state == (int)States.Walking) {
        GetComponent<Animator>().Play("Paddling");
        state = (int)States.Paddling;
      } else if (state == (int)States.Paddling) {
        GetComponent<Animator>().Play("Surfing");
        state = (int)States.Surfing;
      } else if (state == (int)States.Surfing) {
        state = (int)States.Paddling;
      }
    }
	}
}
