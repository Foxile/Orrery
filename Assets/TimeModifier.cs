using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeModifier : MonoBehaviour
{
    public float Mult = 0.0f;       // The multiplier value
    public float timeMult = 0.0f;   // This is what rotation and movement data in other classes will be * by
    public bool pause = false;             // For the pause button
    float MultStore;                // The temp storage for mult for when we pause

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        KeyHandler();

        // Set Time
        float timeOfDay = (Time.deltaTime / 3600) * Mult;
        // Set time to fit around a circle
        if (timeOfDay != 0.0f) { timeMult = ((timeOfDay / 24) * 360.0f); }
        else { timeMult = 0.0f; }
    }

    void KeyHandler()
    {
        // Hold R to pause
        if (Input.GetKeyDown(KeyCode.R))
        {
            MultStore = Mult;
            Mult = 0.0f;
            pause = true;
        }

        // Press Q to slow down time, R to speed up time
        else if (Input.GetKey(KeyCode.Q))
        {
            Mult = Mult - (Mult * 0.1f);
            // cap mult at 1 since we only want 0 when paused
            if (Mult < 1) { Mult = 1.0f; }
        }
        else if (Input.GetKey(KeyCode.E))
        {
            Mult = Mult * 1.1f;
            // Any faster than this and the game potentially breaks
            if (Mult > 15000) { Mult = 15447.0f; }
        }


        // Once R unpressed, unpause game
        if (Input.GetKeyUp(KeyCode.R))
        {
            Mult = MultStore;
            pause = false;
        }

        Debug.Log(Mult);
    }
}
