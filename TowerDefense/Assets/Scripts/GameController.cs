using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
        else
        {
            Debug.Log("SpawnManager not initialized");
            return;
        }
    }

}
