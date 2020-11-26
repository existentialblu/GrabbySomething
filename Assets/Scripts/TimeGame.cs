using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGame : MonoBehaviour
{
    float roundStartTime;
    float roundStartDelayTime = 3f;
    private int waitTime;
    string message;
    bool roundStarted;
    void Start()
    {
        print("Press the space bar when you think the alloted time is up");
        //Create delay between initial message and starting the game
        Invoke("SetNewRandomTime", roundStartDelayTime);
    }

    void Update()
    {
        // If space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && roundStarted)
        {
            InputReceived();
        }
    }

    void SetNewRandomTime()
    {
        waitTime = Random.Range(5, 21);
        roundStartTime = Time.time;
        print("Wait " + waitTime + " seconds");
        roundStarted = true;
    }

    void InputReceived()
    {
        roundStarted = false;
        float playerWaitTime = Time.time - roundStartTime;
        float error = Mathf.Abs(waitTime - playerWaitTime);

       
        print(GenerateMessage(error) + "You waited " + playerWaitTime + "You were " + error + " seconds off");
        Invoke("SetNewRandomTime", roundStartDelayTime);
    }

    string GenerateMessage(float error)
    {
        if (error <= 0.5f)
        {
            message = "Nicely done. ";
        }
        else if (error > 0.5f && error <= 1f)
        {
            message = "That went okay. ";
        }
        else
        {
            message = "That wasn't good. ";
        }
        
        return message;
    }

  
}
