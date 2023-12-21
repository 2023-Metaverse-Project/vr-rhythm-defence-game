using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEditor.Experimental.GraphView;
using System.Net;
using System;
using UnityEditorInternal;

public class DomboShootManager : MonoBehaviour
{
    [Header("Points")]
    public GameObject startPoint;
    public GameObject handPoint;
    public float maxDistance;
    public GameObject XROrigin;

    [Header("Skill Speed Setting")]
    public float perfectSpeed;
    private float missileSpeed = 700;

    [Header("Mode")]
    public Skill skill = 0;

    [SerializeField]
    private GameObject FireballPrefab;
    [SerializeField]
    private GameObject PlasmaPrefabs;
    [SerializeField]
    private GameObject IcePrefabs;

    public void comboShoot(int comboLevel)
    {
        StartCoroutine(ComboShootRepeatedly(comboLevel));
    }

    private IEnumerator ComboShootRepeatedly(int comboLevel)
    {
        int shooting = comboLevel * 10;
        while (shooting >= 0)
        {
            missileSpeed = perfectSpeed;
            ShootProjectileStartTarget();
            yield return new WaitForSeconds(0.2f);
            shooting -= 1;
            skill = (Skill)UnityEngine.Random.Range(0, 3);
        }
    }

    private void ShootProjectileStartTarget()
    {
        int childIndex = UnityEngine.Random.Range(0, startPoint.transform.childCount);
        GameObject child = transform.GetChild(childIndex).gameObject;
        

        Vector3 spawnPosition = child.transform.position;
        GameObject projectile = InstantiateSkillPrefab(spawnPosition);

        Vector3 targetPosition = new Vector3(XROrigin.transform.position.x, XROrigin.transform.position.y - 50, XROrigin.transform.position.z);
        if (Physics.Raycast(handPoint.transform.position, handPoint.transform.forward, out RaycastHit hitInfo, maxDistance))
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
            projectile = Instantiate(FireballPrefab, spawnPosition, Quaternion.identity);
        }
        else if (skill == Skill.Plasma)
        {
            projectile = Instantiate(PlasmaPrefabs, spawnPosition, Quaternion.identity);
        }
        else if (skill == Skill.Ice)
        {
            projectile = Instantiate(IcePrefabs, spawnPosition, Quaternion.identity);
        }

        return projectile;
    }
}