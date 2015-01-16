using UnityEngine;
using System.Collections;

public class CollideDamagable : MonoBehaviour {

	private CellController cellController;
	private GameObject cellControllerObject;

	public int damageValue; // Damage of colliding

	void Start () {
		ReferenceObjects ();
	}	

	private void ReferenceObjects () {
		cellControllerObject = GameObject.FindWithTag ("CellController");
		if (cellControllerObject != null)
			cellController = cellControllerObject.GetComponent <CellController>();
		if (cellController == null)
			Debug.Log ("Cannot find 'GameController' script");
	}

	// OnTriggerStay is used due to non-disappearing hazards and player invincibility
	// after hit
	public void OnTriggerStay2D(Collider2D other) {
		// Only damages player
		if (other.tag == "Player") {
			cellController.DamageMembrane (damageValue, true);
		}
	}
}
