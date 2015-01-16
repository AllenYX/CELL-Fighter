using UnityEngine;
using System.Collections;

public class EndoplasmicRoughController : OrganelleController {
	
	private GameObject nucleolusControllerObject;
	private NucleolusController nucleolusController;
	private GameObject endoplasmicSmoothControllerObject;
	private EndoplasmicSmoothController endoplasmicSmoothController;
	private GameObject golgiApparatusControllerObject;
	private GolgiApparatusController golgiApparatusController;

	private GameObject endoplasmicRoughObject;
	private SpriteRenderer endoplasmicRoughRenderer;

	public Sprite endoplasmicRough_0;
	public Sprite endoplasmicRough_1;
	public Sprite endoplasmicRough_2;

	private GameObject dragObject;
	private bool holdingObject = false;
	
	private Vector3 worldPosition;
	private Vector3 screenPosition;
	private Vector3 offscreenPosition = new Vector3(15, 0, 0);

	private int endoplasmicRibosomes;

	private void Start () {
		ReferenceObjects ();
		ResizeText ();

		DisplayEndoplasmicRibosomes ();
	}	
	
	private void ReferenceObjects () {
		ReferenceCellController ();

		nucleolusControllerObject = GameObject.FindWithTag ("Nucleolus");
		if (nucleolusControllerObject != null)
			nucleolusController = nucleolusControllerObject.GetComponent <NucleolusController>();
		if (nucleolusController == null)
			Debug.Log ("Cannot find 'NucleolusController' script");
		
		endoplasmicSmoothControllerObject = GameObject.FindWithTag ("SmoothER");
		if (endoplasmicSmoothControllerObject != null)
			endoplasmicSmoothController = endoplasmicSmoothControllerObject.GetComponent <EndoplasmicSmoothController>();
		if (endoplasmicSmoothController == null)
			Debug.Log ("Cannot find 'EndoplasmicSmoothController' script");
		
		golgiApparatusControllerObject = GameObject.FindWithTag ("GolgiApparatus");
		if (golgiApparatusControllerObject != null)
			golgiApparatusController = golgiApparatusControllerObject.GetComponent <GolgiApparatusController>();
		if (golgiApparatusController == null)
			Debug.Log ("Cannot find 'GolgiApparatusController' script");

		endoplasmicRoughRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	private void OnMouseDown () {
		if (cellController.playerCell.Protein > 0) {
			dragObject = GameObject.FindWithTag ("DragProtein");
			SetPositionToMouse ();
			holdingObject = true;
		} else {
			print ("No protein to drag");
		}
	}
	
	private void OnMouseDrag () {
		if (holdingObject)
			SetPositionToMouse ();
	}	
	
	//Drop glucose 
	private void OnMouseUp () {
		if (holdingObject) {
			if (endoplasmicSmoothController && 
			    endoplasmicSmoothController.organelleText.enabled)
				cellController.HealMembrane ();
			else if (golgiApparatusController &&
			         golgiApparatusController.organelleText.enabled)
				print ("Opened weapons window");
			else if (nucleolusController &&
			         nucleolusController.organelleText.enabled)
				cellController.CreateRibosome ();
			
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

	private void DisplayEndoplasmicRibosomes () {
		endoplasmicRibosomes = cellController.playerCell.RibosomesLimit;

		if (endoplasmicRibosomes <= 3f)
			endoplasmicRoughRenderer.sprite = endoplasmicRough_0;
		else if (endoplasmicRibosomes <= 6f)
			endoplasmicRoughRenderer.sprite = endoplasmicRough_1;
		else if (endoplasmicRibosomes <= 9f)
			endoplasmicRoughRenderer.sprite = endoplasmicRough_2;
	}
}
