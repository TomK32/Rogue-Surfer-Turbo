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
    GameObject wave = (GameObject) Instantiate(waveTypes[0], position, Quaternion.identity);
    wave.transform.parent = transform;
  }
}
