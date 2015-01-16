using UnityEngine;
using System.Collections;

public class LevelSelectController : MonoBehaviour {

	public int level;
	public Sprite iconNormal;
	public Sprite iconHover;
	public GameObject levelNumber;

	private SpriteRenderer iconSpriteRenderer;
	private TextMesh levelNumberTextMesh;

	private void Start () {
		iconSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		levelNumberTextMesh = levelNumber.GetComponent<TextMesh>();
	}

	private void OnMouseOver () {
		iconSpriteRenderer.sprite = iconHover;
		levelNumberTextMesh.color = Color.red;
	}

	private void OnMouseExit () {
		iconSpriteRenderer.sprite = iconNormal;
		levelNumberTextMesh.color = Color.white;
	}

	private void OnMouseDown () {
		if (level == 0) 
			Application.LoadLevel ((int) Scenes.Level0);
		else if (level == 1)
			Application.LoadLevel ((int) Scenes.PlantTutorial1);
		else if (level == 2)
			Application.LoadLevel ((int) Scenes.PlantTutorial2);
	}
}
