using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEditor.Experimental.GraphView;

public class PerfectShooter : MonoBehaviour
{
    public float speed = 700;
    public float fireRate = 0.2f;
    public GameObject shootPoint;

    private bool canShoot = true;

    [SerializeField]
    private List<GameObject> missilePrefabs = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator Shoot(int missileIndex)
    {
        canShoot = false;
        ShootProjectile(missileIndex);
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    void ShootProjectile(int missileIndex)
    {
        Vector3 spawnPositionWithOffset = shootPoint.transform.position;
        GameObject projectile = Instantiate(missilePrefabs[missileIndex], spawnPositionWithOffset, Quaternion.identity);

        //Vector3 direction = movementSource.forward.normalized;
        //projectile.transform.LookAt(spawnPositionWithOffset + direction * 10f);
        //projectile.GetComponent<Rigidbody>().AddForce(direction * speed);
    }
}
