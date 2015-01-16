using UnityEngine;
using System.Collections;

public class AttackHUD : MonoBehaviour {

	private GameObject attackControllerObject;
	private AttackController attackController;

	public GameObject attackIconObject;
	private SpriteRenderer attackIconSprite;
	public GameObject attackResourceObject;
	private SpriteRenderer attackResourceSprite;
	public GameObject attackCostObject;
	private TextMesh attackCostText;

	public Sprite waterAttackSprite;

	private void Start () {
		ReferenceObjects ();
	}	
	
	private void ReferenceObjects () { 
		attackControllerObject = GameObject.FindWithTag ("AttackController");
		if (attackControllerObject != null)
			attackController = attackControllerObject.GetComponent <AttackController>();
		if (attackController == null)
			Debug.Log ("Cannot find 'AttackController' script");

		attackIconSprite = attackIconObject.GetComponent<SpriteRenderer>();
		attackResourceSprite = attackResourceObject.GetComponent<SpriteRenderer>();
		attackCostText = attackCostObject.GetComponent<TextMesh>();
	}

	private void Update () {
		if (!attackController.AttackAllowed)
			attackCostText.color = Color.red;
		else 
			attackCostText.color = Color.white;
	}
}
