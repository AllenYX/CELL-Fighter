using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	private GameObject cellControllerObject;
	private CellController cellController;
	
	public Texture2D healthFrameBack;
	public Texture2D healthFrameFront;
	private Rect healthFrameRect;

	private float frameX = 0.02f; // Sets frame.x as percent of screen width
	private float frameY = 0.05f; // Sets frame.y as percent of screen height
	private float frameWidth = 0.2f; // Sets frame width as percent of screen width
	private float frameHeight = 0.028f; // Sets frame height as percent of screen height
	
	public Texture2D healthBar;
	private Rect healthBarRect;

	private float barX = 0.008f; // Sets bar.x as percent of frame width
	private float barY = 0.05f; // Sets bar.y as percent of frame height
	private float barWidth = 0.985f; // Sets bar width as percent of frame width
	private float barHeight = 0.85f; // Sets bar height as percent of frame height

	private float healthPercent;

	private void Start () {
		ReferenceObjects ();

		// Sets health bar frame position
		healthFrameRect.x = Screen.width * frameX; 
		healthFrameRect.y = Screen.height * frameY;
		healthFrameRect.width = Screen.width * frameWidth;
		healthFrameRect.height = Screen.height * frameHeight;

		// Sets health bar position, relative to its frame
		healthBarRect.x = healthFrameRect.x + healthFrameRect.width * barX;
		healthBarRect.y = healthFrameRect.y + healthFrameRect.height * barY;
		healthBarRect.height = healthFrameRect.height * barHeight;
	}

	private void OnGUI () {
		DrawFrameBack ();
		DrawBar ();
		DrawFrameFront ();
	}

	private void ReferenceObjects () {
		cellControllerObject = GameObject.FindWithTag ("CellController");
		if (cellControllerObject != null)
			cellController = cellControllerObject.GetComponent <CellController>();
		if (cellController == null)
			Debug.Log ("Cannot find 'CellController' script");
	}

	private void DrawFrameBack () {
		GUI.DrawTexture (healthFrameRect, healthFrameBack);
	}

	// Draws outer frame of health bar
	private void DrawFrameFront () {
		GUI.DrawTexture (healthFrameRect, healthFrameFront);
	}
	
	// Draws inner "health" amount of health bar
	private void DrawBar () {
		// Gets percentage of health from playerCell class
		healthPercent = (float) cellController.playerCell.MembraneHealthCurrent /
			cellController.playerCell.MembraneHealthTotal;

		healthBarRect.width = healthFrameRect.width * barWidth * healthPercent;
		GUI.DrawTexture (healthBarRect, healthBar);
	}
}