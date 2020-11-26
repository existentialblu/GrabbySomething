using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegacyGrabber : MonoBehaviour
{
    public string grabButtonName;

    void Start()
    {
        
    }

    void Update()
    {
        // If grip has been pressed
        if(Input.GetButtonDown(grabButtonName))
        {
            Debug.Log("Thing has been grabbed");
        }
    }
}
