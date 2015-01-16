using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static float aspectWidth = 1366f;
	public static float aspectHeight = 768f;
	public static bool gameTimeRunning = true;

	private GameObject spawnControllerObject;
	private SpawnController spawnController;
	private GameObject cellControllerObject;
	private CellController cellController;
	
	public Camera camIn;
	public Camera camOut;
	public Camera camPlayer;
	private bool camLocked = false;

	private int level = 0;
	private int state = 0;
	private float finishLevelPause = 3f;

	private bool gameOver = false;

	private void Start () {
		level = Application.loadedLevel;
		ReferenceObjects ();

		camIn.enabled = false;
		camOut.enabled = true;
		camPlayer.enabled = false;

		ResumeTime ();
	}

	private void ReferenceObjects () {
		spawnControllerObject = GameObject.FindWithTag ("SpawnController");
		if (spawnControllerObject != null)
			spawnController = spawnControllerObject.GetComponent <SpawnController>();
		if (spawnController == null)
			Debug.Log ("Cannot find 'SpawnController' script");

		cellControllerObject = GameObject.FindWithTag ("CellController");
		if (cellControllerObject != null)
			cellController = cellControllerObject.GetComponent <CellController>();
		if (cellController == null)
			Debug.Log ("Cannot find 'CellController' script");
	}
	
	private void Update() {
		// Checks for game over condition, or pressed 'C' to change in-game screens
		if (gameOver) {
			StartCoroutine ("DisplayGameOverScreen");
		} else if (Input.GetKeyDown(KeyCode.Space) &&
		           !PauseController.gamePaused &&
		           !camLocked) {
			SwitchCamera ();
		}

		if (state == 0) {
			if (level == (int) Scenes.PlantTutorial1) { // Plant Tutorial 1
				StartCoroutine("TutorialPlant1");
			} else if (level == (int) Scenes.PlantTutorial2) { // Plant Tutorial 2
				StartCoroutine("TutorialPlant2");
			} else if (level == (int) Scenes.Level0) {
				state = 1;
				spawnController.StartCoroutines ();
			}
		}
	}

	private IEnumerator TutorialPlant1 () {
		while (true) {
		 	// Do stuff based on state
			if (state == 0) {
				spawnController.StartCoroutine ("SpawnWater");
				PauseTime ();
				camLocked = true;
				state ++;
			} else if (state == 4) {
				ResumeTime ();
				camLocked = false;
				state ++;
			} else if (state == 5) {
				if (camIn.enabled)
					state ++;
			} else if (state == 6) {
				camLocked = true;
			} else if (state == 8) {
				camLocked = false;
				if (cellController.playerCell.VacuoleWater >= cellController.playerCell.VacuoleWaterLimit) {
					state = -1;
				}
			} else if (state == -1) {
				camLocked = true;
				yield return new WaitForSeconds (finishLevelPause);
				Application.LoadLevel ((int) Scenes.PlantTutorial2);
			}

			yield return null;
		}
	}

	private IEnumerator TutorialPlant2 () {
		while (true) {
			if (state == 0) {
				camLocked = true;
			} else if (state == 1) {
				cellController.playerCell.Atp = 0;
				camLocked = false;
				if (camIn.enabled) {
					spawnController.StartCoroutine ("SpawnWater");
					cellController.StopCoroutine("ConsumeAtp");
					state ++;
				}
			} else if (state == 3) {
				if (cellController.playerCell.ChloroplastWater >= 6) {
					state ++;
				}
			} else if (state == 4) {
				if (cellController.playerCell.Glucose > 0) {
					state ++;
				}
			} else if (state == 6) {
				camLocked = false;
				if (cellController.playerCell.Atp > 0) {
					state = -1;
				}
			} else if (state == -1) {
				//Needed otherwise yield return 1s stalls due to inner screen being paused
				camLocked = true;
				SwitchCamera ();
				state = -2;
				yield return new WaitForSeconds (3f);
				Application.LoadLevel ((int) Scenes.LevelSelect);
				yield break;
			}

			yield return null;
		}
	}

	private void SwitchCamera () {
		//Pauses time for close camera cell view, resumes for far view
		if (camOut.enabled) {
			camOut.GetComponent<AudioListener>().enabled = false;
			camIn.GetComponent<AudioListener>().enabled = true;
			PauseTime ();
		} else {
			camIn.GetComponent<AudioListener>().enabled = false;
			camOut.GetComponent<AudioListener>().enabled = true;
			ResumeTime ();
		}
		
		camIn.enabled = !camIn.enabled;
		camOut.enabled = !camOut.enabled;
		camPlayer.enabled = !camPlayer.enabled;
	}

	private void PauseTime () { 
		//Pseudo pauses the game by stopping all timers that run on timescale
		Time.timeScale = 0;
		gameTimeRunning = false;
	}
	
	private void ResumeTime () { 
		Time.timeScale = 1;
		gameTimeRunning = true;
	}

	private IEnumerator DisplayGameOverScreen () {
		// Game over screen needs to be after player death animation
		yield return new WaitForSeconds (4f);
		PlayerPrefs.SetInt ("PreviousLevel", Application.loadedLevel);
		Application.LoadLevel ((int) Scenes.GameOver);
	}

	public bool GameOver {
		set {gameOver = value;}
	}

	public int Level {
		get {return level;}
	}

	public int State {
		get {return state;}
		set {state = value;}
	}
}

public enum Scenes {
	Menu = 0,
	CharacterSelect = 1,
	LevelSelect = 2,
	GameOver = 3,
	PlantTutorial1 = 4,
	PlantTutorial2 = 5,
	Level0 = 6
}
