using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float maxHP = 100;
    private float currentHP;

    private void Awake()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(target.position);
        agent.speed *= Random.Range(0.8f, 1.5f);

        currentHP = maxHP;
    }

    public void DecreaseHP(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
