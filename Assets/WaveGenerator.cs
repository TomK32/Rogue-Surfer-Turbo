using UnityEngine;
using System.Collections;

public class WaveGenerator : MonoBehaviour {

  private float dtSinceLastWave;
  public float waveFrequency = 5f;
  public float minWaveWidth = 20;
  public float maxWaveHeight = 3;

	// Use this for initialization
	void Start () {
    Map map = gameObject.GetComponent<MapGenerator>().map;
	}

  void FixedUpdate() {
    dtSinceLastWave += Time.fixedDeltaTime;
    float seed = Mathf.PerlinNoise(Time.timeSinceLevelLoad, Time.timeSinceLevelLoad + 1);
    if(dtSinceLastWave > waveFrequency * seed) {
      dtSinceLastWave = 0;
      GenerateWave(seed);
    }
  }

  void GenerateWave(float seed) {
    float verticalSize   = (float) Camera.main.orthographicSize * 2.0f;
    float horizontalSize = (float) verticalSize * Screen.width / Screen.height;
    Vector3 position = new Vector3(Random.Range(0, horizontalSize), verticalSize, 0.0f);

    GameObject wave = (GameObject) Instantiate(Resources.Load("Wave"), position, Quaternion.identity);

    int width =  (int)(minWaveWidth + seed * minWaveWidth);
    int height = (int)(maxWaveHeight / 3 + seed * maxWaveHeight);
    wave.GetComponent<WaveController>().Randomize(width, height);
    wave.transform.parent = transform;
  }
}
