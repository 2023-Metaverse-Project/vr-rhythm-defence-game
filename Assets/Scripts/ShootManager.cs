using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEditor.Experimental.GraphView;
using System.Net;
using System;

public class ShootManager : MonoBehaviour
{
    [Header("Points")]
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject handPoint;

    [Header("Skill Speed Setting")]
    public float perfectSpeed;
    public float goodSpeed;
    public float badSpeed;
    private float missileSpeed = 700;

    [Header("Mode")]
    public Skill skill = 0;
    public PressTiming timing = PressTiming.Pass;

    [SerializeField]
    private List<GameObject> FireballPrefabs = new List<GameObject>();
    [SerializeField]
    private List<GameObject> ThunderPrefabs = new List<GameObject>();
    

    public void Shoot()
    {
        switch (timing)
        {
            case PressTiming.Perfect:
                ShootPerfect();
                break;

            case PressTiming.Good:
                ShootGood();
                break;

            case PressTiming.Bad:
                ShootBad();
                break;

            case PressTiming.Miss:
            case PressTiming.Pass:
                break;
        }

        timing = PressTiming.Pass;
    }

    public void SetTiming(PressTiming pressTiming)
    {
        timing = pressTiming;
    }

    public void SetSkill(Skill skill)
    {
        this.skill = skill;
    }

    private void ShootPerfect()
    {
        missileSpeed = perfectSpeed;
        ShootProjectileStartEnd();
    }

    private void ShootGood()
    {
        missileSpeed = goodSpeed;
        ShootProjectileHandForward();
    }

    private void ShootBad()
    {
        missileSpeed = badSpeed;
        ShootProjectileHandForward();
    }

    private void ShootProjectileStartEnd()
    {
        Vector3 spawnPosition = startPoint.transform.position;
        GameObject projectile = null;

        if (skill == Skill.Fireball)
        {
            projectile = Instantiate(FireballPrefabs[(int)timing], spawnPosition, Quaternion.identity);
        }
        

        Vector3 direction = endPoint.transform.position - startPoint.transform.position;
        projectile.transform.LookAt(spawnPosition + direction * 10f);
        projectile.GetComponent<Rigidbody>().AddForce(direction * missileSpeed);
    }

    private void ShootProjectileHandForward()
    {
        Vector3 spawnPosition = handPoint.transform.position;
        GameObject projectile = null;

        if (skill == Skill.Fireball)
        {
            projectile = Instantiate(FireballPrefabs[(int)timing], spawnPosition, Quaternion.identity);
        }

        Vector3 direction = handPoint.transform.forward;
        projectile.transform.LookAt(spawnPosition + direction * 10f);
        projectile.GetComponent<Rigidbody>().AddForce(direction * missileSpeed);
    }
}
