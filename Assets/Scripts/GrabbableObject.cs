using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    private Material material;
    private Color initialColor;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        initialColor = material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void OnHighlight()
    {
        material.color = Color.yellow;
    }

    internal void OnUnhighlight()
    {
        material.color = initialColor;
    }
}
