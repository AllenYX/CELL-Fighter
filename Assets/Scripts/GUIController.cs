using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
	
	private GameObject gameControllerObject;
	private GameController gameController;

	public GUISkin infoWindowSkin; 
	private Rect windowRect;
	private Rect defaultWindowRect;
	private Rect smallWindowTopRightRect;
	private Rect smallWindowBottomRightRect;
	private Rect mediumWindowBottomRightRect;

	public Texture2D arrowUp;
	public Texture2D arrowLeft;
	public Texture2D levelCompleteImage;
	public Texture2D moveControlsImage;
	public Texture2D attackControlsImage;
	public Texture2D switchViewControlsImage;
	public Texture2D photosynthesisImage;
	public Texture2D respirationImage;

	private float aspectWidth = 1366f;
	private float aspectHeight = 768f;
	private float labelFontSize = 18f;
	private float buttonFontSize = 22f;

	private string[,] gameText = new string[5,10];
	private bool closeWindow = false;

	private int level;
	private int state;

	private void Start () {
		ReferenceObjects ();
		InitializeGameText ();
		InitializeWindowRects ();
		ResizeText ();
		GetState ();
	}

	private void ReferenceObjects () {
		gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent <GameController>();
		if (gameController == null)
			Debug.Log ("Cannot find 'GameController' script");
	}

	private void ResizeText () {
		aspectWidth = GameController.aspectWidth;
		aspectHeight = GameController.aspectHeight;

		float widthRatio = Screen.width / aspectWidth;
		float heightRatio = Screen.height / aspectHeight;
		float resizeRatio = Mathf.Min (widthRatio, heightRatio);
		
		infoWindowSkin.label.fontSize = Mathf.FloorToInt (labelFontSize * resizeRatio);
		infoWindowSkin.button.fontSize = Mathf.FloorToInt (buttonFontSize * resizeRatio);
	}
	
	private void OnGUI () {
		GetState ();
		GUI.skin = infoWindowSkin;

		if (level == (int) Scenes.PlantTutorial1) {
			DrawPlantTutorial1 ();
		} else if (level == (int) Scenes.PlantTutorial2) {
			DrawPlantTutorial2 ();
		}

		// Level Completed state
		if (state < 0) {
			GUI.DrawTexture (new Rect (Screen.width * 0.3f, Screen.height * 0.2f, 
			                           Screen.width * 0.4f, Screen.height * 0.08f), levelCompleteImage);
		}
	}

	private void DrawPlantTutorial1 () {
		if (state == 1) {
			windowRect = GUI.Window(0, defaultWindowRect, WindowFunction, "");
		} else if (state == 2) {
			windowRect = GUI.Window(1, defaultWindowRect, WindowFunction, "");
			GUI.DrawTexture (new Rect (Screen.width * 0.14f, Screen.height * 0.1f, 
			                           Screen.width * 0.03f, Screen.height * 0.18f), arrowUp);
		} else if (state == 3) {
			windowRect = GUI.Window(2, defaultWindowRect, WindowFunction, "");
			GUI.DrawTexture (new Rect (Screen.width * 0.03f, Screen.height * 0.18f, 
			                           Screen.width * 0.03f, Screen.height * 0.18f), arrowUp);
		} else if (state == 5) {
			windowRect = GUI.Window(3, smallWindowTopRightRect, WindowFunction, "");
		} else if (state == 6) {
			windowRect = GUI.Window(4, mediumWindowBottomRightRect, WindowFunction, "");
		} else if (state == 7) {
			windowRect = GUI.Window(5, smallWindowBottomRightRect, WindowFunction, "");
			GUI.DrawTexture (new Rect (Screen.width * 0.42f, Screen.height * 0.03f, 
			                           Screen.width * 0.1f, Screen.height * 0.05f), arrowLeft);
		} else if (state == 8) {
			if (!closeWindow)
				windowRect = GUI.Window(6, smallWindowBottomRightRect, WindowFunction, "");
		}
	}

	private void DrawPlantTutorial2 () {
		if (state == 0) {
			windowRect = GUI.Window(7, defaultWindowRect, WindowFunction, "");
			GUI.DrawTexture (new Rect (Screen.width * 0.28f, Screen.height * 0.1f, 
			                           Screen.width * 0.02f, Screen.height * 0.08f), arrowUp);
		} else if (state == 1) {
			windowRect = GUI.Window(8, smallWindowTopRightRect, WindowFunction, "");
		} else if (state == 2) {
			windowRect = GUI.Window(9, mediumWindowBottomRightRect, WindowFunction, "");
		} else if (state == 3) {
			windowRect = GUI.Window(10, smallWindowBottomRightRect, WindowFunction, "");
		} else if (state == 4) {
			if (!closeWindow)
				windowRect = GUI.Window(11, smallWindowBottomRightRect, WindowFunction, "");
		} else if (state == 5) {
			windowRect = GUI.Window(12, defaultWindowRect, WindowFunction, "");
			GUI.DrawTexture (new Rect (Screen.width * 0.58f, Screen.height * 0.03f, 
			                           Screen.width * 0.1f, Screen.height * 0.05f), arrowLeft);
			closeWindow = false;
		} else if (state == 6) {
			if (!closeWindow)
				windowRect = GUI.Window(13, mediumWindowBottomRightRect, WindowFunction, "");
		}
	}
	
	public void WindowFunction (int windowID) {
		// Plant Tutorial 1
		if (windowID == 0) {
			GUI.DrawTexture (new Rect (windowRect.width * 0.1f, windowRect.height * 0.2f, 
			                           windowRect.width * 0.2f, windowRect.height * 0.38f), moveControlsImage);
			GUI.Label(new Rect(windowRect.width * 0.17f, windowRect.height * 0.7f,
			                   windowRect.width * 0.5f, windowRect.height * 0.3f), "MOVE");

			GUI.DrawTexture (new Rect (windowRect.width * 0.45f, windowRect.height * 0.2f, 
			                           windowRect.width * 0.1f, windowRect.width * 0.15f), attackControlsImage);
			GUI.Label(new Rect(windowRect.width * 0.46f, windowRect.height * 0.7f,
			                   windowRect.width * 0.5f, windowRect.height * 0.3f), "ATTACK");

			GUI.DrawTexture (new Rect (windowRect.width * 0.7f, windowRect.height * 0.4f, 
			                           windowRect.width * 0.2f, windowRect.height * 0.16f), switchViewControlsImage);
			GUI.Label(new Rect(windowRect.width * 0.73f, windowRect.height * 0.7f,
			                   windowRect.width * 0.5f, windowRect.height * 0.3f), "SWITCH VIEW");
			ContinueButtonDefault ();
		} else if (windowID == 1){
			GUI.Label(new Rect(windowRect.width * 0.08f, windowRect.height * 0.15f,
			                   windowRect.width * 0.84f, windowRect.height), gameText [0, 0]);
			ContinueButtonDefault ();
		} else if (windowID == 2) {
			GUI.Label(new Rect(windowRect.width * 0.08f, windowRect.height * 0.15f,
			                   windowRect.width * 0.84f, windowRect.height), gameText [0, 1]);
			ContinueButtonDefault ();
		} else if (windowID == 3) {
			GUI.Label(new Rect(windowRect.width * 0.14f, windowRect.height * 0.24f,
			                   windowRect.width * 0.76f, windowRect.height), gameText [0, 2]);
		} else if (windowID == 4) {
			GUI.Label(new Rect(windowRect.width * 0.1f, windowRect.height * 0.15f,
			                   windowRect.width * 0.8f, windowRect.height), gameText [0, 3]);
			ContinueButtonMedium ();
		} else if (windowID == 5) {
			GUI.Label(new Rect(windowRect.width * 0.1f, windowRect.height * 0.2f,
			                   windowRect.width * 0.8f, windowRect.height), gameText [0, 4]);
			ContinueButtonSmall ();
		} else if (windowID == 6) {
			GUI.Label(new Rect(windowRect.width * 0.1f, windowRect.height * 0.2f,
			                   windowRect.width * 0.8f, windowRect.height), gameText [0, 5]);
			CloseButtonSmall ();
		} //Plant Tutorial 2 
		else if (windowID == 7) {
			GUI.Label(new Rect(windowRect.width * 0.08f, windowRect.height * 0.15f,
			                   windowRect.width * 0.84f, windowRect.height), gameText [1, 0]);
			ContinueButtonDefault ();
		} else if (windowID == 8) {	
			GUI.Label(new Rect(windowRect.width * 0.1f, windowRect.height * 0.2f,
			                   windowRect.width * 0.8f, windowRect.height), gameText [1, 1]);
		} else if (windowID == 9) {	
			GUI.Label(new Rect(windowRect.width * 0.1f, windowRect.height * 0.15f,
			                   windowRect.width * 0.8f, windowRect.height), gameText [1, 2]);
			GUI.DrawTexture (new Rect (windowRect.width * 0.1f, windowRect.height * 0.5f, 
			                           windowRect.width * 0.7f, windowRect.height * 0.3f), photosynthesisImage);
			ContinueButtonMedium ();
		} else if (windowID == 10) {	
			GUI.Label(new Rect(windowRect.width * 0.1f, windowRect.height * 0.2f,
			                   windowRect.width * 0.8f, windowRect.height), gameText [1, 3]);
		} else if (windowID == 11) {	
			GUI.Label(new Rect(windowRect.width * 0.1f, windowRect.height * 0.2f,
			                   windowRect.width * 0.8f, windowRect.height), gameText [1, 4]);
			CloseButtonSmall ();
		} else if (windowID == 12) {	
			GUI.Label(new Rect(windowRect.width * 0.08f, windowRect.height * 0.15f,
			                   windowRect.width * 0.84f, windowRect.height), gameText [1, 5]);
			GUI.DrawTexture (new Rect (windowRect.width * 0.12f, windowRect.height * 0.5f, 
			                           windowRect.width * 0.75f, windowRect.height * 0.3f), respirationImage);
			ContinueButtonDefault ();
		} else if (windowID == 13) {	
			GUI.Label(new Rect(windowRect.width * 0.1f, windowRect.height * 0.15f,
			                   windowRect.width * 0.8f, windowRect.height), gameText [1, 6]);
			CloseButtonMedium ();
		}
	}

	private void InitializeGameText () {
		gameText [0, 0] = "The CELL MEMBRANE is the a outer layer of the plant cell. It acts as a skin by providing " +
			"a barrier between the cell and its environment. It is also selectively permeable, meaning it can control " +
			"what goes in and out of the cell. \n\n If the membrane takes too much damage, then your cell will die.";
		gameText [0, 1] = "The CELL WALL is only found on plant cells, and is on the outermost layer. It provides the " +
			"cell with structural support and protection. \n\nThe cell wall shields your membrane from outside damage, " +
			"but this lowers its rigidity. Rigidity can be restored by soring more water.";
		gameText [0, 2] = "Now that you know about the outer structure of the cell, press SPACE to switch views " +
			"to the inner structure.";
		gameText [0, 3] = "Inside the cell are the organelles, which act as the cells organs. \n\nThe VACUOLE " +
			"of the plant cell is a very large sac that stores mainly WATER, but can also hold waste and other " +
			"substances.";
		gameText [0, 4] = "All collected water will be stored in the vacuole, but too much can cause cytolysis " +
						"and damage your cell.";
		gameText [0, 5] = "Switch to the game view and fill up your vacuole with water to complete this level.";

		gameText [1, 0] = "All cells need energy in order to live and perform tasks. To use energy, cells rely " +
			"on the molecule ATP (adenosine triphosphate). \n\nATP acts as an energy currency and will be used " +
			"up regularly to keep the cell alive. Too much ATP will overload your cell, making it slowly lose ATP " +
			"until it becomes balanced.";
		gameText [1, 1] = "When your cell is out of ATP, its membrane will keep getting weaker until it dies. In " +
			"order to create ATP, the plant cell relies on two specialized organelles. (Switch view)";
		gameText [1, 2] = "The CHLOROPLAST is only found in plant cells, and its function is to capture energy " +
			"from the sun to perform PHOTOSYNTHESIS."; 
		gameText [1, 3] = "Click on the VACUOLE to drag water into the CHLOROPLAST. It requires at least 6 water " +
			"to photosynthesize.";
		gameText [1, 4] = "CARBON DIOXIDE is plentiful here so the only thing left to do is to wait for SUNLIGHT " +
			"to be gathered in order for photosynthesis to take effect.";
		gameText [1, 5] = "The GLUCOSE that your cell just produced is the main ingredient in CELLULAR RESPIRATION; " +
			"the reaction used for producing ATP. The creation of ATP is also the primary task of the MITOCHONDRIA.";  
		gameText [1, 6] = "When your mitochondria has enough OXYGEN, drag glucose from the chloroplast into the " +
			"mitochondria to create ATP. \n\nSince your cell is not very efficient, it loses the by-products of " +
			"these reactions and must gather new resources every time.";
	}

	private void InitializeWindowRects () {
		defaultWindowRect = new Rect(Screen.width * 0.2f, Screen.height * 0.2f,
		                             Screen.width * 0.6f, Screen.height * 0.4f);
		smallWindowTopRightRect = new Rect(Screen.width * 0.65f, Screen.height * 0.06f,
		                                   Screen.width * 0.3f, Screen.height * 0.2f);
		smallWindowBottomRightRect = new Rect(Screen.width * 0.65f, Screen.height * 0.76f,
		                                      Screen.width * 0.3f, Screen.height * 0.2f);
		mediumWindowBottomRightRect = new Rect(Screen.width * 0.58f, Screen.height * 0.68f,
		                                       Screen.width * 0.4f, Screen.height * 0.3f);
	}

	private void ContinueButtonDefault () {
		if (GUI.Button (new Rect (windowRect.width * 0.85f, windowRect.height * 0.87f, 
		                          windowRect.width * 0.14f, 30), "CONTINUE"))
			gameController.State ++;
	}

	private void ContinueButtonMedium () {
		if (GUI.Button (new Rect (windowRect.width * 0.78f, windowRect.height * 0.83f, 
		                          windowRect.width * 0.2f, 30), "CONTINUE"))
			gameController.State ++;
	}

	private void ContinueButtonSmall () {
		if (GUI.Button (new Rect (windowRect.width * 0.7f, windowRect.height * 0.75f, 
		                          windowRect.width * 0.3f, 30), "CONTINUE"))
			gameController.State ++;
	}

	private void CloseButtonMedium () {
		if (GUI.Button (new Rect (windowRect.width * 0.91f, windowRect.height * 0.83f, 
		                          windowRect.width * 0.1f, 30), "X"))
			closeWindow = true;
	}

	private void CloseButtonSmall () {
		if (GUI.Button (new Rect (windowRect.width * 0.9f, windowRect.height * 0.75f, 
		                          windowRect.width * 0.1f, 30), "X"))
			closeWindow = true;
	}

	private void GetState () {
		level = gameController.Level;
		state = gameController.State;
	}
}
