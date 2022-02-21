using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    private NavMeshAgent ZombieNavmesh;
    private Animator ZombieAnımator;
    private float Distance;

    void Start()
    {
        ZombieNavmesh = GetComponent<NavMeshAgent>();
        ZombieAnımator = GetComponent<Animator>();
    }

    
    void Update()
    {
        ZombieMovement();
    }

    private void ZombieMovement()
    {
        Distance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        if (ZombieNavmesh.enabled)
        {
            if (Distance > 10)
            {
                ZombieAnımator.SetFloat("Distance", Distance);
                ZombieNavmesh.isStopped = true;
            }
            else if (Distance <= 10 && Distance > ZombieNavmesh.stoppingDistance)
            {
                ZombieAnımator.SetFloat("Distance", Distance);

                ZombieNavmesh.isStopped = false;
                ZombieNavmesh.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
            }
            else if (Distance <= ZombieNavmesh.stoppingDistance)
            {
                ZombieAnımator.SetFloat("Distance", Distance);

                ZombieNavmesh.isStopped = true;

            }
        }

    }
    public void ZombieAttack()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth playerHealtt = Player.GetComponent<PlayerHealth>();
        playerHealtt.PlayerHealthFunction(5);
    }
}
