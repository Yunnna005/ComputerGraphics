using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model
{
    internal List<Vector3> vertices;
    internal List<Vector3Int> faces;
    internal List<Vector2> texture_coordinates;
    internal List<Vector3Int> texture_index_list;
    List<Vector3> normals;

    public Model()
    {
        vertices = new List<Vector3>();
        faces = new List<Vector3Int>();

        texture_coordinates = new List<Vector2>();
        texture_index_list = new List<Vector3Int>();
        normals = new List<Vector3>();

        loadModelData();
    }

    private void loadModelData()
    {
        loadVertices();
        loadFaces();
        loadTextureCoordinates();
        texture_coordinates = Adjust_Texture_Coordinates(texture_coordinates, 512f, 512f);
        loadTextureIndexList();
        loadNormals();
    }

    #region Model details (Verts, Faces, Normals) 
    private void loadNormals()
    {
        //Front
        normals.Add(new Vector3(0, 0, -1)); 
        normals.Add(new Vector3(0, 0, -1));
        normals.Add(new Vector3(0, 0, -1));
        normals.Add(new Vector3(0, 0, -1));
        //Back
        normals.Add(new Vector3(0, 0, 1));
        normals.Add(new Vector3(0, 0, 1));
        normals.Add(new Vector3(0, 0, 1));
        normals.Add(new Vector3(0, 0, 1));
        //Top
        normals.Add(new Vector3(1, 1, 0).normalized);
        normals.Add(new Vector3(1, 1, 0).normalized);
        //Vertical Left
        normals.Add(new Vector3(-1, 0, 0));
        normals.Add(new Vector3(-1, 0, 0));
        //Vertical Right
        normals.Add(new Vector3(1, 0, 0));
        normals.Add(new Vector3(1, 0, 0));
        //Gorizontal Top
        normals.Add(new Vector3(0, 1, 0));
        normals.Add(new Vector3(0, 1, 0));
        //Gorizontal Bottom
        normals.Add(new Vector3(0, -1, 0));
        normals.Add(new Vector3(0, -1, 0));
        //Gorizontal Right
        normals.Add(new Vector3(1, 0, 0));
        normals.Add(new Vector3(1, 0, 0));
    }

    private void loadFaces()
    {  
        faces.Add(new Vector3Int(0, 5, 4));    //Front right
        faces.Add(new Vector3Int(0, 4, 3));    //Front right

        faces.Add(new Vector3Int(0, 3, 2));
        faces.Add(new Vector3Int(0, 2, 1));

        faces.Add(new Vector3Int(6, 10, 11));  //Back right
        faces.Add(new Vector3Int(6, 9, 10));   //Back right
        faces.Add(new Vector3Int(6, 8, 9));    //Back left
        faces.Add(new Vector3Int(6, 7, 8));    //Back left
        
        faces.Add(new Vector3Int(4, 11, 10));  //Top
        faces.Add(new Vector3Int(4, 5, 11));   //Top
       
        faces.Add(new Vector3Int(3, 4, 10));   //Vertical Left
        faces.Add(new Vector3Int(3, 10, 9));   //Vertical Left


        faces.Add(new Vector3Int(0, 11, 5));   //Vertical Right
        faces.Add(new Vector3Int(0, 6, 11));   //Vertical Right

        faces.Add(new Vector3Int(2, 3, 9));    //Gorizontal Top
        faces.Add(new Vector3Int(2, 9, 8));    //Gorizontal Top

        faces.Add(new Vector3Int(0, 7, 6));    //Gorizontal Bottom
        faces.Add(new Vector3Int(0, 1, 7));    //Gorizontal Bottom
        
        faces.Add(new Vector3Int(1, 2, 8));    //Gorizontal Right
        faces.Add(new Vector3Int(1, 8, 7));    //Gorizontal Right
        
    }

    private void loadVertices()
    {
        vertices.Add(new Vector3(-1, -3, 1));   //0
        vertices.Add(new Vector3(3, -3, 1));    //1
        vertices.Add(new Vector3(3, -2, 1));    //2
        vertices.Add(new Vector3(0, -2, 1));    //3
        vertices.Add(new Vector3(0, 2, 1));     //4
        vertices.Add(new Vector3(-1, 3, 1));    //5
        vertices.Add(new Vector3(-1, -3, -1));  //6
        vertices.Add(new Vector3(3, -3, -1));   //7
        vertices.Add(new Vector3(3, -2, -1));   //8
        vertices.Add(new Vector3(0, -2, -1));   //9
        vertices.Add(new Vector3(0, 2, -1));    //10
        vertices.Add(new Vector3(-1, 3, -1));   //11
    }
    #endregion

    #region Texture Details
    private void loadTextureCoordinates()
    {
        texture_coordinates.Add(new Vector2(45,276));   //0
        texture_coordinates.Add(new Vector2(188,276));  //1
        texture_coordinates.Add(new Vector2(188,242));  //2
        texture_coordinates.Add(new Vector2(82,242));   //3
        texture_coordinates.Add(new Vector2(82,90));    //4
        texture_coordinates.Add(new Vector2(45,68));    //5
        texture_coordinates.Add(new Vector2(233,276));    //6
        texture_coordinates.Add(new Vector2(378,276));    //7
        texture_coordinates.Add(new Vector2(378,242));    //8
        texture_coordinates.Add(new Vector2(271,242));    //9
        texture_coordinates.Add(new Vector2(271,90));    //10
        texture_coordinates.Add(new Vector2(233,68));    //11
        texture_coordinates.Add(new Vector2(128,392));    //12
        texture_coordinates.Add(new Vector2(351,392));    //13
        texture_coordinates.Add(new Vector2(351,351));    //14
        texture_coordinates.Add(new Vector2(128,351));    //15
    }

    private void loadTextureIndexList()
    {
        texture_index_list.Add(new Vector3Int(0, 5, 4));     //Front right
        texture_index_list.Add(new Vector3Int(0, 4, 3));     //Front right
        texture_index_list.Add(new Vector3Int(0, 3, 2));     //Front left
        texture_index_list.Add(new Vector3Int(0, 2, 1));     //Front left
        texture_index_list.Add(new Vector3Int(6, 10, 11));   //Back right
        texture_index_list.Add(new Vector3Int(6, 9, 10));    //Back right
        texture_index_list.Add(new Vector3Int(6, 8, 9));     //Back left
        texture_index_list.Add(new Vector3Int(6, 7, 8));     //Back left
        texture_index_list.Add(new Vector3Int(12, 14, 13));  //Top
        texture_index_list.Add(new Vector3Int(12, 15, 14));  //Top
        texture_index_list.Add(new Vector3Int(15, 14, 13));  //Vertical Left
        texture_index_list.Add(new Vector3Int(15, 13, 12));  //Vertical Left
        texture_index_list.Add(new Vector3Int(15, 13, 14));  //Vertical Right
        texture_index_list.Add(new Vector3Int(15, 12, 13));  //Vertical Right
        texture_index_list.Add(new Vector3Int(12, 15, 14));  //Gorizontal Top
        texture_index_list.Add(new Vector3Int(12, 14, 13));  //Gorizontal Top
        texture_index_list.Add(new Vector3Int(12, 14, 15));  //Gorizontal Bottom
        texture_index_list.Add(new Vector3Int(12, 13, 14));  //Gorizontal Bottom
        texture_index_list.Add(new Vector3Int(12, 15, 14));  //Gorizontal Right
        texture_index_list.Add(new Vector3Int(12, 14, 13));  //Gorizontal Right
    }

    List<Vector2> Adjust_Texture_Coordinates(List<Vector2> pixel, float ResX, float ResY)
    {
        List<Vector2> coordinates = new List<Vector2>();
        foreach (Vector2 v in pixel) 
            coordinates.Add(new Vector2(v.x/ResX, 1-v.y/ResY));
        return coordinates;
    }
    #endregion

    public GameObject CreateUnityGameObject()
    {
        Mesh mesh = new Mesh();
        GameObject newGO = new GameObject();

        MeshFilter mesh_filter = newGO.AddComponent<MeshFilter>();
        MeshRenderer mesh_renderer = newGO.AddComponent<MeshRenderer>();

        List<Vector3> coords = new List<Vector3>();
        List<int> dummy_indices = new List<int>();
        List<Vector2> text_coords = new List<Vector2>();
        List<Vector3> normalz = new List<Vector3>();

        for (int i = 0; i < faces.Count; i++)
        {
            Vector3 normal_for_face = normals[i];

            normal_for_face = new Vector3(normal_for_face.x, normal_for_face.y, -normal_for_face.z);

            coords.Add(vertices[faces[i].x]); dummy_indices.Add(i * 3); 
            text_coords.Add(texture_coordinates[texture_index_list[i].x]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].y]); dummy_indices.Add(i * 3 + 2); 
            text_coords.Add(texture_coordinates[texture_index_list[i].y]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].z]); dummy_indices.Add(i * 3 + 1); 
            text_coords.Add(texture_coordinates[texture_index_list[i].z]); normalz.Add(normal_for_face);
        }

        mesh.vertices = coords.ToArray();
        mesh.triangles = dummy_indices.ToArray();
        mesh.uv = text_coords.ToArray();
        mesh.normals = normalz.ToArray();
        mesh_filter.mesh = mesh;

        return newGO;
    }
}
