using UnityEngine;
using System.Collections;

public class SurferPlayercontroller : MonoBehaviour {
  public float speed;
  public float speedFactor;
  public float maxWalkSpeed;
  public float rotation;
  public float rotationFactor;

	// Use this for initialization
	void Start () {
    speed = 0.0f;
    speedFactor = 4.0f;
    maxWalkSpeed = Time.fixedDeltaTime * 10;
    rotation = 0.0f;
    rotationFactor = 360.0f / (Time.fixedDeltaTime * 50.0f);

	  float verticalSize = Camera.main.orthographicSize;
    float horizontalSize = (float) verticalSize * Screen.width / Screen.height;
    transform.Translate(new Vector3(horizontalSize * 0.8f, verticalSize * 0.2f, 0.0f));
	}

	// Update is called once per frame
	void Update () {
    speed += Input.GetAxis("Vertical");
    rotation = Input.GetAxis("Horizontal") * Time.deltaTime * rotationFactor;

    Vector3 movement = new Vector3(0, 1.0f, 0) * (speed * speedFactor * Time.deltaTime);
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
	}
}
