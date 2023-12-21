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
    public GameObject BloodEffect;

    public UnityEvent onGameover;
    public UnityEvent onDamaged;



    private void Awake()
    {
        currentHP = MaxHP;
    }

    public void Play()
    {
        currentHP = MaxHP;
    }

    public void Restart()
    {
        currentHP = MaxHP;
    }

    public void Stop()
    {

    }

    private void Update()
    {
        textHP.text = currentHP.ToString() + "/" + MaxHP.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.ToString());
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
            //Debug.Log("Small");
            currentHP -= smallDamage;
        }

        onDamaged.Invoke();

        other.transform.GetComponent<Enemy>().Die();

        if (currentHP <= 0)
        {
            currentHP = 0;
            onGameover?.Invoke();
        }
    }

}
