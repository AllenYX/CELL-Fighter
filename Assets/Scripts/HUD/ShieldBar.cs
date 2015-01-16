using UnityEngine;
using System.Collections;

public class ShieldBar : MonoBehaviour {

	protected GameObject cellControllerObject;
	protected CellController cellController;
	
	public Texture2D shieldFrame;
	private Rect shieldFrameRect;

	private float frameX = 0.02f;
	private float frameY = 0.115f;
	private float frameWidth = 0.06f;
	private float frameHeight = 0.04f;

	public Texture2D fullShield; //Full shield blocks damage
	public Texture2D damagedShield; //Damaged shield does not block damage
	private Rect shieldRect;

	private float barXOffset = 0.065f;
	private float barY = 0.15f;
	private float barWidth = 0.24f;
	private float barHeight = 0.7f;

	private int fullShields;
	private int totalShields;

	private void Start () {
		ReferenceObjects ();

		shieldFrameRect.x = Screen.width * frameX;
		shieldFrameRect.y = Screen.height * frameY;
		shieldFrameRect.width = Screen.width * frameWidth;
		shieldFrameRect.height = Screen.height * frameHeight;

		shieldRect.y = shieldFrameRect.y + shieldFrameRect.height * barY;
		shieldRect.width = shieldFrameRect.width * barWidth;
		shieldRect.height = shieldFrameRect.height * barHeight;
	}

	private void OnGUI () {
		DrawFrame ();
		DrawShields ();
	}

	private void ReferenceObjects () {
		cellControllerObject = GameObject.FindWithTag ("CellController");
		if (cellControllerObject != null)
			cellController = cellControllerObject.GetComponent <CellController>();
		if (cellController == null)
			Debug.Log ("Cannot find 'CellController' script");
	}

	private void DrawFrame () {
		GUI.DrawTexture (shieldFrameRect, shieldFrame);
	}

	private void DrawShields () {
		fullShields = cellController.playerCell.WallShieldCurrent;
		totalShields = cellController.playerCell.WallShieldTotal;

		for (int i = 0; i < totalShields; i++) {
			shieldRect.x = shieldFrameRect.x +
							shieldFrameRect.width * (barXOffset * (i + 1)) +
							shieldRect.width * i;

			//Draws full shields, then damaged
			if (i < fullShields) 
				GUI.DrawTexture (shieldRect, fullShield);
			else 
				GUI.DrawTexture (shieldRect, damagedShield);
		}
	}
}
