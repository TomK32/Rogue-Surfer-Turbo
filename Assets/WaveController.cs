using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {
  public float speed;
  public Vector3 direction;
  public Sprite[] sprites = new Sprite[3];
  public int width = 0;
  public int height = 0;
  public int state = 0;
  public int spriteScale = 8;
  private float x_scale = 0.5f;
  private float y_scale = 0.25f;


  public void Randomize() {
    speed = 0.05f;
    direction = new Vector3(-0.2f, -1.0f, 0.0f);
    x_scale = Random.Range(x_scale / 2, x_scale * 2);
    y_scale = Random.Range(y_scale / 2, y_scale * 2);
  }

  public Vector3 ForceOnPlayer() {
    return direction * speed;
  }

  public void CreateSprite() {
    for (int x=0; x < width; x++) {
      for (int y=0; y < height; y++) {
        GameObject w = new GameObject("Wave part");
        w.AddComponent("SpriteRenderer");
        w.GetComponent<SpriteRenderer>().sprite = sprites[state];
        w.transform.parent = transform;
        w.transform.localPosition = new Vector3(x * x_scale, y * y_scale, 0);
      }
    }
    setCollider();
  }

  private void setCollider() {
    GetComponent<BoxCollider2D>().size = new Vector2(width * x_scale, height * y_scale);
    GetComponent<BoxCollider2D>().center = new Vector3((width - 1) * x_scale / 2, 0, 0);
  }

  // Update is called once per frame
	void FixedUpdate () {
    transform.Translate(direction * speed);
    if (transform.position.y <= 0.0f) {
      Destroy(gameObject);
    }
	}
}
