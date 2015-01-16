using UnityEngine;
using System.Collections;

public class SideScroller : MonoBehaviour {

	public float speed;

	// Update is called once per frame
	private void Update () {
		renderer.material.mainTextureOffset = new Vector2 ((Time.time * speed) % 1, 0f);
	}
}
