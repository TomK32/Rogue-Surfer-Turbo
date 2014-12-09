using UnityEngine;
using System.Collections;

public class WaveGenerator : MonoBehaviour {

  public int numberOfWaves;
  private GameObject[] waves;
  private float dtSinceLastWave;
  private float waveFrequency;
  public float maxWaveWidth = 20;
  public float maxWaveHeight = 5;

	// Use this for initialization
	void Start () {
    waveFrequency = 0.5f;
	}

  void Update() {
    dtSinceLastWave += Time.deltaTime;
    if(dtSinceLastWave > waveFrequency) {
      dtSinceLastWave = 0;
      GenerateWave();
    }
  }

  void GenerateWave() {
    float verticalSize   = (float) Camera.main.orthographicSize * 2.0f;
    float horizontalSize = (float) verticalSize * Screen.width / Screen.height;
    Vector3 position = new Vector3(Random.Range(0, horizontalSize), verticalSize, 0.0f);

    GameObject wave = (GameObject) Instantiate(Resources.Load("Wave"), position, Quaternion.identity);
    wave.GetComponent<WaveController>().width = (int)(Mathf.PerlinNoise(Time.timeSinceLevelLoad, 1) * maxWaveWidth);
    wave.GetComponent<WaveController>().height = (int)(Mathf.PerlinNoise(Time.timeSinceLevelLoad, 1) * maxWaveHeight);
    wave.GetComponent<WaveController>().Randomize();
    wave.GetComponent<WaveController>().CreateSprite();
    wave.transform.parent = transform;
  }
}
