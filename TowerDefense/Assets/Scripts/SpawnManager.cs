using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public List<EnemySpawner> enemySpawners;

    WaypointManager waypointManager;

    public void Initialize(WaypointManager wpManager)
    {
        waypointManager = wpManager;
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            enemySpawner.Init(waypointManager.GetPath(enemySpawner.pathId));
        }
    }

    public void StartSpawners()
    {
        foreach (EnemySpawner eSpawner in enemySpawners)
        {
            eSpawner.StartSpawner();
        }
    }
}
