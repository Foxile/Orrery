using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vectors
{
    public float x;
    public float y;
    public float z;
    public float w;

    // Constructor
    public Vectors(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = 0;
    }
    public Vectors(float w, float x, float y, float z)
    {
        this.w = w;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    // Overloads
    public static Vectors operator +(Vectors A, Vectors B) { return VectorAdd(A, B); }
    public static Vectors operator -(Vectors A, Vectors B) { return VectorSub(A, B); }
    public static Vectors operator /(Vectors A, float B) { return VectorDiv(A, B); }
    public static Vectors operator *(Vectors A, float B) { return VectorMul(A, B); }


    // Vector Addition
    public static Vectors VectorAdd(Vectors A, Vectors B)
    {
        Vectors r = new Vectors(0, 0, 0)
        {
            x = A.x + B.x,
            y = A.y + B.y,
            z = A.z + B.z
        };
        return r;
    }
    // Vector Subtraction
    public static Vectors VectorSub(Vectors A, Vectors B)
    {
        Vectors r = new Vectors(0, 0, 0)
        {
            x = A.x - B.x,
            y = A.y - B.y,
            z = A.z - B.z
        };
        return r;
    }
    // Vector Multiply
    public static Vectors VectorMul(Vectors A, float B)
    {
        Vectors r = new Vectors(0, 0, 0)
        {
            x = A.x * B,
            y = A.y * B,
            z = A.z * B
        };
        return r;
    }
    // Vector Divide
    public static Vectors VectorDiv(Vectors A, float B)
    {
        Vectors r = new Vectors(0, 0, 0)
        {
            x = A.x / B,
            y = A.y / B,
            z = A.z / B
        };
        return r;
    }

    // Vector Length
    public float VectorLen()
    {
        float r = 0.0f;
        r = (x * x) + (y * y) + (z * z);
        return r;
    }
    // Vector Magnitude
    public float VectorLenSqr()
    {
        float r = 0.0f;
        r = Mathf.Sqrt((x * x) + (y * y) + (z * z));
        return r;
    }

    // Vector Normalise
    public Vectors Normalize()
    {
        Vectors r = new Vectors(0, 0, 0)
        {
            x = x,
            y = y,
            z = z
        };

        // Divide by its length
        r /= r.VectorLenSqr();
        return r;
    }

    // Dot Product
    public static float DotProduct(Vectors A, Vectors B, bool Normalise = false)
    {
        float r = 0.0f;

        Vectors TempA = A;
        Vectors TempB = B;

        // Normalise vectors if needed
        if (Normalise)
        {
            TempA = TempA.Normalize();
            TempB = TempB.Normalize();
        }
      
        r = (TempA.x * TempB.x) + (TempA.y * TempB.y) + (TempA.z * TempB.z);
        return r;
    }

    // Vector2 to Radians
    public static float VtoR(Vectors A)
    {
        float r = 0.0f;

        r = Mathf.Atan2(A.x, A.y);

        return r;
    }
    // Radians to Vector2
    public static Vectors RtoV(float Angle)
    {
        Vectors r = new Vectors(0, 0, 0);

        r.x = Mathf.Cos(Angle);
        r.y = Mathf.Sin(Angle);
        r.z = 0;

        return r;
    }

    // Euler Angle to Direction  DOESNT WORK
    public static Vectors EtoD(Vectors A)
    {
        Vectors r = new Vectors(0, 0, 0);

        A = A / (180.0f / Mathf.PI);

        r.x = Mathf.Cos(A.x) * Mathf.Sin(A.x);
        r.y = Mathf.Sin(A.x);
        r.z = Mathf.Cos(A.y) * Mathf.Cos(A.x);

        return r;
    }

    // Cross product
    public static Vectors CrossProduct(Vectors A, Vectors B)
    {
        Vectors r = new Vectors(0, 0, 0);

        r.x = (A.y * B.z) - (A.z * B.y);
        r.y = (A.z * B.x) - (A.x * B.z);
        r.z = (A.x * B.y) - (A.y * B.x);

        return r;
    }

    // Distance between 2 vectors
    public static float VDistance(Vectors A, Vectors B)
    {
        float r = 0.0f;

        r = Mathf.Sqrt(Mathf.Pow(A.x - B.x, 2) + Mathf.Pow(A.y - B.y, 2) + Mathf.Pow(A.z - B.z, 2));

        return r;
    }

    // Lerp
    public static Vectors VLerp(Vectors A, Vectors B, float C)
    {
        
        return A * (1.0f - C) + B * C;
    }
}

public class Matrix4X4
{
    // Constructors
    public Matrix4X4(Vector4 column1, Vector4 column2, Vector4 column3, Vector4 column4)
    {
        values = new float[4, 4];

        // Column 1
        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = column1.w;

        // Column 2
        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = column2.w;

        // Column 3
        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = column3.w;

        // Column 4
        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = column4.w;

    }
    public Matrix4X4(Vectors column1, Vectors column2, Vectors column3, Vectors column4)
    {
        values = new float[4, 4];

        // Column 1
        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = 0;

        // Column 2
        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = 0;

        // Column 3
        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = 0;

        // Column 4
        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = 1;
    }
    public float[,] values;

    // Multiplication overloader - takes matrix and a vector
    public static Vectors operator *(Matrix4X4 M, Vectors A)
    {
        Vectors r = new Vectors(0, 0, 0);

        // Remeber W needs to be "1"
        r.x = (A.x * M.values[0, 0]) + (A.y * M.values[0, 1]) + (A.z * M.values[0, 2]) + (A.w * M.values[0, 3]);
        r.y = (A.x * M.values[1, 0]) + (A.y * M.values[1, 1]) + (A.z * M.values[1, 2]) + (A.w * M.values[1, 3]);
        r.z = (A.x * M.values[2, 0]) + (A.y * M.values[2, 1]) + (A.z * M.values[2, 2]) + (A.w * M.values[2, 3]);
        r.w = (A.x * M.values[3, 0]) + (A.y * M.values[3, 1]) + (A.z * M.values[3, 2]) + (A.w * M.values[3, 3]);

        return r;
    }
    public static Vector4 operator *(Matrix4X4 M, Vector4 A)
    {
        Vector4 r = new Vector4(0, 0, 0);

        // Remeber W needs to be "1"
        r.x = (A.x * M.values[0, 0]) + (A.y * M.values[0, 1]) + (A.z * M.values[0, 2]) + (A.w * M.values[0, 3]);
        r.y = (A.x * M.values[1, 0]) + (A.y * M.values[1, 1]) + (A.z * M.values[1, 2]) + (A.w * M.values[1, 3]);
        r.z = (A.x * M.values[2, 0]) + (A.y * M.values[2, 1]) + (A.z * M.values[2, 2]) + (A.w * M.values[2, 3]);
        r.w = (A.x * M.values[3, 0]) + (A.y * M.values[3, 1]) + (A.z * M.values[3, 2]) + (A.w * M.values[3, 3]);

        return r;
    }

    // Generate RPYST matrices
    public static Matrix4X4 CreateRoll(float Angle)
    {
        Matrix4X4 r = new Matrix4X4(
            new Vector3(Mathf.Cos(Angle), Mathf.Sin(Angle), 0),
            new Vector3(-Mathf.Sin(Angle), Mathf.Cos(Angle), 0),
            new Vector3(0, 0, 1),
            Vector3.zero);
        return r;
    }
    public static Matrix4X4 CreatePitch(float Angle)
    {
        Matrix4X4 r = new Matrix4X4(
            new Vector3(1, 0, 0),
            new Vector3(0, Mathf.Cos(Angle), Mathf.Sin(Angle)),
            new Vector3(0, -Mathf.Sin(Angle), Mathf.Cos(Angle)),
            Vector3.zero);
        return r;
    }
    public static Matrix4X4 CreateYaw(float Angle)
    {
        Matrix4X4 r = new Matrix4X4(
            new Vector3(Mathf.Cos(Angle), 0, -Mathf.Sin(Angle)),
            new Vector3(0, 1, 0),
            new Vector3(Mathf.Sin(Angle), 0, Mathf.Cos(Angle)),
            Vector3.zero);
        return r;
    }
    public static Matrix4X4 CreateScale(Vectors A)
    {
        Matrix4X4 r = new Matrix4X4(
            new Vector3(1, 0, 0) * A.x,
            new Vector3(0, 1, 0) * A.y,
            new Vector3(0, 0, 1) * A.z,
            Vector3.zero);
        return r;
    }
    public static Matrix4X4 CreateTrans(Vectors A)
    {
        Matrix4X4 r = new Matrix4X4(
            new Vector4(1, 0, 0, A.x),
            new Vector4(0, 1, 0, A.y),
            new Vector4(0, 0, 1, A.z),
            new Vector4(0, 0, 0, 1));
        return r;
    }
}

public class Quarts
{  
    public float w;
    public float x;
    public float y;
    public float z;

    // Constructor
    public Quarts(float A, Vectors B)
    {
        float hAngle = A / 2;
        w = Mathf.Cos(hAngle);
        x = B.x * Mathf.Sin(hAngle);
        y = B.y * Mathf.Sin(hAngle);
        z = B.z * Mathf.Sin(hAngle);
    }
    public Quarts(Vectors B)
    {
        w = 0.0f;
        x = B.x;
        y = B.y;
        z = B.z;
    }

    public Quarts Inverse()
    {
        Quarts r = new Quarts(0,new Vectors(0,0,0));
        r.w = w;
        r.x = -x;
        r.y = -y;
        r.z = -z;
        return r;
    }

    // Creates a vector from quaternion
    public static Vectors GetAxis(Quarts A)
    {
        return new Vectors(A.x, A.y, A.z);
    }
    // Creates a vector from quaternion
    public Vectors GetAxis()
    {
        return new Vectors(x, y, z);
    }

    public void SetAxis(Vectors A)
    //sets a quaternion from a vector
    {
        x = A.x;
        y = A.y;
        z = A.z;
    }

    // Slerp Quaternion Method
    public static Quarts SLERP(Quarts A, Quarts B, float C)
    {
        C = Mathf.Clamp(C, 0.0f, 1.0f);

        Quarts temp = B * A.Inverse();
        Vectors AxisAngle = temp.GetAxisAngle();
        Quarts temp2 = new Quarts(AxisAngle.w * C, new Vectors(AxisAngle.x, AxisAngle.y, AxisAngle.z));

        return temp2 * A;
    }

    // Convert Quaternion into Axis Angle
    public Vectors GetAxisAngle()
    {
        Vectors r = new Vectors(0, 0, 0, 0);

        // Get full Angle
        float halfAngle = Mathf.Acos(w);
        r.w = halfAngle * 2;

        r.x = x / Mathf.Sin(halfAngle);
        r.y = y / Mathf.Sin(halfAngle);
        r.z = z / Mathf.Sin(halfAngle);

        return r;
    }

    // Convert non normalised Quaternion into euler Angle - retrieved from: http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToEuler/
    public static Vector3 GetEulerAngle(Quarts A)
    {
        Vector3 r = new Vector3(0, 0, 0);

        float sqw = A.w * A.w;
        float sqx = A.x * A.x;
        float sqy = A.y * A.y;
        float sqz = A.z * A.z;
        float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
        float test = A.x * A.y + A.z * A.w;
        if (test > 0.499 * unit)
        { // singularity at north pole
            r.y = 2 * Mathf.Atan2(A.x, A.w);
            r.z = Mathf.PI / 2;
            r.x = 0;
            return r;
        }
        if (test < -0.499 * unit)
        { // singularity at south pole
            r.y = -2 * Mathf.Atan2(A.x, A.w);
            r.z = -Mathf.PI / 2;
            r.x = 0;
            return r;
        }
        r.y = Mathf.Atan2(2 * A.y * A.w - 2 * A.x * A.z, sqx - sqy - sqz + sqw);
        r.z = Mathf.Asin(2 * test / unit);
        r.x = Mathf.Atan2(2 * A.x * A.w - 2 * A.y * A.z, -sqx + sqy - sqz + sqw);
        return r;
    }

    // Convert normalised Quaternion into euler Angle - retrieved from: http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToEuler/
    public static Vector3 GetEulerAngleN(Quarts A)
    {
        Vector3 r = new Vector3(0, 0, 0);

        float test = A.x * A.y + A.z * A.w;
        if (test > 0.499)
        { // singularity at north pole
            r.y = 2 * Mathf.Atan2(A.x, A.w);
            r.z = Mathf.PI / 2;
            r.x = 0;
            return r;
        }
        if (test < -0.499)
        { // singularity at south pole
            r.y = -2 * Mathf.Atan2(A.x, A.w);
            r.z = -Mathf.PI / 2;
            r.x = 0;
            return r;
        }
        float sqx = A.x * A.x;
        float sqy = A.y * A.y;
        float sqz = A.z * A.z;
        r.y = Mathf.Atan2(2 * A.y * A.w - 2 * A.x * A.z, 1 - 2 * sqy - 2 * sqz);
        r.z = Mathf.Asin(2 * test);
        r.x = Mathf.Atan2(2 * A.x * A.w - 2 * A.y * A.z, 1 - 2 * sqx - 2 * sqz);
        return r;
    }

    // Norm
    public static Quarts Norm(Quarts A)
    {
        Quarts r = new Quarts(0, new Vectors(0, 0, 0));

        r.w = A.w * 2;
        r.x = A.x * 2;
        r.y = A.y * 2;
        r.z = A.z * 2;

        return r;
    }

    // Quarternion multiplication
    public static Quarts operator *(Quarts S, Quarts R)
    {
        Quarts r = new Quarts(0, new Vectors(0, 0, 0));

        // Turn Quarternions into vectors for calculating xyz values later
        Vectors SV = GetAxis(S);
        Vectors RV = GetAxis(R);
    
        // Calculates the new W value
        float Cross = Vectors.DotProduct(GetAxis(S), GetAxis(R));
        r.w = (S.w * R.w) - Cross;

        // Calculates the new XYZ values
        Vectors temp = new Vectors(0, 0, 0);
        temp = ((RV * S.w) + (SV * R.w) + Vectors.CrossProduct(RV, SV));

        // Put the new xyz values into the return quaternion
        r.x = temp.x;
        r.y = temp.y;
        r.z = temp.z;

        return r;
    }

    // Convert Matrix to qauternion - retrieved from gamasutra: https://www.gamasutra.com/view/feature/131686/rotating_objects_using_quaternions.php?page=1
    public static Quarts MatToQuat(Matrix4X4 M)
    {
        Quarts r = new Quarts(0, new Vectors(0, 0, 0));

        float tr, s;
        float[] q = new float[4];
        int i, j, k;
        int[] nxt = new int[3] {1,2,0};

        tr = M.values[0, 0] + M.values[1, 1] + M.values[2, 2];

        // Check diagonal
        if (tr > 0.0f)
        {
            s = Mathf.Sqrt(tr + 1.0f);
            r.w = s / 2.0f;
            s = 0.5f / s;
            r.x = (M.values[1, 2] - M.values[2, 1]) * s;
            r.y = (M.values[2, 0] - M.values[0, 2]) * s;
            r.z = (M.values[0, 1] - M.values[1, 0]) * s;
        }
        // Diagonal negative
        else
        {
            i = 0;
            if (M.values[1, 1] > M.values[0, 0]) { i = 1; }
            if (M.values[2, 2] > M.values[i, i]) { i = 2; }
            j = nxt[i];
            k = nxt[j];
            s = Mathf.Sqrt(M.values[i, i] - M.values[j, j] + M.values[k, k] + 1.0f);
            q[i] = s * 0.5f;
            if (s != 0.0f) { s = 0.5f / s; }
            q[3] = (M.values[j,k] - M.values[k,j]) * s;
            q[j] = (M.values[i,j] + M.values[j,i]) * s;
            q[k] = (M.values[i,k] + M.values[k,i]) * s;
            r.x = q[0];
            r.y = q[1];
            r.z = q[2];
            r.w = q[3];
        }

            return r;
    }

    // Combine RPY angles together into a quaternion
    public static Quarts QCombinedAxis(Vectors A)
    {
        Quarts r = new Quarts(0, new Vectors(0, 0, 0));

        // Create matrices from axes
        Matrix4X4 R = Matrix4X4.CreateRoll(A.x);
        Matrix4X4 P = Matrix4X4.CreatePitch(A.y);
        Matrix4X4 Y = Matrix4X4.CreateYaw(A.z);

        // Convert to quaternion 
        Quarts RQ = MatToQuat(R);
        Quarts PQ = MatToQuat(P);
        Quarts YQ = MatToQuat(Y);

        // Multiply together to get combined quarternion
        r = (RQ * PQ) * YQ;

        return r;
    }

    // Create a quaternion translation from vector
    public static Quarts QTrans(Vectors A)
    {
        Quarts r = new Quarts(0, new Vectors(0, 0, 0));
        Quarts TQ = new Quarts(0, new Vectors(0, 0, 0));
        Quarts Angle = new Quarts(0, new Vectors(0, 1, 0));

        // Create matrices from axes
        //Matrix4X4 T = Matrix4X4.CreateTrans(A);   For some reason translation matrix not working
        //Vectors Translated = T * A;

        // Convert to quaternion 
        //Quarts TQ = MatToQuat(T);

        // Create quaternion from vector
        TQ.SetAxis(A);
        //TQ = Angle * TQ * Angle.Inverse();

        // Set return value to the new position
        r = TQ;
        //r.SetAxis(Translated);
        return r;
    }

    // Create a quaternion scalar from vector
    public static Quarts QScale(Vectors A)
    {
        Quarts r = new Quarts(0, new Vectors(0, 0, 0));

        // Create matrices from axes
        Matrix4X4 S = Matrix4X4.CreateScale(A);

        // Convert to quaternion 
        Quarts SQ = MatToQuat(S);

        // Set return value to the new position
        r = SQ;

        return r;
    }

    // Combine all quarternions (T * RPY * S) through matrices
    public static void QCombineM(Vectors Translation, Vectors Scale, Vectors AxisRots, Vector3[] ModelSpaceVertices, MeshFilter MF)
    {
        // Create Qauternions from raw data
        Quarts T = QTrans(Translation);             // Not used because not working
        Quarts S = QScale(Scale);
        Quarts RPY = QCombinedAxis(AxisRots);

        // Multiply in correct order
        //Quarts Order = T * RPY * S;

        // Define array of vertices
        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];
        

        // move vertexs to new position
        for (int i = 0; i < TransformedVertices.Length; i++)
        {
            // Convert Vertexs into Quaternions
            Vectors temp = new Vectors(ModelSpaceVertices[i].x, ModelSpaceVertices[i].y, ModelSpaceVertices[i].z);
            Quarts Vertexs = new Quarts(0, new Vectors(0, 0, 0));
            Vertexs.SetAxis(temp);

            // Remember TRS order (T omitted for now because its being weird)
            Quarts Combined = RPY * S;

            // Rotate & Scale vertexs
            Quarts result = Combined * Vertexs * Combined.Inverse();
            Vectors finalV = GetAxis(result);

            // This is our basic vertex mover - just added onto the result of the Q matrix
            Vectors Move = new Vectors(Translation.x + finalV.x, Translation.y + finalV.y, Translation.z + finalV.z);

            // Move the Vertex to its new position
            TransformedVertices[i] = new Vector3(Move.x, Move.y, Move.z);

        }
        // Assign new Vertices
        MF.mesh.vertices = TransformedVertices;

        // Sometimes needed to clean up mesh
        MF.mesh.RecalculateNormals();
        MF.mesh.RecalculateBounds();
    }

    // Combine all quarternions without putting them into matrices first (except Scale)  -  this routine was made because theres an issue with plugging them into matrices that makes them shrink and enlarge when rotating
    public static Vectors QCombine(Vectors Translation, Vectors CurrentLocation, Vectors Scale, Vectors AxisRots, Vector3[] ModelSpaceVertices, MeshFilter MF)
    {
        Vectors r = new Vectors(0, 0, 0);

        // Create Qauternions from raw data
        Quarts S = QScale(Scale);
        Quarts R = new Quarts(AxisRots.x, new Vectors(0, 0, 1));
        Quarts P = new Quarts(AxisRots.y, new Vectors(1, 0, 0));
        Quarts Y = new Quarts(AxisRots.z, new Vectors(0, 1, 0));
        Quarts RPY = R * P * Y;
        // Remember TRS order (T omitted for now because its being weird)
        Quarts Combined = RPY * S;

        // Define array of vertices
        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];

        // move vertexs to new position
        for (int i = 0; i < TransformedVertices.Length; i++)
        {
            // Convert Vertexs into Quaternions
            Vectors temp = new Vectors(ModelSpaceVertices[i].x, ModelSpaceVertices[i].y, ModelSpaceVertices[i].z);
            Quarts Vertexs = new Quarts(0, new Vectors(0, 0, 0));
            Vertexs.SetAxis(temp);

            // Rotate & Scale vertexs - Using slerp
            Quarts slerped = SLERP(Combined, Vertexs, Time.deltaTime);
            slerped = Combined * Vertexs * Combined.Inverse();
            Vectors finalV = GetAxis(slerped);

            //// This is our basic vertex mover - just added onto the result of the Q matrix
            //// LERP for that nice smooth movement (Dont forget to add vertex position onto translation amount, u dummy)
            //Vectors NewLocation = new Vectors(Translation.x + finalV.x, Translation.y + finalV.y, Translation.z + finalV.z);
            //Vectors Move = Vectors.VLerp(NewLocation, finalV, Time.deltaTime);

            // Move the Vertex to its new position
            TransformedVertices[i] = new Vector3(finalV.x, finalV.y, finalV.z);
        }

        // Assign new Vertices
        MF.mesh.vertices = TransformedVertices;

        // Sometimes needed to clean up mesh
        MF.mesh.RecalculateNormals();
        MF.mesh.RecalculateBounds();



        /// return world position
        Vectors NewOriginLocation = CurrentLocation + Translation;
        Vectors NewOrigin = Vectors.VLerp(NewOriginLocation, CurrentLocation, Time.deltaTime);

        r = NewOrigin;
        return r;
    }
}

// This part allows me to edit xy in unity from the object inspector
[System.Serializable]
public class Ellipse
{
    public float x;
    public float y;

    // Constructor
    public Ellipse(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public Vectors Create(float t, Vectors Origin)
    {
        Vectors r = new Vectors(0, 0, 0);

        // Create angle for ellipse
        float angle = Mathf.Deg2Rad * 360.0f * t;
        // Find xy values for vertex
        float xAxis = Mathf.Sin(angle) * x;
        float yAxis = Mathf.Cos(angle) * y;

        // Store in r
        r = new Vectors(xAxis + Origin.x, yAxis + Origin.y, 0.0f);

        return r;
    }
}

