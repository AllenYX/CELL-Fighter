using UnityEngine;
using System.Collections;

public class OrganellePausedBobble : MonoBehaviour {

	private const float gameYHeight = 10f;

	private Vector3 startPosition;
	private float startY;
	private Vector3 currentPosition;

	private float bobbleTime;
	private float minBobbleTime = 3f;
	private float maxBobbleTime = 5f;
	private float deltaYPosition;
	public float minDeltaYPosition = 0.02f;
	public float maxDeltaYPosition = 0.05f;

	private float progressTime = 0f;
	private float timeAtLastFrame = 0f;
	private float timeAtCurrentFrame = 0f;
	private float deltaTime = 0f;

	private float pausedTime = 0f;
	private float timeAtPause = 0f;
	private float timeAtLastFramePause = 0f;
	private float timeAtCurrentFramePause = 0f;
	private float deltaTimePause = 0f;

	private bool goingDown = true;

	private void Start () {
		startPosition = transform.position;
		currentPosition = startPosition;
		startY = startPosition.y;
		StartBobble ();
	}

	// Makes transform move up and down
	private void Update () {
		// Moves when in-game time is paused, excluding during pause menu
		if (Time.timeScale == 0 && !PauseController.gamePaused) {
			// Gets real in-game time, minus pauses in order to "animate" when paused
			timeAtCurrentFrame = Time.realtimeSinceStartup - pausedTime;
			deltaTime = timeAtCurrentFrame - timeAtLastFrame;
			timeAtLastFrame = timeAtCurrentFrame;
			progressTime += deltaTime;

			// Cycles between up and down motions
			if (goingDown) {
				currentPosition.y = startY - (progressTime / bobbleTime) * deltaYPosition;

				if (progressTime >= bobbleTime) {
					goingDown = false;
					progressTime = 0f;
				}
			} else {
				currentPosition.y = startY - (1f - progressTime / bobbleTime) * deltaYPosition;

				if (progressTime >= bobbleTime) {
					goingDown = true;
					progressTime = 0f;
					StartBobble ();
				}
			}

			transform.position = currentPosition;
			timeAtPause = Time.realtimeSinceStartup;
		} else {
			// Needs to keep track of amount of pause screen time, since 
			// when coming out of the pause state ,basing calculations 
			// purely on real-time will not retain previous position and movement
			timeAtCurrentFramePause = Time.realtimeSinceStartup - timeAtPause;
			deltaTimePause = timeAtCurrentFramePause - timeAtLastFramePause;
			timeAtLastFramePause = timeAtCurrentFramePause;
			pausedTime += deltaTimePause;
		}
	}

	private void StartBobble () {
		bobbleTime = Random.Range (minBobbleTime, maxBobbleTime);
		deltaYPosition = 10f * Random.Range (minDeltaYPosition, maxDeltaYPosition);
	}
}
