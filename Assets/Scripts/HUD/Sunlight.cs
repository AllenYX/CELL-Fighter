using UnityEngine;
using System.Collections;

public class Sunlight : MonoBehaviour {
	
	private GameObject lightControllerObject;
	private LightController lightController;

	public Sprite sun_0;
	public Sprite sun_1;
	public Sprite sun_2;
	public Sprite sun_3;
	public Sprite sun_4;
	public Sprite sun_5;
	public Sprite sun_6;
	public Sprite sun_7;
	public Sprite sun_8;
	public Sprite sun_9;
	public Sprite sun_10;
	public Sprite sun_11;
	public Sprite sun_12;
	public Sprite sun_13;
	public Sprite sun_14;
	public Sprite sun_15;
	public Sprite sun_16;

	private SpriteRenderer sunRenderer;

	private void Start () {
		ReferenceObjects ();
	}
	
	private void ReferenceObjects () {
		lightControllerObject = GameObject.FindWithTag ("LightController");
		if (lightControllerObject != null)
			lightController = lightControllerObject.GetComponent <LightController>();
		if (lightController == null)
			Debug.Log ("Cannot find 'Sunlight' script");
		
		sunRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	private void Update () {
		float sunPercent = lightController.SunlightPercent;

		if (sunPercent >= 94.1f)
			sunRenderer.sprite = sun_0;
		else if (sunPercent >= 88.2f)
			sunRenderer.sprite = sun_1;
		else if (sunPercent >= 82.4f)
			sunRenderer.sprite = sun_2;
		else if (sunPercent >= 76.5f)
			sunRenderer.sprite = sun_3;
		else if (sunPercent >= 70.6f)
			sunRenderer.sprite = sun_4;
		else if (sunPercent >= 64.7f)
			sunRenderer.sprite = sun_5;
		else if (sunPercent >= 58.8f)
			sunRenderer.sprite = sun_6;
		else if (sunPercent >= 52.9f)
			sunRenderer.sprite = sun_7;
		else if (sunPercent >= 47.0f)
			sunRenderer.sprite = sun_8;
		else if (sunPercent >= 41.2f)
			sunRenderer.sprite = sun_9;
		else if (sunPercent >= 35.3f)
			sunRenderer.sprite = sun_10;
		else if (sunPercent >= 29.4f)
			sunRenderer.sprite = sun_11;
		else if (sunPercent >= 23.5f)
			sunRenderer.sprite = sun_12;
		else if (sunPercent >= 17.7f)
			sunRenderer.sprite = sun_13;
		else if (sunPercent >= 11.8f)
			sunRenderer.sprite = sun_14;
		else if (sunPercent >= 5.9f)
			sunRenderer.sprite = sun_15;
		else
			sunRenderer.sprite = sun_16;
	}
}
