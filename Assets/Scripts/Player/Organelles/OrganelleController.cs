using UnityEngine;
using System.Collections;

public class OrganelleController : MonoBehaviour {
	
	protected GameObject cellControllerObject;
	protected CellController cellController;
	protected float aspectWidth = 1366f;
	protected float aspectHeight = 768f;
	protected float resizeRatio;
	protected float defaultFontSize = 18f;

	public GUIText organelleText;

	protected void ReferenceCellController () {
		cellControllerObject = GameObject.FindWithTag ("CellController");
		if (cellControllerObject != null)
			cellController = cellControllerObject.GetComponent <CellController>();
		if (cellController == null)
			Debug.Log ("Cannot find 'CellController' script");
	}

	protected void ResizeText () {
		aspectWidth = GameController.aspectWidth;
		aspectHeight = GameController.aspectHeight;

		float widthRatio = Screen.width / aspectWidth;
		float heightRatio = Screen.height / aspectHeight;
		resizeRatio = Mathf.Min (widthRatio, heightRatio);

		organelleText.fontSize = Mathf.FloorToInt (defaultFontSize * resizeRatio);
	}

	private void OnMouseOver () {
		if (!PauseController.gamePaused)
			organelleText.enabled = true;
	}
	
	private void OnMouseExit () {
		organelleText.enabled = false;
	}
}
