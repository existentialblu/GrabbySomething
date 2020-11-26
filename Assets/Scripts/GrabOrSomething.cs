using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GrabOrSomething : MonoBehaviour
{
    public SteamVR_Action_Boolean GripGrab;
    public SteamVR_Action_Boolean GrabPinch;
    public SteamVR_Input_Sources handType;
    private bool gripped = false;
    private bool grippedAlready = true;
    private bool pinched = false;
    private bool pinchedAlready = true;
    private GrabObject highlightedObject;
    private GrabObject grabbedObject;
    public GrabObject grabOnStart;

    void Start()
    {
        // Listen for trigger inputs
        GripGrab.AddOnStateDownListener(GripSqueeze, handType);
        GripGrab.AddOnStateUpListener(GripRelease, handType);
        GrabPinch.AddOnStateDownListener(TriggerDown, handType);
        GrabPinch.AddOnStateUpListener(TriggerUp, handType);

        // if we shoudl grab an object at start
        if(grabOnStart != null)
        {
            Gr
        }

    }

    void Update()
    {
        // if grip has been squeezed
        if(gripped && !grippedAlready)
        {
            //Debug.Log("grip has been squeezed");
            grippedAlready = true;

            // If there's a highlighted object
            if(highlightedObject != null)
            {
                GrabObject(highlightedObject);
            }
        }

        // if grip has been released
        if(!gripped && grippedAlready)
        {
            //Debug.Log("grip has been released");
            grippedAlready = false;

            // If there's a grabbed object
            if(grabbedObject != null)
            {
                DropObject(highlightedObject);
            } 
        }
        // if trigger has been depressed
        if(pinched)
        {
            // and if already holding something
            if(grabbedObject != null)
            {
                // let the object know that the trigger is being pressed
                grabbedObject.OnTriggerStart();
            }
        }
        
        // if trigger has been released
        if(!pinched)
        {
            // and if still holding something
            if(grabbedObject != null)
            {
                grabbedObject.OnTriggerEnd();
            }
        }
    }

    private void GrabObject(GrabObject obj)
    {
        // Store the object that the hand is grabbing
        grabbedObject = obj;
        // Parent the object to the hand
        grabbedObject.transform.SetParent(transform);
        // Disable gravity on object and make it kinematic
        var rigidBody = grabbedObject.GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;

    }
    private void DropObject(GrabObject highlightedObject)
    {
        // Unparent grabbed object
        grabbedObject.transform.SetParent(null);
        // Enable gravity and make object nonkinematic
        var rigidBody = grabbedObject.GetComponent<Rigidbody>();
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;

        // Clear grabbed object
        grabbedObject = null;
    }

    private void GripSqueeze(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        gripped = true;
        //Debug.Log(handType + " is gripped");
    }

    public void GripRelease(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        gripped = false;
       // Debug.Log(handType + " is ungripped");
        
    }

    private void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        pinched = true;
        //Debug.Log(handType + " trigger down");
    }

    private void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        pinched = false;
        //Debug.Log(handType + " trigger up");
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the obect we just touched is a grabbable object
        var grabbableObject = other.GetComponent<GrabObject>();
        if(grabbableObject != null)
        {
            // If something is already grabbed
            if (grabbedObject != null)
            {
                // Let the grabbed object know that another object is highlighted
                grabbedObject.OnAnotherObjectHighlighted(grabbableObject);
            }

            else
            {
                // Highlight grabbable object
                grabbableObject.OnHighlight();

                // Store highlighted object
                highlightedObject = grabbableObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the obect we just stopped touched is a grabbable object
        var grabbableObject = other.GetComponent<GrabObject>();
        if(grabbableObject != null)
        {
            // Unyellow the object
            grabbableObject.OnUnhighlight();

            // TODO: Modify to handle multiple objects in range of the hand at once

            highlightedObject = null;
        }
    }


}
