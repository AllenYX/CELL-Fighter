using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	//private GameObject gameControllerObject;
	//private GameController gameController;

	public AudioClip clip0;
	private AudioSource audioSource;

	//private int level;
	//private int currentState;
	//private int nextState;

	private void Start () {
		//ReferenceObjects ();

		audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.clip = clip0;
		// Play must be every time the audio clip changes
		audio.Play();

		//level = gameController.Level;
		//currentState = gameController.Level;
	}

	/*private void Update () {
		nextState = gameController.State;

		if (nextState != currentState) {
			currentState = nextState;

			if (level == (int) Scenes.PlantTutorial1) {
				if (currentState == 0)
					//Play new music when going into this new state
			}
		}
	}

	private void ReferenceObjects () {
		gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent <GameController>();
		if (gameController == null)
			Debug.Log ("Cannot find 'GameController' script");
	}*/
}
