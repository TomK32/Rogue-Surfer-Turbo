using UnityEngine;
using System.Collections;

public class DestroyAfterAnimation : MonoBehaviour {

  // Will be set to the gameobject's animation by default.
  public Animation animation;
  private bool hasBeenPlayed = false;

	void LateUpdate () {
	  if (!animation) { animation = gameObject.animation; }
    if (!hasBeenPlayed && animation.isPlaying) {
      hasBeenPlayed = true;
    }
    if (hasBeenPlayed && !animation.isPlaying) {
      Destroy(gameObject);
    }
    animation.Play();
	}
}
