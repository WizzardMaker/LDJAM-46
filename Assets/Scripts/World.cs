using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
	public static World instance;

	public float worldLength = 200, worldWidth = 80, trackWidth = 3;

	public enum TrainSpeedSetting
	{
		Stop,
		Slow = 5,
		Fast = 20,
	}

	public TrainSpeedSetting trainSpeed = TrainSpeedSetting.Slow;
	public float actualSpeed;

	public List<WorldObject> objects = new List<WorldObject>();

	public List<WorldObject> spawnPrefabs = new List<WorldObject>();
	public List<Obstacle> obstaclesPrefabs = new List<Obstacle>();
	public List<EnemyController> enemiesPrefabs = new List<EnemyController>();

	public float minTimeForSpawn, maxTimeForSpawn;
	public float minTimeForObstacle, maxTimeForObstacle;
	public float minTimeForEnemy, maxTimeForEnemy;

	// Start is called before the first frame update
	void Start() {
		instance = this;
		StartCoroutine(RandomObjectSpawn());
		StartCoroutine(RandomObstacleSpawn());
		StartCoroutine(RandomEnemySpawn());
	}

	public IEnumerator RandomObjectSpawn() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(minTimeForSpawn, maxTimeForSpawn) * (trainSpeed == TrainSpeedSetting.Slow ? 4 : 1));

			if (spawnPrefabs.Count == 0)
				break;

			if (trainSpeed == TrainSpeedSetting.Stop)
				continue;

			WorldObject w = Instantiate(spawnPrefabs[Random.Range(0, spawnPrefabs.Count)], transform);

			float xSpawn = Random.Range(-worldWidth / 2, worldWidth / 2);
			xSpawn += trackWidth * (xSpawn > 0 ? 1 : -1);


			w.transform.position = new Vector3(xSpawn, 0, worldLength / 2);
		}
	}
	public IEnumerator RandomObstacleSpawn() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(minTimeForObstacle, maxTimeForObstacle) * (trainSpeed == TrainSpeedSetting.Slow ? 4 : 1));

			if (obstaclesPrefabs.Count == 0)
				break;

			if (trainSpeed == TrainSpeedSetting.Stop)
				continue;

			Obstacle o = Instantiate(obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Count)], transform);

			o.transform.position = new Vector3(0, 0, worldLength / 2);
		}
	}
	public IEnumerator RandomEnemySpawn() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(minTimeForEnemy, maxTimeForEnemy) * (trainSpeed == TrainSpeedSetting.Slow ? .5f : 1));

			if (obstaclesPrefabs.Count == 0)
				break;


			int waveSize = Random.Range(1,5);
			for (int i = 0; i < waveSize; i++) {
				EnemyController o = Instantiate(enemiesPrefabs[Random.Range(0, enemiesPrefabs.Count)], transform);

				o.transform.position = new Vector3(Random.Range(-worldWidth/2, worldWidth / 2), 0, -worldLength / 2);
			}
		}
	}

	// Update is called once per frame
	void Update() {
		actualSpeed = Mathf.SmoothStep(actualSpeed, (float)trainSpeed, Time.deltaTime * 4 * (trainSpeed == TrainSpeedSetting.Stop ? 2 :1));

        foreach(WorldObject o in objects) {
			o.transform.position -= Vector3.forward * actualSpeed * Time.deltaTime;
		}
    }
}
