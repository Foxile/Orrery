using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetIcon : MonoBehaviour
{
    public Planet planet;  // Our planet for the resize icon
    public FlyCamera cam;  // Our camera
    GameObject Icon;       // Our new Sphere icon
    public float scale;    // To keep planets at their relative scale when scaling up

    // Start is called before the first frame update
    void Start()
    {
        // Create a sphere
        Icon = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // Give it the texture of its parent
        Icon.GetComponent<MeshRenderer>().material = GetComponentInParent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Icon.transform.position = new Vector3(planet.x, planet.y, planet.z);

        // Find diferrence between planet and camera
        Vectors Difference = new Vectors(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z) - new Vectors(planet.x, planet.y, planet.z);
        // Normalise our difference
        Difference.Normalize();
        // Work out distance
        float Distance = Difference.VectorLenSqr();

        // Im not sure how to set some sort of magnitude, so instead the thickness of the line is 100th of the distance
        Distance = (Distance / 10) * scale;

        // So it doesnt get too large
        Distance = Mathf.Clamp(Distance, 0.0f, 100.0f);

        
        // Scale the icon up
        Icon.transform.localScale = new Vector3(Distance, Distance, Distance);
    }
}
