using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASexplosionAnimator : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void triggerExplosion()
    {
        if (animator != null) 
        {
            animator.Play("Asteroid Explosion");
        }
    }
}
