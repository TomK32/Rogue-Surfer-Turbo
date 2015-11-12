using UnityEngine;
using System.Collections;

public class WaveGenerator : MonoBehaviour {

  private float[] nextWaveAt = {1.0f, 5.0f};
  private float[] waveLengths = {20f, 34f};
  private float[] waveAmplitudes = {1.5f, 0.5f};
  public GameObject waveController;

  void FixedUpdate() {
    bool generate = false;
    for (int i = 0; i < this.waveLengths.Length; i++) {
      if (nextWaveAt[i] <= Time.timeSinceLevelLoad) {
        generate = true;
        nextWaveAt[i] += waveLengths[i];
      }
    }
    if (generate) {
      float waveAmplitude = 0f;
      float waveLength = 0f;
      for (int i = 0; i < this.waveLengths.Length; i++) {
        waveAmplitude += Mathf.Sin(Time.timeSinceLevelLoad + waveAmplitudes[i]);
        waveLength += waveLength;
      }
      GenerateWave(waveAmplitude / waveAmplitudes.Length, waveLength / waveAmplitudes.Length);
    }
  }

  void GenerateWave(float amplitude, float waveLength) {
    float verticalSize   = (float) Camera.main.orthographicSize * 2.0f;
    float horizontalSize = (float) verticalSize * Screen.width / Screen.height;
    Vector3 position = new Vector3(Random.Range(0, horizontalSize), verticalSize, 0.0f);

    GameObject wave = (GameObject)Instantiate(this.waveController, position, Quaternion.identity);
    wave.GetComponent<WaveController>().map = gameObject.GetComponent<MapGenerator>().map;
    wave.GetComponent<WaveController>().CreateWave(amplitude, waveLength);
    wave.transform.parent = transform;
  }
}
