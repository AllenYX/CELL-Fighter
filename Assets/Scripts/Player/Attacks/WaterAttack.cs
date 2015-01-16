using UnityEngine;
using System.Collections;

public class WaterAttack : MonoBehaviour {

	private Mover moverScript;
	private float slowPercent = 0.5f;
	private float slowTime = 4f;

	private float speed = 4f;

	private void Start () {
		rigidbody2D.velocity = new Vector2 (1f, 0f) * speed;
	}

	public void OnTriggerEnter2D(Collider2D other) {
		// Only damages player
		if (other.tag == "Enemy") {
			//other.rigidbody2D.velocity *= 0.5f;
			moverScript = other.GetComponent <Mover>();
			moverScript.SlowEffect(slowPercent, slowTime);

			Destroy (gameObject);
		}
	}
}
