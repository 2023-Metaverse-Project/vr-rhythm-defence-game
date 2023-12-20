using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        
    }

    public void Victory()
    {
        animator.SetBool("isVictory", true);
    }

    public void DieAnim()
    {
        animator.SetBool("isAlive", false);
    }

    public void GetHitAnim()
    {
        animator.SetTrigger("getHit");
    }
}
