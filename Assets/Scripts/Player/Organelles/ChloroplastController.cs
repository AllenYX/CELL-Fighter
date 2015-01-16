using UnityEngine;
using System.Collections;

public class ChloroplastController : OrganelleController {

	private GameObject mitochondriaControllerObject;
	private MitochondriaController mitochondriaController;
	private GameObject lightControllerObject;
	private LightController lightController;

	public GameObject waterChloroplastIcon;
	private SpriteRenderer waterChloroplastIconRenderer;
	public GUIText waterChloroplastText;
	private float waterChloroplastFontSize = 16f;

	private float photosynthesisCurrentMeter = 0f; //Timer for glucose production
	private float photosynthesisEndMeter = 1f;
	private float photosynthesisPercent = 0f;
	private float photosynthesisMinTime = 10f;
	private float photosynthesisLambda = 0.5f; //Exponential cumulative distribution

	private SpriteRenderer chloroplastWaterRenderer;
	
	public Sprite water_0;
	public Sprite water_1;
	public Sprite water_2;
	public Sprite water_3;
	public Sprite water_4;
	
	private GameObject photosynthesisObject;
	private SpriteRenderer photosynthesisRenderer;
	
	public Sprite photosynthesis_0;
	public Sprite photosynthesis_1;
	public Sprite photosynthesis_2;
	public Sprite photosynthesis_3;
	public Sprite photosynthesis_4;
	public Sprite photosynthesis_5;
	public Sprite photosynthesis_6;
	public Sprite photosynthesis_7;
	public Sprite photosynthesis_8;

	private GameObject dragObject;
	private bool holdingObject = false;
	
	private Vector3 worldPosition;
	private Vector3 screenPosition;
	private Vector3 offscreenPosition = new Vector3(15, 0, 0);

	private float waterPercent;


	private void Start () {
		ReferenceObjects ();
		ResizeText ();
		waterChloroplastText.fontSize = Mathf.FloorToInt (waterChloroplastFontSize * resizeRatio);

		StartCoroutine ("Photosynthesis");
	}	
	
	private void ReferenceObjects () {
		ReferenceCellController ();
		
		mitochondriaControllerObject = GameObject.FindWithTag ("Mitochondria");
		if (mitochondriaControllerObject != null)
			mitochondriaController = mitochondriaControllerObject.GetComponent <MitochondriaController>();
		if (mitochondriaController == null)
			Debug.Log ("Cannot find 'MitochondriaController' script");

		lightControllerObject = GameObject.FindWithTag ("LightController");
		if (lightControllerObject != null)
			lightController = lightControllerObject.GetComponent <LightController>();
		if (lightController == null)
			Debug.Log ("Cannot find 'LightController' script");

		photosynthesisObject = GameObject.FindWithTag ("ChloroplastPhotosynthesis");
		if (photosynthesisObject != null)
			photosynthesisRenderer = photosynthesisObject.GetComponent<SpriteRenderer>();

		chloroplastWaterRenderer = gameObject.GetComponent<SpriteRenderer>();
		waterChloroplastIconRenderer = waterChloroplastIcon.GetComponent <SpriteRenderer>();
	}

	private void Update () {
		DisplayChloroplastWater ();
		DisplayChloroplastPhotosynthesis ();

		if (organelleText.enabled) {
			waterChloroplastText.enabled = true;
			waterChloroplastIconRenderer.enabled = true;

			waterChloroplastText.text = cellController.playerCell.ChloroplastWater +
				"/" + cellController.playerCell.ChloroplastWaterLimit;
		} else {
			waterChloroplastText.enabled = false;
			waterChloroplastIconRenderer.enabled = false;
		}

	}

	private void OnMouseDown () {
		if (cellController.playerCell.Glucose > 0) {
			dragObject = GameObject.FindWithTag ("DragGlucose");
			SetPositionToMouse ();
			holdingObject = true;
		}
	}

	private void OnMouseDrag () {
		if (holdingObject)
			SetPositionToMouse ();
	}	

	//Drop glucose 
	private void OnMouseUp () {
		if (holdingObject) {
			if (mitochondriaController &&
			    mitochondriaController.organelleText.enabled &&
			    mitochondriaController.RespirationPercent >= 1f)
				mitochondriaController.HasGlucose = true;
			
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

	private IEnumerator Photosynthesis () {
		while (true) {
			while (photosynthesisCurrentMeter < photosynthesisEndMeter) {
				yield return new WaitForSeconds(1f);
				//(1 - e^(-x*5)), where x = sunlight[0-1] gives good exponential cdf (log-like graph)
				//Yield time and divided value allow for finer value/rate adjustments
				if (cellController.playerCell.ChloroplastWater >= cellController.CreateGlucoseWaterCost)
					photosynthesisCurrentMeter += (1f - Mathf.Exp(-5f * photosynthesisLambda *
					                                              lightController.SunlightPercent /
					                                              100f))
													/ photosynthesisMinTime;

				if (photosynthesisCurrentMeter > photosynthesisEndMeter)
					photosynthesisCurrentMeter = photosynthesisEndMeter;

				photosynthesisPercent = photosynthesisCurrentMeter / photosynthesisEndMeter;
			}
			//Reset glucose synthesis timer
			photosynthesisCurrentMeter = 0f;

			cellController.CreateGlucose ();
		}
	}

	private void DisplayChloroplastWater () {
		float waterPercent = (float)cellController.playerCell.ChloroplastWater /
				cellController.playerCell.ChloroplastWaterLimit;

		if (waterPercent == 0f)
			chloroplastWaterRenderer.sprite = water_0;
		else if (waterPercent < 0.33f)
			chloroplastWaterRenderer.sprite = water_1;
		else if (waterPercent < 0.66f)
			chloroplastWaterRenderer.sprite = water_2;
		else if (waterPercent < 1f)
			chloroplastWaterRenderer.sprite = water_3;
		else if (waterPercent == 1f)
			chloroplastWaterRenderer.sprite = water_4;
	}

	private void DisplayChloroplastPhotosynthesis () {
		if (photosynthesisPercent == 0f)
			photosynthesisRenderer.sprite = photosynthesis_0;
		else if (photosynthesisPercent < 0.125f)
			photosynthesisRenderer.sprite = photosynthesis_1;
		else if (photosynthesisPercent < 0.25f)
			photosynthesisRenderer.sprite = photosynthesis_2;
		else if (photosynthesisPercent < 0.375f)
			photosynthesisRenderer.sprite = photosynthesis_3;
		else if (photosynthesisPercent < 0.5f)
			photosynthesisRenderer.sprite = photosynthesis_4;
		else if (photosynthesisPercent < 0.625f)
			photosynthesisRenderer.sprite = photosynthesis_5;
		else if (photosynthesisPercent < 0.75f)
			photosynthesisRenderer.sprite = photosynthesis_6;
		else if (photosynthesisPercent < 0.875f)
			photosynthesisRenderer.sprite = photosynthesis_7;
		else if (photosynthesisPercent <= 1f)
			photosynthesisRenderer.sprite = photosynthesis_8;
	}

	public float PhotosynthesisPercent {
		get { return photosynthesisPercent;}
	}
}
