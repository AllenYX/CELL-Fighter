using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {

	public GUISkin buttonSkin;
	private float aspectHeight = 768f;
	private float defaultFontSize = 26f;
	private int level;

	private void Start () {
		aspectHeight = 768 / Screen.height;
		level = PlayerPrefs.GetInt ("PreviousLevel");
	}
	
	private void OnGUI () {
		GUI.skin = buttonSkin;
		GUI.skin.button.fontSize = Mathf.FloorToInt (defaultFontSize * aspectHeight);
		print (Application.loadedLevel);
		
		if (GUI.Button(new Rect(Screen.width * 0.3f, Screen.height * 0.55f, Screen.width * 0.15f, Screen.height * 0.05f),
		               "Retry"))
			Application.LoadLevel (level);
		if (GUI.Button(new Rect(Screen.width * 0.45f, Screen.height * 0.55f, Screen.width * 0.15f, Screen.height * 0.05f),
		               "Quit"))
			Application.LoadLevel ((int) Scenes.LevelSelect);
	}
}
