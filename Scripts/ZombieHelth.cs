using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHelth : MonoBehaviour
{
    public float ZombieHealthValue;
    public float ZombieHealthLeft;


    void Start()
    {
        ZombieHealthLeft = ZombieHealthValue;
        FindAllColliders();
    }

    
    public void ZombieHelthFunction(float damage)
    {
        ZombieHealthLeft -= damage;

        if (ZombieHealthLeft<=0)
        {
            GetComponent<Animator>().enabled = false;
            Rigidbody[] allRigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody Rigid in allRigidbodies)
            {
                Rigid.isKinematic = false;
            }

            GetComponent<NavMeshAgent>().enabled = false;

        }
    }

    public void FindAllColliders()
    {
        Collider[] allColliders = gameObject.GetComponentsInChildren<Collider>();
        foreach(Collider collider in allColliders)
        {
            collider.tag = "Zombie";
        }

        Rigidbody[] allChildRigidboies = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rigid in allChildRigidboies)
        {
            rigid.isKinematic = true;
        }
    }
}
