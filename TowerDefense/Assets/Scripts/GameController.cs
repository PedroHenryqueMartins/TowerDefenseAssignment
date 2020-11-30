using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public WaypointManager wpManager;
    public SpawnManager spManager;

    void Start()
    {
        if (spManager != null)
        {
            spManager.Initialize(wpManager);
            spManager.StartSpawners();
            ServiceLocator.Register<SpawnManager>(spManager);
            ServiceLocator.Register<WaypointManager>(wpManager);
        }
        else
        {
            Debug.Log("SpawnManager not initialized");
            return;
        }
    }

    private void Update()
    {
        WinLoseCondition();
    }

    void WinLoseCondition()
    {
        int waypointCheck = ServiceLocator.Get<Enemy>().currentWaypoint;

        if (ServiceLocator.Get<EnemySpawner>().enemiesPerWave == 10 && ServiceLocator.Get<UIManager>().enemiesLeft == 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
        else if (ServiceLocator.Get<Enemy>().currentWaypoint == waypointCheck)
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }


}
