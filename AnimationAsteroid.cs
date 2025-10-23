using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAsteroid : MonoBehaviour
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
        if (collision.gameObject.CompareTag("bullet") || collision.gameObject.CompareTag("Planet") || collision.gameObject.CompareTag("shield"))
        {
            animator.SetTrigger("Destroyed");
            moveTowardsScript.enabled = false; //disable the move towards
            Debug.Log("collided with planet or bullet");
        }
    }
}
