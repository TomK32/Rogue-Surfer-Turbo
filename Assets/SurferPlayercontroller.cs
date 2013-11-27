using UnityEngine;
using System.Collections;

public class SurferPlayercontroller : MonoBehaviour {
  public float speed;
  public float[] speedFactors;
  public float maxWalkSpeed;
  public float rotation;
  public float[] rotationFactors;
  private enum States:int { Walking, Paddling, Surfing };
  public Sprite[] stateSprites = new Sprite[3];
  public int state;
  public float standingTime;

	// Use this for initialization
	void Start () {
    speed = 0.0f;
    speedFactors = new float[3] {1.0f, 0.25f, 0.025f};
    maxWalkSpeed = Time.fixedDeltaTime * 10;
    rotation = 0.0f;
    state = (int)States.Walking;
    rotationFactors = new float[3] { 90.0f, 150.0f, 120.0f };

	  float verticalSize = Camera.main.orthographicSize;
    float horizontalSize = (float) verticalSize * Screen.width / Screen.height;
    transform.Translate(new Vector3(horizontalSize * 0.8f, verticalSize * 0.2f, 0.0f));
	}

  float RotationFactor() {
    return 360.0f / (Time.fixedDeltaTime * rotationFactors[state]);
  }
  float SpeedFactor() {
    return speedFactors[state];
  }

	// Update is called once per frame
	void Update () {
    speed += Input.GetAxis("Vertical");
    rotation = Input.GetAxis("Horizontal") * Time.deltaTime * RotationFactor();

    Vector3 movement = new Vector3(0, 1.0f, 0) * (speed * SpeedFactor() * Time.deltaTime);
    if (speed != 0.0f && transform.position.y >= 0.0f && transform.position.y < Camera.main.orthographicSize * 2.0f)
      transform.Translate(movement);
    transform.Rotate(0, 0, -rotation);

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
