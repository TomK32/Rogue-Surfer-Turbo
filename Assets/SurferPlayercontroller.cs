using UnityEngine;
using System.Collections;

public class SurferPlayercontroller : MonoBehaviour {
  public float speed;
  public Vector3 direction;
  public float maxWalkSpeed;
  public float speedFactor;

	// Use this for initialization
	void Start () {
    direction = new Vector3(0, 1.0f, 0);
    speed = 0.0f;
    speedFactor = 4.0f;
    maxWalkSpeed = 1.0f;

	  float verticalSize = Camera.main.orthographicSize;
    float horizontalSize = (float) verticalSize * Screen.width / Screen.height;
    transform.Translate(new Vector3(horizontalSize * 0.8f, verticalSize * 0.2f, 0.0f));
	}
	
	// Update is called once per frame
	void Update () {
    speed += Input.GetAxis("Vertical");
    if (speed < 0)
      speed = 0;
    else if (speed > maxWalkSpeed)
      speed = maxWalkSpeed;
    else // slow down
      speed -= Time.deltaTime;


    transform.Translate(direction * speed * speedFactor * Time.deltaTime);
	}
}
