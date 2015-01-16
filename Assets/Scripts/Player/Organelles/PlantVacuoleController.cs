using UnityEngine;
using System.Collections;

public class PlantVacuoleController : OrganelleController {

	private GameObject chloroplastControllerObject;
	private ChloroplastController chloroplastController;
	
	private GameObject vacuoleWaterObject;
	private SpriteRenderer vacuoleWaterRenderer;

	public Sprite water_1;
	public Sprite water_2;
	public Sprite water_3;
	public Sprite water_4;
	public Sprite water_5;
	public Sprite water_6;
	public Sprite water_7;
	public Sprite water_8;
	public Sprite water_9;
	public Sprite water_10;
	
	private GameObject dragObject;
	private bool holdingObject = false;
	
	private Vector3 worldPosition;
	private Vector3 screenPosition;
	private Vector3 offscreenPosition = new Vector3(15, 0, 0);

	private float waterPercent;

	private void Start () {
		ReferenceObjects ();
		ResizeText ();
	}
	
	private void ReferenceObjects () {
		ReferenceCellController ();
		
		chloroplastControllerObject = GameObject.FindWithTag ("Chloroplast");
		if (chloroplastControllerObject != null)
			chloroplastController = chloroplastControllerObject.GetComponent <ChloroplastController>();
		if (chloroplastController == null)
			Debug.Log ("Cannot find 'ChloroplastController' script");
		
		vacuoleWaterObject = GameObject.FindWithTag ("VacuoleWater");
		if (vacuoleWaterObject != null)
			vacuoleWaterRenderer = vacuoleWaterObject.GetComponent<SpriteRenderer>();
	}


	private void Update () {
		DisplayWater ();
	}
	
	private void OnMouseDown () {
		if (cellController.playerCell.VacuoleWater > 0) {
			dragObject = GameObject.FindWithTag ("DragWater");
			SetPositionToMouse ();
			holdingObject = true;
		}
	}
	
	private void OnMouseDrag () {
		if (holdingObject)
			SetPositionToMouse ();
	}

	//Drop water 
	private void OnMouseUp () {
		if (holdingObject) {
			if (chloroplastController && 
			    chloroplastController.organelleText.enabled)
				cellController.AddChloroplastWater ();

			dragObject.transform.position = offscreenPosition;
			holdingObject = false;
		}
	}

	private void SetPositionToMouse () {
		screenPosition = Input.mousePosition;
		screenPosition.z = 10;
		worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
		dragObject.transform.position = worldPosition;
	}

	private void DisplayWater () {
		waterPercent = (float) cellController.playerCell.VacuoleWater / 
			cellController.playerCell.VacuoleWaterLimit * 100f;

		if (waterPercent == 0f)
			vacuoleWaterRenderer.sprite = null;
		else if (waterPercent <= 10f)
			vacuoleWaterRenderer.sprite = water_1;
		else if (waterPercent <= 20f)
			vacuoleWaterRenderer.sprite = water_2;
		else if (waterPercent <= 30f)
			vacuoleWaterRenderer.sprite = water_3;
		else if (waterPercent <= 40f)
			vacuoleWaterRenderer.sprite = water_4;
		else if (waterPercent <= 50f)
			vacuoleWaterRenderer.sprite = water_5;
		else if (waterPercent <= 60f)
			vacuoleWaterRenderer.sprite = water_6;
		else if (waterPercent <= 70f)
			vacuoleWaterRenderer.sprite = water_7;
		else if (waterPercent <= 80f)
			vacuoleWaterRenderer.sprite = water_8;
		else if (waterPercent <= 90f)
			vacuoleWaterRenderer.sprite = water_9;
		else
			vacuoleWaterRenderer.sprite = water_10;
	}
}
