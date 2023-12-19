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

    [Header("Setting")]
    public float speed = 700;
    public int missileIndex = 0;

    [Header("Mode")]
    public PressTiming timing = PressTiming.Pass;

    [SerializeField]
    private List<GameObject> missilePrefabs = new List<GameObject>();

    public void Shoot()
    {
        switch (timing)
        {
            case PressTiming.Perfect:
                ShootProjectileStartEnd();
                break;

            case PressTiming.Good:
            case PressTiming.Bad:
                ShootProjectileHandForward();
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

    private void ShootProjectileStartEnd()
    {
        Vector3 spawnPosition = startPoint.transform.position;
        GameObject projectile = Instantiate(missilePrefabs[missileIndex], spawnPosition, Quaternion.identity);

        Vector3 direction = endPoint.transform.position - startPoint.transform.position;
        projectile.transform.LookAt(spawnPosition + direction * 10f);
        projectile.GetComponent<Rigidbody>().AddForce(direction * speed);
    }

    private void ShootProjectileHandForward()
    {
        Vector3 spawnPosition = handPoint.transform.position;
        GameObject projectile = Instantiate(missilePrefabs[missileIndex], spawnPosition, Quaternion.identity);

        Vector3 direction = handPoint.transform.forward;
        projectile.transform.LookAt(spawnPosition + direction * 10f);
        projectile.GetComponent<Rigidbody>().AddForce(direction * speed);
    }
}
