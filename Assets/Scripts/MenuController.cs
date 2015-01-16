using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GUISkin buttonSkinSelect;
	public GUISkin buttonSkinNormal;
	private float aspectWidth;
	private float aspectHeight;
	private float resizeRatio;
	private float defaultFontSize = 24f;
	private float smallFontSize = 20f;

	private Rect confirmWindowRect;
	public Texture2D confirmWindow;
	private bool displayConfirmWindow = false;

	private void Start () {
		ResizeText ();

		confirmWindowRect = new Rect (Screen.width * 0.4f, Screen.height * 0.4f,
		                              Screen.width * 0.2f, Screen.height * 0.2f);
	}

	private void ResizeText () {
		aspectWidth = GameController.aspectWidth;
		aspectHeight = GameController.aspectHeight;
		
		float widthRatio = Screen.width / aspectWidth;
		float heightRatio = Screen.height / aspectHeight;
		resizeRatio = Mathf.Min (widthRatio, heightRatio);
	}

	private void OnGUI () {
		GUI.skin = buttonSkinSelect;
		GUI.skin.button.fontSize = Mathf.FloorToInt (defaultFontSize * resizeRatio);
		
		if (PlayerPrefs.GetInt ("ExistingData") == 1)
			if (GUI.Button(new Rect(Screen.width * 0.75f, Screen.height * 0.45f,
		                        Screen.width * 0.16f, Screen.height * 0.06f), 
		               "CONTINUE GAME"))
					Application.LoadLevel ((int) Scenes.LevelSelect);
		if (GUI.Button(new Rect(Screen.width * 0.75f, Screen.height * 0.53f,
		                        Screen.width * 0.16f, Screen.height * 0.06f), 
		               "NEW GAME")) {
			if (PlayerPrefs.GetInt("ExistingData", 0) == 1)
			//if (PlayerPrefs.HasKey("ExistingData"))
				displayConfirmWindow = true;
			else {
				ResetPlayerPrefs ();
				Application.LoadLevel ((int) Scenes.CharacterSelect);
			}
		}
		if (GUI.Button(new Rect(Screen.width * 0.75f, Screen.height * 0.61f,
		                        Screen.width * 0.16f, Screen.height * 0.06f), 
		               "OPTIONS"))
			print ("clicked options");
		if (GUI.Button(new Rect(Screen.width * 0.75f, Screen.height * 0.69f,
		                        Screen.width * 0.16f, Screen.height * 0.06f), 
		               "QUIT"))
			Application.Quit();

		if (displayConfirmWindow) {
			DrawConfirmationWindow ();
		}
	}

	private void DrawConfirmationWindow () {
		GUI.skin = buttonSkinNormal;
		GUI.skin.button.fontSize = Mathf.FloorToInt (defaultFontSize * resizeRatio);
		GUI.skin.label.fontSize = Mathf.FloorToInt (smallFontSize * resizeRatio);

		GUI.DrawTexture (confirmWindowRect, confirmWindow);
		GUI.Label (new Rect (Screen.width * 0.42f, Screen.height * 0.42f,
		                     Screen.width * 0.17f, Screen.height * 0.18f), 
		           "Are you sure you want to start a new game? You will lose all current progress.");

		if (GUI.Button(new Rect(Screen.width * 0.45f, Screen.height * 0.55f,
		                        Screen.width * 0.04f, Screen.height * 0.05f), 
		               "YES")) {
			ResetPlayerPrefs ();
			Application.LoadLevel ((int) Scenes.CharacterSelect);
		}
		if (GUI.Button(new Rect(Screen.width * 0.51f, Screen.height * 0.55f,
		                        Screen.width * 0.04f, Screen.height * 0.05f), 
		               "NO"))
			displayConfirmWindow = false;
	}

	private void ResetPlayerPrefs () {
		PlayerPrefs.SetInt("ExistingData", 0);
		PlayerPrefs.SetString("CellType", "None");
		PlayerPrefs.SetInt("PlantTutorialLevel", 1);
		PlayerPrefs.SetInt("PlantGameLevel", 1);
		
		PlayerPrefs.Save ();
	}
}
