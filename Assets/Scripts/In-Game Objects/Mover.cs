using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
	
	public float minSpeed;
	public float maxSpeed;
	private float speed;
	private float startSpeed;
	private float speedPercent;
	
	private void Start () {
		speed = Random.Range (minSpeed, maxSpeed);
		rigidbody2D.velocity = new Vector2 (-1f, 0f) * speed;
		startSpeed = speed;
	}

	public void SlowEffect (float percent, float time) {
		speedPercent = percent;

		StopCoroutine ("Slowed");
		StartCoroutine ("Slowed", time);
	}

	private IEnumerator Slowed (float time) {
		rigidbody2D.velocity *= speedPercent;

		yield return new WaitForSeconds (time);

		rigidbody2D.velocity = new Vector2 (-1f, 0f) * startSpeed;
		yield return 0;
	}
}

