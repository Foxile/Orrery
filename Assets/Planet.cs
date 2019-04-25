using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    Vector3[] ModelSpaceVertices;

    /// <summary>
    /// Remember:
    /// Scale for planetoids: 1:10,000 Km
    /// scale for distance: 1:250,000 Km
    /// </summary>

    // Variables for orbiting
    public Planet Parent;           // The parent xyz for the origin rotation
    Vectors RotationOrigin;         // Where the ellipse is drawn and planet rotate around
    public OrbitPath orbit;         // Store for the orbit   
    [Range(0.0f,1.0f)]              // Distance from start to end of ellipse is between 0 and 1
    public float Oprogress = 0.0f;  // Progress along orbit
    float Operiod;           // How long it takes (in seconds) to go around orbit 1 time
    TimeModifier TimeM;             // Our time, handles rotation, orbit, translation speeds - and pause


    // Variables for rendering
    float yaw = 0.0f;               // Yaw angle axis
    float pitch = 0.0f;             // Pitch angle axis
    float roll = 0.0f;              // Roll angle axis
    float scalex = 1.0f;            // X scale of model mesh
    float scaley = 2.0f;            // Y scale of model mesh
    float scalez = 1.0f;            // Z scale of model mesh

    // Variables for transform movement
    public Vectors CurrentPosition = new Vectors(0,0,0);    // Curent mesh position
    public float x;                                         // x position of currentPosition, unity is silly and wont show the xyz of vertex positions in game/edditor
    public float y;                                         // y position of currentPosition
    public float z;                                         // z position of currentPosition
    float Vx = 0.0f;                                        // The addon to CurrentPosition - how much the vertices on the mesh have moved in the x direction
    float Vy = 0.0f;                                        // The addon to CurrentPosition - how much the vertices on the mesh have moved in the y direction
    float Vz = 0.0f;                                        // The addon to CurrentPosition - how much the vertices on the mesh have moved in the z direction

    // Physics
    float timeOfDay = 0.0f;
    float timeBoost = 25.0f;
    float scaleFactor = 0.00001f;

    // Start is called before the first frame update
    void Start()
    {
        // Get Vertex count
        MeshFilter MF = GetComponent<MeshFilter>();
        ModelSpaceVertices = MF.mesh.vertices;
        CurrentPosition = new Vectors(transform.position.x, transform.position.y, transform.position.z);

        // So we can see current position of mesh (as matrix moves verices)
        x = CurrentPosition.x;
        y = CurrentPosition.y;
        z = CurrentPosition.z;

        // Fills our orbit path data
        orbit = GetComponent<OrbitPath>();
        // Find time
        TimeM = FindObjectOfType<TimeModifier>();

        // Set ellipses origin
        if (Parent != null)
        {
            RotationOrigin = new Vectors(Parent.x, Parent.y, Parent.z);
        }  
        else { RotationOrigin = new Vectors(0,0,0); }

        // Set our orbit
        setOrbit();
    }

    // Update is called once per frame
    void Update()
    {
        // Set Oorbit speed
        Operiod = TimeM.timeMult;

        // Set our Ellipse orbit
        setEllipseOrigin();

        // Handle pause of ellipse movement (since a mult of 0 doesnt stop it completely)
        if (TimeM.pause == false)
        {
            // Move along planets orbit
            MovingOrbit();
        }


       
        // Rotate around planets axis (Day/Night Cycle) based on current speed
        roll += TimeM.timeMult;
        // Render object
        MeshFilter MF = GetComponent<MeshFilter>(); // Tells QCombine routine the object its moving and all its data
        CurrentPosition = (Quarts.QCombine(new Vectors(Vx, Vy, Vz), CurrentPosition, new Vectors(scalex, scaley, scalez), new Vectors(roll, pitch, yaw), ModelSpaceVertices, MF));
        transform.localPosition = new Vector3(CurrentPosition.x, CurrentPosition.y, CurrentPosition.z);


        // Debug - shows xyz position on screen since mesh rendered doesnt move the actual transform    
        x = CurrentPosition.x;
        y = CurrentPosition.y;
        z = CurrentPosition.z;
    }

    // Calculate the moving orbit of the planet
    void MovingOrbit()
    {      
        if (Operiod < 0.1f) { Operiod = 0.1f; }         // If it reaches 0, its breaks game
        Oprogress += Operiod / 100;                     // Set oprogress
        Oprogress %= 1.0f;                              // If we go beyond 1, reset between 0,1
        setOrbit();                                     // Set our orbit
    }

    // Set Orbit vertex data for current position
    void setOrbit()
    {
        // Grab position along orbit
        Vectors orbitPosition = orbit.orbit.Create(Oprogress, RotationOrigin);

        // Sort out matrix conversion
        Matrix4X4 yawMatrix = Matrix4X4.CreateYaw(orbit.yaw);
        Matrix4X4 pitchMatrix = Matrix4X4.CreatePitch(orbit.pitch);
        Matrix4X4 rollMatrix = Matrix4X4.CreateRoll(orbit.roll);

        Vectors rollVertex = rollMatrix * orbitPosition;
        Vectors pitchVertex = pitchMatrix * rollVertex;
        Vectors yawVertex = yawMatrix * pitchVertex;

        // Set position to this position
        CurrentPosition = yawVertex;
        //Debug.Log(orbitPosition.x + " " + orbitPosition.y + " " + orbitPosition.z);
    }

    // Set ellipses origin - needs to be done every frame since the origin of satellites constantly changes
    void setEllipseOrigin()
    {      
        if (Parent != null)
        {
            RotationOrigin = new Vectors(Parent.x, Parent.y, Parent.z);
            orbit.xOrigin = Parent.x;
            orbit.yOrigin = Parent.y;
        }
        else
        {
            RotationOrigin = new Vectors(0, 0, 0);
            orbit.xOrigin = 0;
            orbit.yOrigin = 0;
        }
        orbit.DrawEllipse();
    }
}
