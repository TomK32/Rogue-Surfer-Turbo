using UnityEngine;
using System.Collections;

public class SignCollider : MonoBehaviour {
  public GameObject text;
  public float destroyAfter = 5.0f;

  void OnTriggerExit2D() {
    text.SetActive(false);
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (collider.gameObject.tag == "Player") {
      text.SetActive(true);
      Invoke("OnTriggerExit2D", destroyAfter);
    }
  }
}
