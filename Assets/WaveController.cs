using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {
  public float speed;
  public Vector3 direction;
  public Sprite[] sprites;

  void Start() {
    speed = 1.0f;
    direction = new Vector2(-0.2f, -1.0f);
  }

  // Update is called once per frame
	void Update () {
    transform.Translate(direction * speed * Time.deltaTime);
    if (transform.position.y <= 0.0f) {
      Destroy(gameObject);
    }
	}
}
