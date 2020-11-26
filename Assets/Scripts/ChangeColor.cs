using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public GameObject currentCube;
    private Material currentColor;
    public Material grabbedColor;

    void Start()
    {
  
    }

    void OnCollisionEnter(Collision collision)
    {
        currentColor = grabbedColor;
    }
}
