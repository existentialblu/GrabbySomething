using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    private Material material;
    private Color initialColor;
    public bool cantBeDropped;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
        material = GetComponent<MeshRenderer>().material;
        initialColor = material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnHighlight()
    {
        material.color = Color.yellow;
    }

    public virtual void OnUnhighlight()
    {
        material.color = initialColor;
    }

    public virtual void OnGrabbed()
    {
        
    }

    public virtual void OnDropped()
    {

    }

    public virtual void OnTriggerStart()
    {

    }

    public virtual void OnTriggerEnd()
    {

    }

    public virtual void OnAnotherObjectHighlighted(GrabObject otherObject)
    {

    }

}
