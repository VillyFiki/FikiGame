using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotGenerator : MonoBehaviour
{
    public void Generate()
    {
        var mesh = new Mesh();
        var renderer = GetComponent<MeshFilter>();

        mesh.vertices = new Vector3[]
        {
            new Vector3(0, 0),
            new Vector3(1, 0),
            new Vector3(-0.5f, 0.5f),
            new Vector3(1.5f, 0.5f),
            new Vector3(0.5f, 1),
        };

        

        mesh.triangles = new int[]
        {
            0,1,2,1,2,3,2,3,4
        };

        renderer.mesh = mesh;
    }

    void Start()
    {
        Generate();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var pos = new Vector2(Mathf.Cos(72 * Mathf.Deg2Rad), Mathf.Sin(72 * Mathf.Deg2Rad));
        Gizmos.DrawLine(new Vector3(0, 0),pos);

        var nPos = pos;
        nPos.y += 1;
        Gizmos.DrawLine(pos, nPos);
    }
}

public class Point
{
    public float x { get; set; }
    public float y { get; set; }
}

public class PathParams
{
    public Point Point { get; set; }
    public float Points { get; set; }
    public float Rotation { get; set; }
    public float Ratio { get; set; }
    public float Depth { get; set; }
}

public class Path
{
    public Point Point { get; set; }
    public float Points { get; set; }
    public float Rotation { get; set; }
    public float Ratio { get; set; }
    public float Depth { get; set; }

   public Point ParametricWithRotate (Point point, float a, float b, float alpha, float number)
    {
        var t = 1f;

        return new Point()
        {
            x = (a * Mathf.Cos(t)) * Mathf.Cos(alpha) - (b * Mathf.Sin(t)) * Mathf.Sin(alpha) + point.x,
            y = -(a * Mathf.Cos(t)) * Mathf.Sin(alpha) + -(b * Mathf.Sin(t)) * Mathf.Cos(alpha) + point.y
        };
    }

    private void Smooth()
    {

    }
}