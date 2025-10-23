using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickerControler : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;
    public Component light2D;

    void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        light2D.GetComponent<Light2D>().enabled = false;
        timeDelay = Random.Range(0.01f, 0.35f);
        yield return new WaitForSeconds(timeDelay);
        light2D.GetComponent <Light2D>().enabled = true;
        timeDelay = Random.Range(0.01f, 0.35f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
