using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    public GameObject fireballWeaponPrefab;
    public GameObject plasmaWeaponPrefab;
    public GameObject iceWeaponPrefab;

    public GameObject fireEffectPrefab;
    public GameObject plasmaEffectPrefab;
    public GameObject iceEffectPrefab;

    private void Awake()
    {
        fireballWeaponPrefab.SetActive(true);
        plasmaWeaponPrefab.SetActive(false);
        iceWeaponPrefab.SetActive(false);

        fireEffectPrefab.SetActive(false);
        plasmaEffectPrefab.SetActive(false);
        iceEffectPrefab.SetActive(false);
    }

    public void ChangeWeapon(Skill skill)
    {
        fireballWeaponPrefab.SetActive(false);
        plasmaWeaponPrefab.SetActive(false);
        iceWeaponPrefab.SetActive(false);
        
        if (skill == Skill.Fireball)
        {
            fireEffectPrefab.SetActive(false);
            fireEffectPrefab.SetActive(true);
            fireEffectPrefab.GetComponent<ParticleSystem>().Play();

            fireballWeaponPrefab.SetActive(true);
        }
        else if (skill == Skill.Plasma)
        {
            plasmaEffectPrefab.SetActive(false);
            plasmaEffectPrefab.SetActive(true);
            plasmaEffectPrefab.GetComponent<ParticleSystem>().Play();

            plasmaWeaponPrefab.SetActive(true);
        }
        else if (skill == Skill.Ice) 
        {
            iceEffectPrefab.SetActive(false);
            iceEffectPrefab.SetActive(true);
            iceEffectPrefab.GetComponent<ParticleSystem>().Play();

            iceWeaponPrefab.SetActive(true);
        }
    }
}
