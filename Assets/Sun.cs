using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    Vector3[] ModelSpaceVertices;

    // Render Details
    float yaw = 0.0f;
    float pitch = 0.0f;
    float roll = 0.0f;
    float scalex = 1.0f;
    float scaley = 2.0f;
    float scalez = 1.0f;
    public Vectors CurrentPosition = new Vectors(0, 0, 0);
    float scaleFactor = 0.00001f;

    // Physics
    public float Mass;          // A mass of 1 = mass of Earth (5.972 × 10²⁴ kg)
    const float G = 6674f;     // Gravitational constant
    float timeOfDay = 0.0f;
    float timeBoost = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Get Vertex count
        MeshFilter MF = GetComponent<MeshFilter>();
        ModelSpaceVertices = MF.mesh.vertices;
        CurrentPosition = new Vectors(transform.position.x, transform.position.y, transform.position.z);
        Mass = Mass * Mathf.Pow(scaleFactor, 2);
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate
        timeOfDay = (Time.deltaTime / 3600) * timeBoost;
        yaw += ((timeOfDay / 24) * 360.0f);

        // Render object
        MeshFilter MF = GetComponent<MeshFilter>(); // Tells QCombine routine the object its moving and all its data
        Quarts.QCombine(new Vectors(0, 0, 0), new Vectors(0, 0, 0), new Vectors(scalex, scaley, scalez), new Vectors(roll, pitch, yaw), ModelSpaceVertices, MF);
    }

    // For physics with planets
    private void FixedUpdate()
    {
        //// Find all planets, check attraction against each
        //Planet[] planets = FindObjectsOfType<Planet>();
        //foreach (Planet planet in planets)
        //{
        //    if (planet != this)
        //    {
        //        Attraction(planet);
        //    }
        //}


    }

    // Real Life gravity, Doesnt work properly - but at least i tried
    void Attraction(Planet planet)
    {
        //// Direction from each other
        ////Vectors Direction = CurrentPosition - planet.CurrentPosition;
        ////float Distance = Direction.VectorLenSqr();
        ////float mForce = G * (Mass * planet.Mass) / Mathf.Pow(Distance, 2);
        ////Vectors Force = Direction.Normalize() * mForce;
        //Vectors Forward = new Vectors(0,1,0);

        //Vectors Direction = CurrentPosition - planet.CurrentPosition;
        //float Distance = Direction.VectorLenSqr();
        //Vectors dF = (CurrentPosition - planet.CurrentPosition) / Distance;
        //float fV = (Mass * planet.Mass) / Mathf.Pow(Distance, 2);
        //Vectors f = (dF*fV) * G;

        //f /= 0.0000000000000000000001f;

        //f += (Forward * 1.1f) * 0.1f;

        

        //planet.Vx += f.x;
        //planet.Vy += f.y;
        //planet.Vz += f.z;

        ////Debug.Log(Force.x + " " + Force.y + " " + Force.z);
    }
}
