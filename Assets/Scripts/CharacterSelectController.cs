using UnityEngine;
using System.Collections;

public class CharacterSelectController : MonoBehaviour {
	
	public int cell;
	public Sprite boxNormal;
	public Sprite boxHover;

	public GUIStyle labelStyle;
	public Texture2D infoBox;
	private Rect infoBoxRect;

	private float aspectWidth = 1366f;
	private float aspectHeight = 768f;
	private float labelFontSize = 20f;
	private bool hovering = false;

	private SpriteRenderer boxSpriteRenderer;

	private void Start () {
		boxSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		infoBoxRect = new Rect (Screen.width * 0.25f, Screen.height * 0.7f, Screen.width * 0.5f, Screen.height * 0.25f);
		ResizeText ();
	}

	private void ResizeText () {
		aspectWidth = GameController.aspectWidth;
		aspectHeight = GameController.aspectHeight;
		
		float widthRatio = Screen.width / aspectWidth;
		float heightRatio = Screen.height / aspectHeight;
		float resizeRatio = Mathf.Min (widthRatio, heightRatio);
		
		labelStyle.fontSize = Mathf.FloorToInt (labelFontSize * resizeRatio);
	}

	private void OnMouseEnter () {
		boxSpriteRenderer.sprite = boxHover;
		hovering = true;
	}

	private void OnMouseExit () {
		boxSpriteRenderer.sprite = boxNormal;
		hovering = false;
	}
	
	private void OnMouseDown () {
		if (cell == 0) {
			PlayerPrefs.SetInt ("ExistingData", 1);
			Application.LoadLevel ((int) Scenes.LevelSelect);
		}
		// animal cell
		//else if (cell == 1) 
			//Application.LoadLevel ((int) Scenes.LevelSelect);
	}

	private void OnGUI () {
		if (hovering) {
			GUI.DrawTexture (infoBoxRect, infoBox);
			if (cell == 0) {
				GUI.Label(new Rect (Screen.width * 0.28f, Screen.height * 0.73f, 
				                    Screen.width * 0.44f, Screen.height * 0.3f), "Plant cell description", labelStyle);
			} else if (cell == 1) {
				GUI.Label(new Rect (Screen.width * 0.28f, Screen.height * 0.73f, 
				                       Screen.width * 0.44f, Screen.height * 0.3f), "Animal cell description", labelStyle);
			}
		}
	}
}
