using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravityConstant = 5.0f;
    public float gravityRadius = 10.0f;

    private Rigidbody2D rbBullet;
    private float originalSpeed;

    private void Start()
    {
        rbBullet = GetComponent<Rigidbody2D>();
        originalSpeed = rbBullet.velocity.magnitude;
    }

    private void FixedUpdate()
    {
        ApplyGravity("Planet");

        rbBullet.velocity = rbBullet.velocity.normalized * originalSpeed;
    }

    void ApplyGravity(string tag)
    {
        GameObject[] gravityObjects = GameObject.FindGameObjectsWithTag(tag);

        foreach (var gravityObject in gravityObjects)
        {
            if (gravityObject == gameObject) 
                continue;

            Rigidbody2D rbCelestial = gravityObject.GetComponent<Rigidbody2D>();
            CircleCollider2D celestialCollider = gravityObject.GetComponent<CircleCollider2D>();

            if (celestialCollider != null)
            {
                Vector2 closestPoint = celestialCollider.ClosestPoint(rbBullet.position);
                Vector2 direction = closestPoint - rbBullet.position;
                float distance = direction.magnitude;

                if (!float.IsNaN(direction.x) && !float.IsNaN(direction.y))
                {
                    float dist = direction.magnitude;

                    if (dist <= gravityRadius)
                    {
                        float forceMagnitude = (gravityConstant * (rbBullet.mass * rbCelestial.mass)) / Mathf.Pow(dist, 2);
                        Vector2 force = direction.normalized * forceMagnitude;

                        rbBullet.AddForce(force, ForceMode2D.Force);

                        float requiredSpeed = originalSpeed;

                        if (rbBullet.velocity.magnitude > requiredSpeed)
                        {
                            rbBullet.velocity = rbBullet.velocity.normalized * requiredSpeed;
                        }
                    }
                }
            }
        }
    }
}
