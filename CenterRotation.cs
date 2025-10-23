using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MainPlanetRotation : MonoBehaviour
{
    private bool isRotating = true;
    public float rotationSpeed = 50f;
    public float maxRotationSpeed = 100f;
    public float accelerationMultiplier = 2f;
    private float currentSpeed;
    private bool isAccelerated = false;
    private float accelerationTimer = 0f;
    private Star[] stars;

    void Start()
    {
        currentSpeed = rotationSpeed;
        stars = FindObjectsOfType<Star>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAccelerated = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isAccelerated = false;
            accelerationTimer = 0f;
        }
        if (isAccelerated)
        {
            accelerationTimer += Time.deltaTime;

            // Increase the speed of the rotation by time, up to the "maxRotationSpeed"
            currentSpeed = Mathf.Lerp(rotationSpeed, maxRotationSpeed, accelerationTimer / 1.2f);

            currentSpeed = Mathf.Clamp(currentSpeed, rotationSpeed, maxRotationSpeed);
        }
        else
        {
            currentSpeed = rotationSpeed;
        }
        
        float angle = transform.eulerAngles.z + (currentSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, angle);

        foreach (var star in stars)
        {
           // star.UpdateStarRotation(currentSpeed);
        }
    }
}




