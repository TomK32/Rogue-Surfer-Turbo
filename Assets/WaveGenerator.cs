using UnityEngine;
using System.Collections;

public class WaveGenerator : MonoBehaviour {

  public int numberOfWaves;
  public GameObject[] waveTypes;
  private GameObject[] waves;
  private float dtSinceLastWave;
  private float waveFrequency;

	// Use this for initialization
	void Start () {
    waveFrequency = 0.5f;
    float verticalSize   = (float) Camera.main.orthographicSize * 2.0f;
    float horizontalSize = (float) verticalSize * Screen.width / Screen.height;
    for(int x = numberOfWaves / 20; x > 0; x--) {
      for(int y = numberOfWaves / 40; y > 0; y--) {
        GenerateWave(Random.Range(0, horizontalSize), verticalSize);
      }
    }
	}

  void Update() {
    dtSinceLastWave += Time.deltaTime;
    if(dtSinceLastWave > waveFrequency) {
      dtSinceLastWave = 0;
      GenerateWave(Random.Range(0, Screen.width), Screen.height);
    }
  }

  void GenerateWave(float x, float y) {
    Vector3 position = new Vector3(x, y, 0.0f);
    GameObject wave = (GameObject) Instantiate(waveTypes[0], position, Quaternion.identity);
    wave.transform.parent = transform;
  }
}
