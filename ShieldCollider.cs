using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollider : MonoBehaviour
{

   // public asteroid shieldColliderAScount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.CompareTag("asteroid"))
        {
            if (shieldColliderAScount != null)

            {
                shieldColliderAScount.AsteroidDestroyed();
            }
        */
        if (collision.gameObject.CompareTag("asteroid"))
        {
            Destroy(collision.gameObject, 0.5f);
        }
    }
}