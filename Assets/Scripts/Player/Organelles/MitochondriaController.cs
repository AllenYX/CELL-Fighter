using UnityEngine;
using System.Collections;

public class MitochondriaController : OrganelleController {

	private SpriteRenderer mitochondriaRenderer;

	public Sprite respiration_0;
	public Sprite respiration_1;
	public Sprite respiration_2;
	public Sprite respiration_3;
	public Sprite respiration_4;
	public Sprite respiration_5;
	public Sprite respiration_6;
	public Sprite respiration_7;
	public Sprite respiration_8;
	public Sprite respiration_9;
	public Sprite respiration_10;

	private float respirationCurrentTime = 0f;
	private float respirationEndTime = 200f;
	private float respirationPercent;
	private bool hasGlucose = false;
	
	private void Start () {
		ReferenceObjects ();
		ResizeText ();

		StartCoroutine ("CellularRespiration");
	}

	private void ReferenceObjects () {
		ReferenceCellController ();
		mitochondriaRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	IEnumerator CellularRespiration () {
		while (true) {
			while (respirationCurrentTime < respirationEndTime) {
				yield return new WaitForSeconds(0.1f);
				respirationCurrentTime += 1f;

				DisplayMitochondriaRespiration ();
			}

			yield return null;

			//Reset ATP synthesis timer
			if (hasGlucose) {
				respirationCurrentTime = 0f;
				cellController.CreateAtp ();
				hasGlucose = false;

				DisplayMitochondriaRespiration ();
			}
		}
	}

	private void DisplayMitochondriaRespiration () {
		respirationPercent = respirationCurrentTime / respirationEndTime;

		if (respirationPercent == 0f)
			mitochondriaRenderer.sprite = respiration_0;
		else if (respirationPercent < 0.11f)
			mitochondriaRenderer.sprite = respiration_1;
		else if (respirationPercent < 0.22f)
			mitochondriaRenderer.sprite = respiration_2;
		else if (respirationPercent < 0.33f)
			mitochondriaRenderer.sprite = respiration_3;
		else if (respirationPercent < 0.44f)
			mitochondriaRenderer.sprite = respiration_4;
		else if (respirationPercent < 0.55f)
			mitochondriaRenderer.sprite = respiration_5;
		else if (respirationPercent < 0.66f)
			mitochondriaRenderer.sprite = respiration_6;
		else if (respirationPercent < 0.77f)
			mitochondriaRenderer.sprite = respiration_7;
		else if (respirationPercent < 0.88f)
			mitochondriaRenderer.sprite = respiration_8;
		else if (respirationPercent < 1f)
			mitochondriaRenderer.sprite = respiration_9;
		else if (respirationPercent == 1f)
			mitochondriaRenderer.sprite = respiration_10;
	}

	public float RespirationPercent {
		get { return respirationCurrentTime / respirationEndTime;}
	}

	public bool HasGlucose {
		set { hasGlucose = value;}
	}
}
