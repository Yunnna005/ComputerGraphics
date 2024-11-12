using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonDrawing : MonoBehaviour
{
    public Material fillMaterial;
    Model model;
    private void Start()
    {
        model = new Model();
        CreatePolygonMesh();
    }

    private void CreatePolygonMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = model.vertices.ToArray();

        int[] triangles = new int[model.faces.Count * 3];
        for (int i = 0; i < model.faces.Count; i++)
        {
            triangles[i * 3] = model.faces[i].x;     // First vertex index
            triangles[i * 3 + 1] = model.faces[i].y; // Second vertex index
            triangles[i * 3 + 2] = model.faces[i].z; // Third vertex index
        }

        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        //MeshFilter meshFilter = model.
    }
}
