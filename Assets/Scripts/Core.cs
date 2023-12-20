using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Core : MonoBehaviour
{
    [Header("Core Setting")]
    public float MaxHP;
    public float currentHP;
    public float bigDamage;
    public float middleDamage;
    public float smallDamage;

    [Header("ETC")]
    public TextMeshProUGUI textHP;

    public UnityEvent onGameover;


    private void Awake()
    {
        currentHP = MaxHP;
    }

    private void Update()
    {
        textHP.text = currentHP.ToString() + "/" + MaxHP.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.ToString());
        if (other.transform.CompareTag("MonsterBig"))
        {
            currentHP -= bigDamage;
        }
        else if (other.transform.CompareTag("MonsterMiddle"))
        {
            currentHP -= middleDamage;
        }
        else if (other.transform.CompareTag("MonsterSmall"))
        {
            Debug.Log("Small");
            currentHP -= smallDamage;
        }

        other.transform.GetComponent<Enemy>().Die();

        if (currentHP < 0)
            onGameover?.Invoke();
    }

}
