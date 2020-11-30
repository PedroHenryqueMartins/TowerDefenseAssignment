using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public float maxSpeed = 4.0f;
    //public Transform waypoint;
    public float maxHealth = 100.0f;

    NavMeshAgent agent;
    Animator animator;
    float dividedSpeed = 0.0f;
    bool isDead = false;

    WaypointManager.Path path;
    public int currentWaypoint = 0;
    float currentHealth = 0.0f;
    float deathClipLength;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            //agent.SetDestination(waypoint.position);
            agent.speed = maxSpeed;
        }

        dividedSpeed = 1 / maxSpeed;

        AnimationClip[] animations = animator.runtimeAnimatorController.animationClips;
        if (animations == null || animations.Length <= 0)
        {
            Debug.Log("No animations found");
            return;
        }

        for (int i = 0; i < animations.Length; ++i)
        {
            if (animations[i].name == "Death")
            {
                deathClipLength = animations[i].length;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();

        if (path == null || path.Waypoints == null || path.Waypoints.Count <= currentWaypoint)
        {
            return;
        }

        Transform destination = path.Waypoints[currentWaypoint];
        agent.SetDestination(destination.position);

        if ((transform.position - destination.position).sqrMagnitude < 3.0f * 3.0f)
        {
            currentWaypoint++;
        }
    }


    void UpdateAnimation()
    {
        animator.SetFloat("EnemySpeed", agent.velocity.magnitude * dividedSpeed);
        animator.SetBool("isDead", isDead);
    }

    public void Initialize(WaypointManager.Path p)
    {
        path = p;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0.0f)
        {
            StartCoroutine("Killed");
        }
    }

    public IEnumerator Killed()
    {
        isDead = true;
        agent.Stop();
        yield return new WaitForSeconds(1);
        ServiceLocator.Get<UIManager>().score += 50;
        ServiceLocator.Get<UIManager>().enemiesLeft--;
        ServiceLocator.Get<UIManager>().enemiesKilled++;
        if (ServiceLocator.Get<UIManager>().enemiesLeft == 0)
        {
            ServiceLocator.Get<EnemySpawner>().enemiesPerWave += 1;
        }
        Destroy(gameObject);
    }

}
