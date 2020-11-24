using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public float damage;
    public float range = 30.0f;
    public float fireRate;

    GameObject currentTarget = null;

    public GameObject muzzle;
    public GameObject cannonBase;

    float nextTimeToFire = 0.0f;
    float dividedFireRate = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        dividedFireRate = 1.0f / fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();

        if (Time.time >- nextTimeToFire)
        {
            nextTimeToFire = Time.time + dividedFireRate;
            Shoot();
        }
    }


    void UpdateTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        GameObject target = GetClosestTarget(enemies);

        if (target != null)
        {
            if ((target.transform.position - muzzle.transform.position).sqrMagnitude < range * range)
            {
                currentTarget = target;
                cannonBase.transform.LookAt(currentTarget.transform);
            }
        }
        else
        {
            currentTarget = null;
        }
    }

    GameObject GetClosestTarget(Enemy[] enemies)
    {
        GameObject retEnemy = null;
        for (int i = 0; i < enemies.Length; ++i)
        {
            if (enemies[i].GetHealth() > 0.0f)
            {
                if (retEnemy == null)
                {
                    retEnemy = enemies[i].gameObject;
                }
                else if ((enemies[i].transform.position - muzzle.transform.position).sqrMagnitude < (retEnemy.transform.position - muzzle.transform.position).sqrMagnitude)
                {
                    retEnemy = enemies[i].gameObject;
                }
            }
        }

        return retEnemy;

    }

    void Shoot()
    {
        if (currentTarget != null)
        {
            currentTarget.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

}
