using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent; 
    private EnemyAnimatorController animatorController;

    [SerializeField]
    private float maxHP = 100;
    [SerializeField]
    private float speed;
    private float currentHP;

    [Header("Damages")]
    public SkinnedMeshRenderer skinnedMeshRenderer;

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
    public Skill enemyType;
    public float multiplier;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animatorController = GetComponent<EnemyAnimatorController>();

        agent.SetDestination(new Vector3(-3, 8, -124));
        agent.speed = speed;

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
        StopAllCoroutines();
        agent.speed = 0;
        skinnedMeshRenderer.material.color = Color.red;
        animatorController.DieAnim();
        if (gameObject != null)
            Destroy(gameObject, 2f);
    }

    private IEnumerator HitNormalEffect()
    {
        Color originalColor = skinnedMeshRenderer.material.color;
        float originalSpeed = agent.speed;

        skinnedMeshRenderer.material.color = Color.yellow;
        agent.speed = 0;
        animatorController.GetHitAnim();
        yield return new WaitForSeconds(0.4f);
        skinnedMeshRenderer.material.color = originalColor;
        agent.speed = originalSpeed;
    }

    private IEnumerator HitCriticalEffect()
    {
        Color originalColor = skinnedMeshRenderer.material.color;
        float originalSpeed = agent.speed;

        skinnedMeshRenderer.material.color = Color.red;
        agent.speed = 0;
        animatorController.GetHitAnim();
        yield return new WaitForSeconds(0.4f);
        skinnedMeshRenderer.material.color = originalColor;
        agent.speed = originalSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Skill attackType = Skill.Fireball;
        float baseDamage = fireballBadDamage;


        if (collision.gameObject.layer ==  LayerMask.NameToLayer("FireballPerfect"))
        {
            attackType = Skill.Fireball;
            baseDamage = fireballPerfectDamage;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("FireballGood"))
        {
            attackType = Skill.Fireball;
            baseDamage = fireballGoodDamage;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("FireballBad"))
        {
            attackType = Skill.Fireball;
            baseDamage = fireballBadDamage;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("PlasmaPerfect"))
        {
            attackType = Skill.Plasma;
            baseDamage = plasmaPerfectDamage;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("PlasmaGood"))
        {
            attackType = Skill.Plasma;
            baseDamage = plasmaGoodDamage;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("PlasmaBad"))
        {
            attackType = Skill.Plasma;
            baseDamage = plasmaBadDamage;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("IcePerfect"))
        {
            attackType = Skill.Ice;
            baseDamage = icePerfectDamage;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("IceGood"))
        {
            attackType = Skill.Ice;
            baseDamage = iceGoodDamage;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("IceBad"))
        {
            attackType = Skill.Ice;
            baseDamage = iceBadDamage;
        }

        StopAllCoroutines();
        if (enemyType == attackType)
        {
            //Debug.Log("Critical" + enemyType + attackType);
            StartCoroutine(HitCriticalEffect());
            DecreaseHP(multiplier * baseDamage);
        }
        else
        {
            //Debug.Log("Normal" + enemyType + attackType);
            StartCoroutine(HitNormalEffect());
            DecreaseHP(baseDamage);
        }
    }
}
