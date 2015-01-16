using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	// Destroys all game objects that hit it
	// Stops cluttering of objects that go offscreen
	private void OnTriggerEnter2D(Collider2D other) {
		Destroy(other.gameObject);
	}
}
