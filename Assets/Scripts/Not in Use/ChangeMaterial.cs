using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    
   Color[] colors;

   Renderer thisRend;

   float transitionTime = 5f;

    void Start()
    {
        thisRend = GetComponent<Renderer>();
        colors = new Color[6];

        colors[0] = Color.white;
        colors[1] = Color.black;
        colors[2] = Color.red;
        colors[3] = Color.green;
        colors[4] = Color.blue;
        colors[5] = Color.magenta;

        StartCoroutine(ColorChange());
    }

   
    void Update()
    {
        
    }

    IEnumerator ColorChange()
    {
        while(true)
        {
            Color newColor = colors[Random.Range(0, 5)];
            float transitionRate = 0f;

            while(transitionRate < 1)
            {
                thisRend.material.SetColor("_Color", Color.Lerp(thisRend.material.color, newColor, Time.deltaTime * transitionTime));

                transitionRate += Time.deltaTime / transitionTime;

                yield return null;
            }
            yield return null;
        }
    }
}
