using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAnimationRed : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (animator != null && gameObject.activeInHierarchy)
        {

            animator.SetTrigger("TriggerUpgradeRed");
            Debug.Log("Played upgrade animation");
        }
        else
        {
            Debug.Log("UpgradeAnimation did not work");
        }
    }

}