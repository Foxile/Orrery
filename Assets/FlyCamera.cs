using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;


public class FlyCamera : MonoBehaviour
{
    float Speed;            // Cameras normal movement
    float Sensitivity;      // Look sensitivity
    float RunIncrement;     // Incremental speed increase for holding shift (think unity free cam)
    float RunTotal;         // Maximum accrued speed
    Vectors Mouse;          // Stores Mouse position for look movement
    float RunMultiplier;    // Stores run multiplier

    void Start()
    {
        Speed = 5f;
        Sensitivity = 0.004f;
        RunIncrement = 15f;         
        RunTotal = 100f;
        Mouse = new Vectors(Screen.width / 2, Screen.height / 2, 0);
        RunMultiplier = 1.0f;
    }

    void Update()
    {
       // Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        // Find out how far away mouse is from center
        Mouse = new Vectors(Input.mousePosition.x, Input.mousePosition.y, 0) - Mouse;
        // Add speed
        Mouse = new Vectors(-Mouse.y * Sensitivity, Mouse.x * Sensitivity, 0);
        // Create euler
        Mouse = new Vectors(transform.eulerAngles.x + Mouse.x, transform.eulerAngles.y + Mouse.y, 0);
        // The rotation really doesnt like x angles of over 90 degrees - cant seem to figure it out yet
        //if (Mouse.x > 90.0f) {Mouse.x = 90.0f; }
        //if (Mouse.x < -1.0f) { Mouse.x = -1.0f; }

        transform.eulerAngles = new Vector3(Mouse.x, Mouse.y, 0.0f);
        // Reset Mouse for later
        Mouse = new Vectors(Screen.width / 2, Screen.height / 2, 0); ;


        Vectors Run = WASDMovement();
        // If speeding
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Makes cam go faster till it hits the limit
            RunMultiplier += Time.deltaTime;
            Run = Run * RunMultiplier * RunIncrement;
            // Clamp speed between the max and min
            Run.x = Mathf.Clamp(Run.x, -RunTotal, RunTotal);
            Run.y = Mathf.Clamp(Run.y, -RunTotal, RunTotal);
            Run.z = Mathf.Clamp(Run.z, -RunTotal, RunTotal);
        }
        else
        {
            // Normal speed
            RunMultiplier = Mathf.Clamp(RunMultiplier * 0.5f, 1.0f, RunTotal);
            Run *= Speed;
        }
        Run *= Time.deltaTime;

        // Hold space to move only on the x and z planes (Very useful)
        Vectors newPos = new Vectors(transform.position.x, transform.position.y, transform.position.z);
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(new Vector3(Run.x,Run.y,Run.z));
            newPos.x = transform.position.x;
            newPos.z = transform.position.z;
            transform.position = (new Vector3(newPos.x, newPos.y, newPos.z));
        }
        else { transform.Translate(new Vector3(Run.x, Run.y, Run.z)); }



    }

    private Vectors WASDMovement()
    {
        Vector3 r = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            r += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            r += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            r += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            r += Vector3.right;
        }
        return new Vectors(r.x, r.y,r.z);
    }

}

