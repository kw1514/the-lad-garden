using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    //public Renderer render;
    [SerializeField] Material ladPeel;
    [SerializeField] Color bodyColor;
    [SerializeField] Color accentColor;
    [SerializeField] GameObject Part;
   
    void Start()
    {
        //ladPeel = render.material;
        //ladPeel.SetColor("Body", new Color(255f, 0, 255f));
        //setBodyColor(bodyColor);
        //setAccentColor(accentColor);
    }

    public void setBodyColor(Color color)
    {
        //You need to look at the shader file to see the actual value for the color.
        //Debug.Log(ladPeel.GetColor("_Body") + "is the color");
        ladPeel = GetComponent<Renderer>().material;
        //Debug.Log(ladPeel);
        Material newMat = new Material(ladPeel);
        newMat.SetColor("_Body", color);
        ladPeel = newMat;
        Part.GetComponent<Renderer>().material = ladPeel;

    }

    public void setAccentColor(Color color)
    {
        //You need to look at the shader file to see the actual value for the color.
        //Debug.Log(ladPeel.GetColor("_Body") + "is the color");
        ladPeel = GetComponent<Renderer>().material;
        //Debug.Log(ladPeel);
        Material newMat = new Material(ladPeel);
        newMat.SetColor("_Accent", color);
        ladPeel = newMat;
        Part.GetComponent<Renderer>().material = ladPeel;

    }
}
