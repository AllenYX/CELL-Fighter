using UnityEngine;
using System.Collections;

public class ExitToMenuButton : MonoBehaviour {

	private TextMesh buttonText;

	private void Start () {
		buttonText = gameObject.GetComponent<TextMesh>();
	}

	private void OnMouseEnter () {
		buttonText.color = Color.yellow;
	}

	private void OnMouseExit () {
		buttonText.color = Color.white;
	}

	private void OnMouseDown () {
		Application.LoadLevel ((int) Scenes.Menu);
	}
}
