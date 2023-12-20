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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.transform.CompareTag("MonsterBig"))
        {
            currentHP -= bigDamage;
        }
        else if (collision.transform.CompareTag("MonsterMiddle"))
        {
            currentHP -= middleDamage;
        }
        else if (collision.transform.CompareTag("MonsterSmall"))
        {
            Debug.Log("Small");
            currentHP -= smallDamage;
        }

        collision.transform.GetComponent<Enemy>().Die();

        if (currentHP < 0)
            onGameover?.Invoke();
    }
}
