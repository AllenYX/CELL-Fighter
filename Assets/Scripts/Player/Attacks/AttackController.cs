using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour {
	
	private GameObject cellControllerObject;
	private CellController cellController;
	private AudioSource audioSource;
	
	public GameObject waterAttack;
	private int waterAttackCost = 2;
	public AudioClip waterAttackAudio;
	private float waterAttackVolume = 0.7f;
	private float waterAttackPitch = 0.5f;

	private int currentAttack = 0;
	private bool attackAllowed = false;
	private bool attackDelayed = false;
	private float delayTime = 0f;

	private void Start () {
		ReferenceObjects ();
	}	

	private void ReferenceObjects () { 
		cellControllerObject = GameObject.FindWithTag ("CellController");
		if (cellControllerObject != null)
			cellController = cellControllerObject.GetComponent <CellController>();
		if (cellController == null)
			Debug.Log ("Cannot find 'GameController' script");

		audioSource = gameObject.GetComponent <AudioSource>();
	}

	private void Update () {
		CheckAttackRequirements ();

		if (Time.timeScale == 0f || attackDelayed)
			return;

		if (Input.GetMouseButton (0)) {
			StartCoroutine ("PerformAttack", currentAttack);
		}
	}

	private void CheckAttackRequirements () {
		if (currentAttack == (int) Attack.Water) {
			if (cellController.playerCell.VacuoleWater >= waterAttackCost)
				attackAllowed = true;
			else
				attackAllowed = false;
		}
	}

	private IEnumerator PerformAttack (int attackType) {
		attackDelayed = true;

		if (attackAllowed) {
			if (attackType == (int) Attack.Water) {
				cellController.AddVacuoleWater (-waterAttackCost);
				Instantiate (waterAttack, transform.position, Quaternion.identity);
				delayTime = 0.8f;
				
				audioSource.clip = waterAttackAudio;
				audioSource.volume = waterAttackVolume;
				audioSource.pitch = waterAttackPitch;
				audioSource.Play ();
			}
			
			yield return new WaitForSeconds (delayTime);
		}

		attackDelayed = false;
		yield return 0;
	}

	public int CurrentAttack {
		get{ return currentAttack; }
		set{ currentAttack = value;}
	}

	public bool AttackAllowed {
		get{ return attackAllowed; }
	}
}

public enum Attack {
	Water = 0
}
