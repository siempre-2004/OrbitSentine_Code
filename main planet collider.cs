using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlanetCollider : MonoBehaviour
{
    public List<GameObject> planets = new List<GameObject>();
    public void DecreasePlanets(GameObject planetToDecrease)
    {
        Star starDecrease = planetToDecrease.GetComponent<Star>();
        if (starDecrease != null && starDecrease.isUnlocked)
        {
            starDecrease.HandleAsteroidDecrease();
            Debug.Log("Handle Decrease asteroid function called");
        }
        else
        {
            Debug.Log("Star component not found or planet not unlocked");
        }
    }

    public void DecreaseMultiPlanets(int countToDecrease)
    {
        List<GameObject> unlockedPlanets = new List<GameObject>();
        foreach (GameObject planet in planets)
        {
            Star starComponent = planet.GetComponent<Star>();
            if (starComponent != null && starComponent.isUnlocked)
            {
                unlockedPlanets.Add(planet);
            }
        }
        Debug.Log($"Unlocked planets count: {unlockedPlanets.Count}");
        int decreaseCount = Mathf.Min(countToDecrease, unlockedPlanets.Count);
        for (int i = 0; i < decreaseCount; i++)
        {
            int index = Random.Range(0, unlockedPlanets.Count);
            GameObject planetToDecrease = unlockedPlanets[index];
            DecreasePlanets(planetToDecrease);
            unlockedPlanets.RemoveAt(index);
        }
    }
    void Start()
    {
        planets.Add(GameObject.Find("planet1"));
        planets.Add(GameObject.Find("planet2"));
        planets.Add(GameObject.Find("planet3"));
        planets.Add(GameObject.Find("planet4"));
        planets.Add(GameObject.Find("planet5"));
        planets.Add(GameObject.Find("planet6"));
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        string asteroid = "asteroid";
        string uAsteroid = "upgradeAsteroid";
        if (collision.gameObject.CompareTag(asteroid) )
        {
           // DecreaseMultiPlanets(1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag(uAsteroid) )
        {
            Destroy(collision.gameObject);
        }
    }
}