using UnityEngine;
using System.Collections;

public class PlayerGenerator : MonoBehaviour {
  public int players = 1;

	void Start () {
    for(int i=0; i < players; i++) {
      PlacePlayer();
    }
	}

  void PlacePlayer() {
    Map map = GetComponent<MapGenerator>().map;
    Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);
    GameObject player = (GameObject) Instantiate(Resources.Load("Player"), position, Quaternion.identity);
    player.GetComponent<SurferPlayercontroller>().map = map;
    player.GetComponent<EnforceBoundaries>().map = gameObject;
    player.transform.parent = transform;
  }

  void FixedUpdate() {
  }
}
