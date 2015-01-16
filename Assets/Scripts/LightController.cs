using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {

	private float sunlightPercent = 0f;
	private float startSunlightPercent; //Percentage of sunlight at start of lighting change
	private float endSunlightPercent; //Percentage of sunlight at end of lighting change
	private float speed = 0.05f;

	private int lightAmount = 2; //0 = low(night), 1 = medium(morning/evening), 2 = high(day)
	private int lightChanger = 1;
	private float baseIntensity = 0f;
	private float incrementIntensity = 0.07f; //Total = increment * 100
	private float waitTime;

	private void Start () {
		StartCoroutines ();
	}

	private void StartCoroutines () {
		StartCoroutine ("ChangeSunlight");
	}

	private void StopCoroutines () {
		StopCoroutine ("ChangeSunlight");
	}
	
	private IEnumerator ChangeSunlight () {
		float tParam = 0f;
		while (true) {
			AdjustLight ();

			//Lerps toward new light value
			while (tParam <= 1.0f) {
				tParam += Time.deltaTime * speed;
				sunlightPercent = Mathf.Lerp (startSunlightPercent, endSunlightPercent, tParam);
				if (Time.timeScale == 1)
					gameObject.light.intensity = baseIntensity + incrementIntensity * sunlightPercent;

				yield return null;
			}
			tParam = 0f;
			SetWaitTime ();
			yield return new WaitForSeconds(waitTime);
		}
	}

	private void AdjustLight () {
		//Randomly changes light value
		startSunlightPercent = sunlightPercent;
		
		if (lightAmount == 2) {
			lightChanger = -1;
			endSunlightPercent = Random.Range (70f,100f);
		} else if (lightAmount == 1) {
			endSunlightPercent = Random.Range (20f, 70f);
		} else {
			lightChanger = 1;
			endSunlightPercent = Random.Range (0f, 20f);
		}
	}

	private void SetWaitTime () {		
		if (lightAmount == 2) {
			waitTime = Random.Range (9.0f, 12.0f);
		} else if (lightAmount == 1) {
			waitTime = Random.Range (2.0f, 3.0f);
		} else {
			waitTime = Random.Range (7.0f, 9.0f);
		}

		lightAmount += lightChanger;
	}
	
	public float SunlightPercent {
		get { return sunlightPercent;}
	}
}
