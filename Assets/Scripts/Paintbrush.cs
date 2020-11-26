using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintbrush : GrabObject
{
    private SphereCollider sphereCollider;
    private Color currentColor;
    public LineRenderer paintStrokePrefab;
    private LineRenderer currentStroke;
    private bool painting;
    private List<Vector3> segmentPositions = new List<Vector3>();
    private Vector3 lastSegmentPosition;
    public float segmentLength = 0.01f;

    protected override void Start()
    {
        base.Start();
        
        sphereCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        // If we are painting
        if(painting)
        {
            // Has the paintbrush moved enough from last segment position to warrent new segment
            if(Vector3.Distance(transform.position, lastSegmentPosition) > segmentLength)
            {
                // Add new segment
                segmentPositions.Add(transform.position);

                // Update the line renderer
                UpdateStroke();

                // Update the last segment position
                lastSegmentPosition = transform.position;
            }

            // If the paintbrush hasn't move far enough
            
        }
    }

    public override void OnGrabbed()
    {
        base.OnGrabbed();

        sphereCollider.isTrigger = true;
        
    }

    public override void OnDropped()
    {
        base.OnDropped();

        sphereCollider.isTrigger = false;
    }

    public override void OnTriggerStart()
    {
        base.OnTriggerStart();

        // Create an instance of the paint stroke prefab
        currentStroke = Instantiate(paintStrokePrefab);

        // Set the color of the paint stroke to match paintbrush's current color
        currentStroke.material.color = currentColor;

        // Flag the brush as painting
        painting = true;

        // Clear any existing segments from the last paint stroke
        segmentPositions.Clear();

        // Add the paintbrush's position as the start of the first segment
        segmentPositions.Add(transform.position);

        // Update the line renderer
        UpdateStroke();

        // Update the last segment position
        lastSegmentPosition = transform.position;
    }

    private void UpdateStroke()
    {
        // Update the line renderer's actual positions based on list of segment positions
        currentStroke.positionCount = segmentPositions.Count;
        currentStroke.SetPositions(segmentPositions.ToArray());
    }

    public override void OnTriggerEnd()
    {
        base.OnTriggerEnd();

        // Flag the paintbrush as not painting
        painting = false;

        // Clear segment position list thingy
        segmentPositions.Clear();
    }

    public override void OnAnotherObjectHighlighted(GrabObject otherObject)
    {
        base.OnAnotherObjectHighlighted(otherObject);

        var paintPot = otherObject.GetComponent<PaintPots>();
        if(paintPot != null)
        {
            currentColor = paintPot.color;
            GetComponent<MeshRenderer>().material.color = currentColor;
        }
    }

}
