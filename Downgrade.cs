using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Downgrade : MonoBehaviour
{
   public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            GameObject collidedPlanet = collision.gameObject;
            Star star = collidedPlanet.GetComponentInParent<Star>();
            if (star != null && star.isUnlocked)
            {
                star.ResetLevel();
                DisableCurrentComponent(collidedPlanet);
            }
            else
            {
                Debug.Log("could not reset");
            }

            DisableCurrentComponent(collidedPlanet);
            
        }
    }

    private void DisableCurrentComponent(GameObject collidedPlanet)
    {
        if (collidedPlanet != null)
        {
            collidedPlanet.SetActive(false);
        }
    }
    private void EnableParentComponent(GameObject planet)
    {
        if (planet != null)
        {
            planet.SetActive(true);
        }
    }
}

    

