using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Make sure Line renderer exists
[RequireComponent(typeof(LineRenderer))]
public class OrbitPath : MonoBehaviour
{
    // Line Renderer
    public LineRenderer LR;
    // Segments in the Ellipse
    [Range(3, 450)]
    public int segments;
    // Ellipse
    public Ellipse orbit;
    // Orbit origin
    public float xOrigin;               // Set this to parent
    public float yOrigin;               // Set this to parent
    public float yaw = 0.0f;            // Yaw angle of orbit
    public float roll = 0.0f;           // Roll angle of orbit
    public float pitch = 0.0f;          // Pitch angle of orbit
    // Camera, to find cam distance from line so we can size it appropriately
    public FlyCamera cam;

    // Set it up
    void Awake()
    {
        LR = GetComponent<LineRenderer>();
        DrawEllipse();
    }

    public void DrawEllipse()
    {       
        Vector3[] vertices = new Vector3[segments + 1];
        for (int i = 0; i < segments; i++)
        {
            // Turn into floats to prevent issues
            float h = i;
            float l = segments;
            float t = h / l;
            // create ellipse position
            Vectors pos = orbit.Create(t, new Vectors(xOrigin, yOrigin,0));

            Matrix4X4 yawMatrix = Matrix4X4.CreateYaw(yaw);
            Matrix4X4 pitchMatrix = Matrix4X4.CreatePitch(pitch);
            Matrix4X4 rollMatrix = Matrix4X4.CreateRoll(roll);

            Vectors rollVertex = rollMatrix * pos;
            Vectors pitchVertex = pitchMatrix * rollVertex;
            Vectors yawVertex = yawMatrix * pitchVertex;

            // Store the position
            vertices[i] = new Vector3(yawVertex.x, yawVertex.y, yawVertex.z);
        }
        // Set the last point to the first (closes the loop)
        vertices[segments] = vertices[0];

        LR.positionCount = segments + 1;
        LR.SetPositions(vertices);

        // Set line width based on distance from camera
        float magnitude = ResizeOrbitMesh(vertices);
        LR.endWidth = magnitude;
        LR.startWidth = magnitude;
    }

    // Scale the orbits mesh to camera distance
    public float ResizeOrbitMesh(Vector3[] positions)
    {
        float r = 0.0f;

        // Get planet component for its xyz location
        Planet Parent = GetComponentInParent<Planet>();
        // Find diferrence between planet and camera
        Vectors Difference = new Vectors(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z) - new Vectors(Parent.x, Parent.y, Parent.z);
        // Normalise our difference
        Difference.Normalize();
        // Work out distance
        float Distance = Difference.VectorLenSqr();

        // Im not sure how to set some sort of magnitude, so instead the thickness of the line is 100th of the distance
        Distance = Distance/ 100;

        return r = Distance;
    }

    private void OnValidate()
    {
        // Prevents errors caused by accessing when orbit is null or application is off
        if (Application.isPlaying && LR != null)
        {
            DrawEllipse();
        }      
    }
}
