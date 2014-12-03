using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {
  public float speed;
  public Vector3 direction;
  public Sprite[] sprites = new Sprite[3];
  int width;
  int height;
  public int spriteScale = 8;

  void Start() {
    speed = 0.2f;

    //gameObject.GetComponent<SpriteRenderer>().sprite = sprites[(int)state];
    direction = new Vector3(-0.2f, -1.0f, 0.0f);
  }

  public void CreateSprite(int width, int height) {
    this.width = width;
    this.height = height;
    Texture2D texture = new Texture2D(spriteScale * width, spriteScale * height);
    Color[] pixels = sprites[1].texture.GetPixels();
    for (int x=0; x < width; x++) {
      for (int y=0; y < height; y++) {
        texture.SetPixels(x * spriteScale, y * spriteScale, spriteScale, spriteScale, pixels);
      }
    }
    GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0, 0, spriteScale * width, spriteScale * height), new Vector2(width/2,height/2), spriteScale);
  }

  }

  // Update is called once per frame
	void FixedUpdate () {
    transform.Translate(direction * speed);
    if (transform.position.y <= 0.0f) {
      Destroy(gameObject);
    }
	}
}
