using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private GameObject cellControllerObject;
	private CellController cellController;

	private Animator animator;
	private AudioSource audioSource;

	public AudioClip shieldedAudio;
	private float shieldedVolume = 0.9f;
	private float shieldedPitch = 0.8f;
	public AudioClip playerHitAudio;
	private float hitVolume = 0.4f;
	private float hitPitch = 0.6f;
	public AudioClip playerDeathAudio;
	private float deathVolume = 0.3f;
	private float deathPitch = 1f;

	public Boundary boundary;
	private float moveHorizontal;
	private float moveVertical;

	private Vector2 acceleration;
	private float maxAcceleration = 0.5f;
	private float maxVelocity = 2f;
	private float knockbackForce = 150f;

	private bool shielded = false;
	private bool invincible = false;
	private bool knockback = false;
	private bool moving = false;
	private bool dead = false;
	private bool audioPlaying = false;

	private void Start () {
		ReferenceObjects ();
	}

	private void ReferenceObjects () { 
		cellControllerObject = GameObject.FindWithTag ("CellController");
		if (cellControllerObject != null)
			cellController = cellControllerObject.GetComponent <CellController>();
		if (cellController == null)
			Debug.Log ("Cannot find 'GameController' script");

		animator = gameObject.GetComponent <Animator>();
		audioSource = gameObject.GetComponent <AudioSource>();
	}
	
	private void Update () {
		if (Time.timeScale == 0f)
			return;

		if (dead) {
			rigidbody2D.velocity = new Vector2 (0f, 0f);
			collider2D.enabled = false;
			animator.SetBool("Dead", dead);
			if (!audioPlaying) {
				audioSource.clip = playerDeathAudio;
				audioSource.volume = deathVolume;
				audioSource.pitch = deathPitch;
				audioSource.Play ();
				audioPlaying = true;
			}
			return;
		}

		if (knockback) {
			rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
			rigidbody2D.AddForce(new Vector2 (-knockbackForce, 0f));
			knockback = false;
		}
	}

	private void FixedUpdate () {
		if (!dead) {
			moveHorizontal = Input.GetAxis ("Horizontal");
			moveVertical = Input.GetAxis ("Vertical");

			if (moveHorizontal == 0 && moveVertical == 0) {
				rigidbody2D.drag = maxVelocity * 2f;
				if (moving) {
					animator.SetBool("Moving", false);
					moving = false;
				}
			} else {
				rigidbody2D.drag = maxVelocity;
				if (!moving) {
					animator.SetBool("Moving", true);
					moving = true;
				}
			}

			if ((rigidbody2D.velocity.x >= maxVelocity && moveHorizontal > 0) || 
			    (rigidbody2D.velocity.x <= -maxVelocity && moveHorizontal < 0))
				moveHorizontal = 0f;
			if ((rigidbody2D.velocity.y >= maxVelocity && moveVertical > 0) || 
			    (rigidbody2D.velocity.y <= -maxVelocity && moveVertical < 0))
				moveVertical = 0f;

			acceleration = new Vector2 (moveHorizontal * maxAcceleration, moveVertical * maxAcceleration);
			rigidbody2D.velocity += acceleration;
			
			transform.position = new Vector2
			(
				Mathf.Clamp (transform.position.x, boundary.xMin, boundary.xMax),
				Mathf.Clamp (transform.position.y, boundary.yMin, boundary.yMax)
			);
		}
	}

	public void ToggleShielded () {
		shielded = !shielded;
		animator.SetBool("Shielded", shielded);

		if (shielded) {
			audioSource.clip = shieldedAudio;
			audioSource.volume = shieldedVolume;
			audioSource.pitch = shieldedPitch;
			audioSource.Play ();
		}
	}

	public void ToggleInvincibility () {
		invincible = !invincible;
		animator.SetBool("Invincible", invincible); 

		if (invincible) {
			audioSource.clip = playerHitAudio;
			audioSource.volume = hitVolume;
			audioSource.pitch = hitPitch;
			audioSource.Play ();
		}
	}

	public bool Knockback {
		set { knockback = value;}
	}
	
	public bool Dead {
		set { dead = value;}
	}
}

[System.Serializable]
public class Boundary {
	public float xMin = -8f;
	public float xMax = 6f;
	public float yMin = 5.8f;
	public float yMax = 13.3f;
}
