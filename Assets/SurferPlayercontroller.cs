using UnityEngine;
using System.Collections;

public class SurferPlayercontroller : MonoBehaviour {
  public Map map;
  private float speed;
  public float[] speedFactors = new float[3] {1.0f, 0.25f, 0.025f};
  private float maxWalkSpeed;
  private float rotation;
  private float[] rotationFactors = new float[3] { 10.0f, 5.0f, 20.0f };
  private enum States:int { Walking, Paddling, Surfing };
  public Sprite[] stateSprites = new Sprite[3];
  public int state;
  private float standingTime;
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

	// Update is called once per frame
	void FixedUpdate () {
    float input_speed = Input.GetAxis("Vertical") * SpeedFactor();
    if (last_speed > input_speed)
      input_speed = 0.0f;
    last_speed = input_speed;
    Debug.Log(SpeedFactor());
    Debug.Log(Input.GetAxis("Vertical"));
    rotation = Input.GetAxis("Horizontal");

    rigidbody2D.AddForce(gameObject.transform.up * input_speed);

    transform.Rotate(0, 0, -rotation * RotationFactor());

    if (speed <= 0)
      speed = 0;
    else if (speed > maxWalkSpeed)
      speed = maxWalkSpeed;
    else if (speed > 0.0f)
      // slow down
      speed -= Time.deltaTime;


    if(standingTime > 0) {
      standingTime -= Time.deltaTime;
    } else if(Input.GetButtonDown("Stand")) {
      standingTime = Time.fixedDeltaTime * 40.0f;
      if(state == (int)States.Walking) {
        state = (int)States.Paddling;
      } else if (state == (int)States.Paddling) {
        state = (int)States.Surfing;
      } else if (state == (int)States.Surfing) {
        state = (int)States.Paddling;
      }
      changeStanding();
    }
	}
  void changeStanding() {
    gameObject.GetComponent<SpriteRenderer>().sprite = stateSprites[(int)state];
  }
}
