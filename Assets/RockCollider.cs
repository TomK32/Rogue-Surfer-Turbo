using UnityEngine;
using System.Collections;

public class RockCollider : MonoBehaviour {
  private bool isCoveredWithWave = false;

	void OnTriggerEnter2D (Collider2D collider) {
    if (!isCoveredWithWave && collider.gameObject.tag == "Player") {
      print("Hit");
    }
	}
  void OnTriggerStay2D (Collider2D collider) {
    if (collider.gameObject.tag == "Wave")
      isCoveredWithWave = true;
  }
  void OnTriggerExit2D (Collider2D collider) {
    if (collider.gameObject.tag == "Wave")
      isCoveredWithWave = false;
  }
}
