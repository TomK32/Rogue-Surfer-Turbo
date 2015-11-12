using UnityEngine;
using System.Collections;


// Act like good sinus waves
public class Wave {
  public float amplitude = 10.0f; // amplitude at start
  public float height = 5.0f; // current height above sea level, 1/2 of the amplitude
  public float wavelength = 100.0f; // distance from start to end
  public float period = 20.0f; // time between two waves. seconds
  // time to travel this distance.
  // in deep water the speed depends on period, when (depth < wavelength / 2) on depth
  public float speed = 0.1f;
  public Vector2 direction;
  public Vector2 origin;

  public Wave(float amplitude, float wavelength) {
    this.amplitude = amplitude;
    this.wavelength = wavelength;
    this.height = amplitude / 2.0f;
    // set wavePoints
  }

  public float setSpeed(float depth) {
    if (isShallow(depth)) {
      this.speed = Mathf.Pow(9.8f * depth, 2.0f);
    } else {
      this.speed = Mathf.Pow((9.8f * this.wavelength) / (2 * Mathf.PI), 2.0f);
    }
    return speed;
  }
  // from here the speed decreases while the height increases
  public bool isShallow(float depth) {
    return depth < this.wavelength / 2;
  }

  // even closer than the shallow zone, this is where waves do break
  public bool isBreaking(float depth) {
    return depth < 1.28f * this.height;
  }
}

public class WaveController : MonoBehaviour {
  public Sprite[] sprites = new Sprite[3];
  public Wave wave;
  public Map map;
  public int state = 0;
  public int spriteScale = 8;
  private float x_scale = 0.5f;
  private float y_scale = 0.25f;


  public void CreateWave(float amplitude, float wavelength) {
    this.wave = new Wave(amplitude, wavelength);
    gameObject.GetComponent<Rigidbody2D>().AddForce(wave.direction * wave.speed);
    CreateSprite();
  }

  public void CreateSprite() {
    for (int x=0; x < map.width; x++) {
      for (int y=0; y < this.wave.wavelength / 2; y++) {
        GameObject w = new GameObject("Wave part");
        w.AddComponent<SpriteRenderer>();
        w.GetComponent<SpriteRenderer>().sprite = sprites[state];
        w.transform.parent = transform;
        w.transform.localPosition = new Vector3(x * x_scale, y * y_scale, 0);
      }
    }
    setCollider();
  }

  private void setCollider() {
    GetComponent<BoxCollider2D>().size = new Vector2(map.width * x_scale, this.wave.height * y_scale);
    GetComponent<BoxCollider2D>().offset = new Vector3((map.width - 1) * x_scale / 2, 0, 0);
  }

  void FixedUpdate () {
    if (this.wave == null) { return; }
    if (this.wave.isShallow(this.map.DepthAt(transform.position))) {
    }
    if (transform.position.y <= 0.0f) {
      Destroy(gameObject);
    }
  }
}
