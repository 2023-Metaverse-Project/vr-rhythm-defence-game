using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEditor.Experimental.GraphView;
using System.Net;
using System;
using UnityEditorInternal;

public class ShootManager : MonoBehaviour
{
    [Header("Points")]
    public GameObject startPoint;
    public GameObject handPoint;
    public float maxDistance;
    public GameObject XROrigin;

    [Header("Skill Speed Setting")]
    public float perfectSpeed;
    public float goodSpeed;
    public float badSpeed;
    private float missileSpeed = 700;

    [Header("Mode")]
    public Skill skill = 0;
    public PressTiming timing = PressTiming.Pass;
    public LayerMask layerMask;

    [SerializeField]
    private List<GameObject> FireballPrefabs = new List<GameObject>();
    [SerializeField]
    private List<GameObject> PlasmaPrefabs = new List<GameObject>();
    [SerializeField]
    private List<GameObject> IcePrefabs = new List<GameObject>();

    private void Update()
    {
        Shoot();
    }
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
        ShootProjectileStartTarget();
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

    private void ShootProjectileHandForward()
    {
        Vector3 handPosition = handPoint.transform.position;
        GameObject projectile = InstantiateSkillPrefab(handPosition);

        Vector3 direction = handPoint.transform.forward.normalized;
        projectile.transform.LookAt(handPosition + direction * 10f);
        projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        projectile.GetComponent<Rigidbody>().AddForce(direction * missileSpeed);
    }

    private void ShootProjectileStartTarget()
    {
        Vector3 spawnPosition = startPoint.transform.position;
        GameObject projectile = InstantiateSkillPrefab(spawnPosition);

        Vector3 targetPosition = new Vector3(XROrigin.transform.position.x, XROrigin.transform.position.y - 50, XROrigin.transform.position.z);
        if (Physics.Raycast(handPoint.transform.position, handPoint.transform.forward, out RaycastHit hitInfo, maxDistance, layerMask))
        {
            targetPosition = hitInfo.transform.position + hitInfo.transform.forward.normalized * 1f;
        }


        Vector3 direction = (targetPosition - spawnPosition).normalized;
        projectile.transform.LookAt(spawnPosition + direction * 20f);
        projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        projectile.GetComponent<Rigidbody>().AddForce(direction * missileSpeed);
    }

    private GameObject InstantiateSkillPrefab(Vector3 spawnPosition)
    {
        GameObject projectile = null;
        if (skill == Skill.Fireball)
        {
            projectile = Instantiate(FireballPrefabs[(int)timing], spawnPosition, Quaternion.identity);
        }
        else if (skill == Skill.Plasma)
        {
            projectile = Instantiate(PlasmaPrefabs[(int)timing], spawnPosition, Quaternion.identity);
        }
        else if (skill == Skill.Ice)
        {
            projectile = Instantiate(IcePrefabs[(int)timing], spawnPosition, Quaternion.identity);
        }

        return projectile;
    }
}
