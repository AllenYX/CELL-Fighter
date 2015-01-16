using UnityEngine;
using System.Collections;

public class PercentBarHorizontal : MonoBehaviour {

	private GameObject chloroplastControllerObject;
	private ChloroplastController chloroplastController;
	private GameObject mitochondriaControllerObject;
	private MitochondriaController mitochondriaController;

	public Texture2D barFrame;
	private Rect barFrameRect;
	private float barFrameX = 0.18f;
	private float barPhotosynthesisY = 0.9f; // Handles both photosynthesis and
	private float barRespirationY = 0.94f; 	  // respiration
	private float barFrameWidth = 0.08f;
	private float barFrameHeight = 0.012f;

	public Texture2D percentBar;
	private Rect percentBarRect;
	private float percentBarX = 0.0118f;
	private float percentBarY = 0.113f;
	private float percentBarWidth = 0.98f;
	private float percentBarHeight = 0.772f;

	private float percentBarAbsoluteWidth;
	private float percentBarAbsoluteHeight;

	private float percent = 0.5f;

	private void Start () {
		ReferenceObjects ();

		barFrameRect.x = Screen.width * barFrameX;
		barPhotosynthesisY *= Screen.height;
		barRespirationY *= Screen.height;
		barFrameRect.width = Screen.width * barFrameWidth;
		barFrameRect.height = Screen.height * barFrameHeight;
		
		percentBarRect.x = barFrameRect.x + barFrameRect.width * percentBarX;
		percentBarRect.height = barFrameRect.height * percentBarHeight;

		percentBarAbsoluteHeight = barFrameRect.height * percentBarY;
		percentBarAbsoluteWidth = barFrameRect.width * percentBarWidth;

		/*textPhotosynthesisY = Screen.height * (barPhotosynthesisY - 0.01f);
		textRespirationY = Screen.height * (barRespirationY - 0.01f);
		
		textRect.width = barFrame.width / 2;
		textRect.height = barFrame.height;
		textRect.x = barFrameRect.x;*/
	}

	private void OnGUI () {
		if (GameController.gameTimeRunning) {
			DrawFrame ();
			DrawBar ();
		}
	}

	private void ReferenceObjects () {
		chloroplastControllerObject = GameObject.FindWithTag ("Chloroplast");
		if (chloroplastControllerObject != null)
			chloroplastController = chloroplastControllerObject.GetComponent <ChloroplastController>();
		if (chloroplastController == null)
			Debug.Log ("Cannot find 'ChloroplastController' script");
		
		mitochondriaControllerObject = GameObject.FindWithTag ("Mitochondria");
		if (mitochondriaControllerObject != null)
			mitochondriaController = mitochondriaControllerObject.GetComponent <MitochondriaController>();
		if (mitochondriaController == null)
			Debug.Log ("Cannot find 'MitochondriaController' script");
	}
	
	private void DrawFrame () {
		if (gameObject.tag == "PhotosynthesisBar") {
			if (chloroplastController)
				percent = chloroplastController.PhotosynthesisPercent;
			barFrameRect.y = barPhotosynthesisY;
		} else if (gameObject.tag == "RespirationBar") {
			if (mitochondriaController)
				percent = mitochondriaController.RespirationPercent;
			barFrameRect.y = barRespirationY;
		}

		GUI.DrawTexture (barFrameRect, barFrame);
	}

	private void DrawBar () {
		percentBarRect.y = barFrameRect.y + percentBarAbsoluteHeight;
		percentBarRect.width =  percentBarAbsoluteWidth * percent;

		GUI.DrawTexture (percentBarRect, percentBar);
	}
}
