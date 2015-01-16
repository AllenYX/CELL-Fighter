using UnityEngine;
using System.Collections;

public class AnimationWaterDeath : MonoBehaviour {

	private float animationDestroyTime = 0.5f;

	private void Start () {
		StartCoroutine ("PlayAnimation");
	}

	private IEnumerator PlayAnimation () {
		yield return new WaitForSeconds (animationDestroyTime);
		// Destroys game object after animation is finished, since
		// it is considered seperate from its destroyed parent "Water"
		Destroy (gameObject);
	}
}

