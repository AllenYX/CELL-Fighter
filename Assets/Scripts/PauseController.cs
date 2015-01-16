using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {
	
	public static bool gamePaused = false;

	public Camera camPause;
		
	public Texture2D backgroundFilter;
	private Rect filterRect;
	
	public GUISkin pauseMenuSkin;
	public Texture2D menuBackground;
	private Rect menuRect;
	private float menuRectX = 0.35f;
	private float menuRectY = 0.3f;
	private float menuRectWidth = 0.3f;
	private float menuRectHeight = 0.4f;

	private Rect labelRect;
	private float labelRectX = 0.2f;
	private float labelRectY = 0.1f;
	private float labelRectWidth = 0.6f;
	private float labelRectHeight = 0.2f;

	private Rect buttonRect0;
	private Rect buttonRect1;
	private Rect buttonRect2;
	private Rect buttonRect3;
	private float buttonRectX = 0.2f;
	private float buttonRectY = 0.35f;
	private float buttonRectYOffset = 0.14f;
	private float buttonRectWidth = 0.6f;
	private float buttonRectHeight = 0.1f;

	private float labelFontSize = 40f;
	private float buttonFontSize = 20f;
	private int resizedLabelFontSize;
	private int resizedButtonFontSize;
	private float aspectHeight = 768f;
	
	private bool showMenu = false;

	// Use this for initialization
	private void Start () {
		camPause.enabled = false;

		filterRect = new Rect (-20f, -20f, Screen.width * 1.1f, Screen.height * 1.1f);
		menuRect = new Rect (Screen.width * menuRectX, Screen.height * menuRectY,
		                     Screen.width * menuRectWidth, Screen.height * menuRectHeight);
		labelRect = new Rect (menuRect.x + menuRect.width * labelRectX,
		                      menuRect.y + menuRect.height * labelRectY,
		                      menuRect.width * labelRectWidth, menuRect.height * labelRectHeight);
		buttonRect0 = new Rect (menuRect.x + menuRect.width * buttonRectX,
		                        menuRect.y + menuRect.height * buttonRectY,
		                        menuRect.width * buttonRectWidth, menuRect.height * buttonRectHeight);
		buttonRect1 = new Rect (menuRect.x + menuRect.width * buttonRectX,
		                        menuRect.y + menuRect.height * (buttonRectY + buttonRectYOffset),
		                        menuRect.width * buttonRectWidth, menuRect.height * buttonRectHeight);
		buttonRect2 = new Rect (menuRect.x + menuRect.width * buttonRectX,
		                        menuRect.y + menuRect.height * (buttonRectY + buttonRectYOffset * 2f),
		                        menuRect.width * buttonRectWidth, menuRect.height * buttonRectHeight);
		buttonRect3 = new Rect (menuRect.x + menuRect.width * buttonRectX,
		                        menuRect.y + menuRect.height * (buttonRectY + buttonRectYOffset * 3f),
		                        menuRect.width * buttonRectWidth, menuRect.height * buttonRectHeight);

		aspectHeight = Screen.height / aspectHeight;
		resizedLabelFontSize = Mathf.FloorToInt (labelFontSize * aspectHeight);
		resizedButtonFontSize = Mathf.FloorToInt (buttonFontSize * aspectHeight);
	}

	// Update is called once per frame
	private void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			TogglePause ();
		}
	}
	
	private void OnGUI () {
		if (showMenu) {
			GUI.depth = 0;
			GUI.skin = pauseMenuSkin;
			GUI.skin.label.fontSize = resizedLabelFontSize;
			GUI.skin.button.fontSize = resizedButtonFontSize;
			
			GUI.DrawTexture (filterRect, backgroundFilter);
			GUI.DrawTexture (menuRect, menuBackground);
			GUI.Label (labelRect, "PAUSED");

			if (GUI.Button(buttonRect0, "RESUME GAME"))
				TogglePause ();
			if (GUI.Button(buttonRect1, "OPTIONS"))
				print("click options");
			if (GUI.Button(buttonRect2, "EXIT LEVEL")) {
				gamePaused = false;
				Application.LoadLevel ((int) Scenes.LevelSelect);
			}
			if (GUI.Button(buttonRect3, "EXIT TO MENU")) {
				gamePaused = false;
				Application.LoadLevel ((int) Scenes.Menu);
			}
		}
	}

	private void TogglePause () {
		// When paused, a camera with a higher depth will be enabled.
		// This camera will look at an empty game object with a collider
		// that covers the entire view, effectively disabling background
		// collider detection.
		camPause.enabled = !camPause.enabled;

		if (!showMenu) { // Open pause menu
			gamePaused = true;
			if (GameController.gameTimeRunning)
				PauseTime ();
		} else { // Close pause menu
			gamePaused = false;
			if (GameController.gameTimeRunning)
				ResumeTime ();
		}

		showMenu = !showMenu;
	}
	
	private void PauseTime () { 
		//Pseudo pauses the game by stopping all timers that run on timescale
		Time.timeScale = 0;
	}
	
	private void ResumeTime () { 
		Time.timeScale = 1;
	}
}
