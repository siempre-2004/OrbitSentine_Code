using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAnimator : MonoBehaviour
{
    Animator animator;
    MoveTowards moveTowardsScript;

    void Start()
    {
        animator = GetComponent<Animator>();
        moveTowardsScript = GetComponent<MoveTowards>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        string bullet = "bullet";
        string planet = "Planet";
        if (collision.gameObject.CompareTag(bullet) || collision.gameObject.CompareTag(planet))
        {
            animator.SetTrigger("Destroyed");
            moveTowardsScript.enabled = false; //disable the move towards
            Debug.Log("collided with planet or bullet");
        }
    }
}
