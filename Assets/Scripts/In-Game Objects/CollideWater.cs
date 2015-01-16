using UnityEngine;
using System.Collections;

public class CollideWater : MonoBehaviour {
	
	private CellController cellController;
	private GameObject cellControllerObject;
	public GameObject waterDeath;

	private int waterValue = 3; // Amount of water player receives

	private void Start ()
	{
		ReferenceObjects ();
	}
	
	private void ReferenceObjects () {
		cellControllerObject = GameObject.FindWithTag ("CellController");
		if (cellControllerObject != null)
			cellController = cellControllerObject.GetComponent <CellController>();
		if (cellController == null)
			Debug.Log ("Cannot find 'GameController' script");
	}

	public void OnTriggerEnter2D(Collider2D other) {
		//Player-Water collision
		if (other.tag == "Player") {
			cellController.AddVacuoleWater (waterValue);

			// Creates new object that represents water after collision.
			// This property is not added to the original prefab since it
			// stops the movement script
			Instantiate (waterDeath, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}
