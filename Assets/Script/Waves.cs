using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{


	public Wave[] waves;

	public Transform spawnPoint;

	public float timeBetweenWaves = 5f;
	private float countdown = 2f;
	private int waveIndex = 0;

	void Update()
	{
		

		if (waveIndex == waves.Length)
		{
			
			this.enabled = false;
			Debug.Log("You win");
		}

		if (countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
			return;
		}

		countdown -= Time.deltaTime;

	}

	IEnumerator SpawnWave()
	{
		

		Wave wave = waves[waveIndex];

		//EnemiesAlive = wave.count;

		for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(1f / wave.rate);
		}

		waveIndex++;
	}

	void SpawnEnemy(GameObject enemy)
	{
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
	}
}
[System.Serializable]
public class Wave
{

    public GameObject enemy;
    public int count;
    public float rate;

}