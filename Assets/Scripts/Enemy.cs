using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
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

    [Header("Damages")]
    public float fireballPerfectDamage;
    public float fireballGoodDamage;
    public float fireballBadDamage;

    public float plasmaPerfectDamage;
    public float plasmaGoodDamage;
    public float plasmaBadDamage;

    public float icePerfectDamage;
    public float iceGoodDamage;
    public float iceBadDamage;

    [Header("Enemy Attack Type")]
    public Skill EnemyType;
    public float multiplier;


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
        if (gameObject != null)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Skill attackType = Skill.Fireball;
        float baseDamage = fireballBadDamage;


        if (collision.transform.CompareTag("FireballPerfect"))
        {
            attackType = Skill.Fireball;
            baseDamage = fireballPerfectDamage;
        }
        else if (collision.transform.CompareTag("FireballGood"))
        {
            attackType = Skill.Fireball;
            baseDamage = fireballGoodDamage;
        }
        else if (collision.transform.CompareTag("FireballBad"))
        {
            attackType = Skill.Fireball;
            baseDamage = fireballBadDamage;
        }
        else if (collision.transform.CompareTag("PlasmaPerfect"))
        {
            attackType = Skill.Plasma;
            baseDamage = plasmaPerfectDamage;
        }
        else if (collision.transform.CompareTag("PlasmaGood"))
        {
            attackType = Skill.Plasma;
            baseDamage = plasmaGoodDamage;
        }
        else if (collision.transform.CompareTag("PlasmaBad"))
        {
            attackType = Skill.Plasma;
            baseDamage = plasmaBadDamage;
        }
        else if (collision.transform.CompareTag("IcePerfect"))
        {
            attackType = Skill.Ice;
            baseDamage = icePerfectDamage;
        }
        else if (collision.transform.CompareTag("IceGood"))
        {
            attackType = Skill.Ice;
            baseDamage = iceGoodDamage;
        }
        else if (collision.transform.CompareTag("IceBad"))
        {
            attackType = Skill.Ice;
            baseDamage = iceBadDamage;
        }


        if (EnemyType == attackType)
        {
            DecreaseHP(multiplier * baseDamage);
        }
        else
        {
            DecreaseHP(baseDamage);
        }
    }
}
