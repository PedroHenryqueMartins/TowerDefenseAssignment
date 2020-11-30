using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfWaves;
    public int enemiesPerWave;
    public int timeBetweenWaves;
    public int delayToStart;
    public int pathId;

    int currentWave = 0;

    WaypointManager.Path _path;

    public void Init(WaypointManager.Path path)
    {
        ServiceLocator.Register<EnemySpawner>(this);
        _path = path;
    }

    public void StartSpawner()
    {
        StartCoroutine("WaveSpawn");
    }

    IEnumerator WaveSpawn()
    {
        yield return new WaitForSeconds(delayToStart);

        while (currentWave < numberOfWaves)
        {
            yield return Spawn(currentWave++);
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    IEnumerator Spawn(int waveNumber)
    {
        for (int i = 0; i < enemiesPerWave; ++i)
        {
            GameObject go = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            go.GetComponent<Enemy>().Initialize(_path);
            ServiceLocator.Get<UIManager>().enemiesLeft = enemiesPerWave;
            yield return new WaitForSeconds(.5f);
        }
    }
}
