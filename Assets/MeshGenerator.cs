using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))] // Makes sures that there is always a mesh filter as the same object as that of the script.
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    public int xSize = 20;
    public int zSize = 20;
    Color[] colors;
    Vector3[] vertices;
    int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        //throw new NotImplementedException();
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
    }

    private void CreateShape()
    {
        //Vertex Count = (xSize + 1) * (zSize + 1).
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        colors = new Color[vertices.Length];

        for (int j = 0; j < vertices.Length; j++)
        {
            colors[j] = Color.Lerp(Color.green, Color.gray, vertices[j].y);
        }

        int i = 0;
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * 3f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        int vert = 0; // The vertex we are currently looking at.
        int tris = 0; // To keep track of the triangle we are currently looking at.

        triangles = new int[xSize * zSize * 6];

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {

                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        

        

    }


    private void OnDrawGizmos()
    {

        if (vertices == null)
            return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
