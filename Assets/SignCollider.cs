using UnityEngine;
using System.Collections;

public class SignCollider : MonoBehaviour {
  public GameObject text;

  void OnTriggerExit2D() {
    text.SetActive(false);
  }

	void OnTriggerEnter2D() {
    text.SetActive(true);
	}
}
