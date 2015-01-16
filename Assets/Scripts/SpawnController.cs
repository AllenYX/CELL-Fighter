using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {

	private Vector3 spawnValues = new Vector3 (10f, 7.8f, 0f);
	private Vector3 spawnPosition;
	
	public GameObject water;
	private float spawnWaterTimeMin = 3f;
	private float spawnWaterTimeMax = 6f;
	
	public GameObject testEnemy;
	private float spawnTestEnemyTimeMin = 2f;
	private float spawnTestEnemyTimeMax = 6f;
	
	public GameObject testEnemyBackground;
	private float spawnBackgroundTimeBase = 4f;
	private float spawnBackgroundTimeLong = 10f;
	
	public void StartCoroutines () {
		StartCoroutine ("SpawnWater");
		StartCoroutine ("SpawnTestEnemy");
		StartCoroutine ("SpawnBackgroundObjects", spawnBackgroundTimeLong);
	}
	
	// Spawns water continuously
	public IEnumerator SpawnWater () {
		float spawnWaterTime = 0f;
		while (true) {
			spawnWaterTime = Random.Range (spawnWaterTimeMin, spawnWaterTimeMax);
			yield return new WaitForSeconds(spawnWaterTime);
			
			// Sets position that object will be created at
			spawnPosition = new Vector3 (spawnValues.x, 
			                                     Random.Range (0f, spawnValues.y) + 5.7f,
			                                     spawnValues.z);
			// Creates a new prefabricated object at the desired location
			Instantiate (water, spawnPosition, Quaternion.identity);
		}
	}
	
	public IEnumerator SpawnTestEnemy () {
		float spawnTestEnemyTime = 0f;
		while (true) {
			spawnTestEnemyTime = Random.Range (spawnTestEnemyTimeMin, spawnTestEnemyTimeMax);
			yield return new WaitForSeconds(spawnTestEnemyTime);
			
			spawnPosition = new Vector3 (spawnValues.x, 
			                                     Random.Range (0f, spawnValues.y) + 5.7f,
			                                     spawnValues.z + 1f);
			Instantiate (testEnemy, spawnPosition, Quaternion.identity);
		}
	}
	
	public IEnumerator SpawnBackgroundObjects (float maxSpawnTime) {
		float spawnBackgroundTime;
		while (true) {
			spawnBackgroundTime = Random.Range (spawnBackgroundTimeBase, maxSpawnTime);
			yield return new WaitForSeconds(spawnBackgroundTime);
			
			spawnPosition = new Vector3 (spawnValues.x, 
			                                     Random.Range (0f, spawnValues.y + 1f) + 5.7f,
			                                     spawnValues.z + 2f);
			Instantiate (testEnemyBackground, spawnPosition, Quaternion.identity);
		}
	}
}
