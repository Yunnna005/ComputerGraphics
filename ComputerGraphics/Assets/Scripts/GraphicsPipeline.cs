using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphicsPipeline : MonoBehaviour
{
    Model myModel;
    Texture2D screenTexture;
    Renderer screenRender;
    GameObject plane;
    private float angle;

    Vector2 a2;
    Vector2 b2;
    Vector2 c2;
    Vector2 A;
    Vector2 B;

    void Start()
    {
        plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        screenRender = plane.GetComponent<Renderer>();

        plane.transform.up = -Vector3.forward;

        screenTexture = new Texture2D(1024, 1024);
        screenRender.material.mainTexture = screenTexture;
        myModel = new Model();

    }
    private void Update()
    {
        RenderModelToTexture();
    }

    #region Create Model
    private void displayVert(List<Vector4> imageAfterRotation)
    {
        string matrixDisplay = "";
        for(int i = 0; i<imageAfterRotation.Count; i++)
        {
            Vector4 vert = imageAfterRotation[i];
            matrixDisplay += $"[{vert.x} {vert.y} {vert.z} {vert.w}]" + "\n";
        }
        print(matrixDisplay);
    }

    private List<Vector4> convertToHomg(List<Vector3> vertices)
    {
        List<Vector4> output = new List<Vector4>();

        foreach (Vector3 v in vertices)
        {
            output.Add(new Vector4(v.x, v.y, v.z, 1.0f));
        }
        return output;

    }
    private List<Vector4> applyTransformation(List<Vector4> verts, Matrix4x4 tranformMatrix)
    {
        List<Vector4> output = new List<Vector4>();
        foreach (Vector4 v in verts)
        { output.Add(tranformMatrix * v); }

        return output;

    }

    private void displayMatrix(Matrix4x4 rotationMatrix)
    {
        for (int i = 0; i < 4; i++)
        { print(rotationMatrix.GetRow(i)); }
    }
    #endregion

    #region Model Matrics
    private void Matrics()
    {
        myModel = new Model();
        List<Vector4> verts = convertToHomg(myModel.vertices);
        //Rotation
        Vector3 axis = (new Vector3(19, 1, 1)).normalized;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-28, axis), Vector3.one);
        displayMatrix(rotationMatrix);
        List<Vector4> imageAfterRotation = applyTransformation(verts, rotationMatrix);
        displayVert(imageAfterRotation);

        //Scale
        Vector3 scale = (new Vector3(3, 1, 1));
        Matrix4x4 scaleMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
        displayMatrix(scaleMatrix);
        List<Vector4> imageAfterScale = applyTransformation(imageAfterRotation, scaleMatrix);
        displayVert(imageAfterScale);

        //Translation
        Vector3 translation = (new Vector3(-3, -3, 2));
        Matrix4x4 translationMatrix = Matrix4x4.TRS(translation, Quaternion.identity, Vector3.one);
        displayMatrix(translationMatrix);
        List<Vector4> imageAfterTranslation = applyTransformation(imageAfterScale, translationMatrix);
        displayVert(imageAfterTranslation);

        //Single Transformation Matrix
        Matrix4x4 singleTransformationMatrix = translationMatrix * scaleMatrix * rotationMatrix;
        displayMatrix(singleTransformationMatrix);
        List<Vector4> imageAfterTransformation = applyTransformation(verts, singleTransformationMatrix);
        displayVert(imageAfterTransformation);

        Vector3 cameraPosition = new Vector3(21, 4, 51);
        Vector3 cameraLootAt = new Vector3(1, 3, 1).normalized;
        Vector3 cameraLootUp = new Vector3(2, 1, 19).normalized;
        Matrix4x4 cameraMatrix = Matrix4x4.LookAt(cameraPosition, cameraLootAt, cameraLootUp);
        displayMatrix(cameraMatrix);
        List<Vector4> imageAfterCameraMetrix = applyTransformation(imageAfterTranslation, cameraMatrix);
        displayVert(imageAfterCameraMetrix);

        //Projection z=-1
        Matrix4x4 projectionMatrix = Matrix4x4.Perspective(90, 1, 1, 1000);
        displayMatrix(projectionMatrix);
        List<Vector4> imageAfterProjection = applyTransformation(imageAfterCameraMetrix, projectionMatrix);
        displayVert(imageAfterProjection);

        Matrix4x4 matrixForEverything = projectionMatrix * cameraMatrix * singleTransformationMatrix;
        displayMatrix(matrixForEverything);
        List<Vector4> imageFinal = applyTransformation(verts, matrixForEverything);
        displayVert(imageFinal);
    }

    #endregion

    #region Clipping
    /// <summary>
    /// Trivial Acceptance
    /// (oc1+oc2) == null
    /// 
    /// Trivial Rejection
    /// (oc1*oc2) != null
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public bool LineClipping(ref Vector2 start, ref Vector2 end)
    {
        OutCode start_oc = new OutCode(start);
        OutCode end_oc = new OutCode(end);

        OutCode inScrean_oc = new OutCode(new Vector2(0, 0));

        if (start_oc + end_oc == inScrean_oc)
        {
            return true;
        }
        if (start_oc * end_oc != inScrean_oc)
        {
            return false;
        }

       if(start_oc == inScrean_oc) { return LineClipping(ref end, ref start); };

        if (start_oc.up)
        {
            start = Intersect(start, end, 0);
            return LineClipping(ref start, ref end);
        }

        if (start_oc.down)
        {
            start = Intersect(start, end, 1);
            return LineClipping(ref start, ref end);
        }

        if (start_oc.left)
        {
            start = Intersect(start, end, 2);
            return LineClipping(ref start, ref end);
        }

        if (start_oc.right)
        {
            start = Intersect(start, end, 3);
            return LineClipping(ref start, ref end);
        }



        return false;
    }

    public Vector2 Intersect(Vector2 start, Vector2 end, int edge)
    {
        // int edge 0 = up, 1 = down, 2 = left, 3 = right

        // Check for vertical line
        if (end.x == start.x)
        {
            return edge switch
            {
                0 => new Vector2(start.x, 1), // Up edge
                1 => new Vector2(start.x, -1), // Down edge
                2 => new Vector2(-1, start.y), // Left edge
                3 => new Vector2(1, start.y),  // Right edge
                _ => new Vector2(float.NaN, float.NaN) // No intersection with up or down edges
            };
        }

        // Check for horizontal line
        if (end.y == start.y)
        {
            return edge switch
            {
                0 => new Vector2(float.NaN, float.NaN), // Up edge (no valid x)
                1 => new Vector2(float.NaN, float.NaN), // Down edge (no valid x)
                _ => new Vector2(float.NaN, float.NaN) // No intersection with left or right edges
            };
        }

        // Regular case: calculate slope and intercept
        float m = (end.y - start.y) / (end.x - start.x);
        // y = mx+c  or  x = y-c/m
        float c = start.y - m * start.x;

        switch (edge)
        {
            case 0: // up
                return new Vector2((1 - c) / m, 1); // y = 1
            case 1: // down
                return new Vector2((-1 - c) / m, -1); // y = -1
            case 2: // left
                return new Vector2(-1, c - m); // left x = -1
            case 3: // right
                return new Vector2(1, m + c); // right x = 1
            default:
                return new Vector2(float.NaN, float.NaN); // Invalid edge
        }
    }

    #endregion

    #region Breshenham

    public List<Vector2Int> Bresh(Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> outList = new List<Vector2Int>();
        outList.Add(start);
        int x, y, dx, dy, neg, pos, p;
        x = start.x;
        y = start.y;
        dx = end.x - x;
        if (dx < 0)
        {
            return Bresh(end, start);
        }
        dy = end.y - y;
        if (dy < 0)
        {
            return NegY(Bresh(NegY(start), NegY(end)));
        }
        if (dy > dx)
        {
            return SwapXY(Bresh(SwapXY(start), SwapXY(end)));
        }
        neg = 2 * (dy - dx);
        pos = 2 * dy;
        p = 2 * dy - dx;

        while (x != end.x)
        {
            x++;
            if (p <= 0)
            {
                p += pos;
            }
            else
            {
                y++;
                p += neg;
            }

            outList.Add(new Vector2Int(x, y));
        }

        return outList;
    }

    private List<Vector2Int> SwapXY(List<Vector2Int> vector2Ints)
    {
        List<Vector2Int> swapList = new List<Vector2Int>();
        foreach (var v in vector2Ints)
        {
            swapList.Add(SwapXY(v));
        }
        return swapList;
    }

    private Vector2Int SwapXY(Vector2Int start)
    {
        return new Vector2Int(start.y, start.x);
    }

    //NegY methods are overloding
    public List<Vector2Int> NegY(List<Vector2Int> vector2Ints)
    {
        List<Vector2Int> negatedList = new List<Vector2Int>();
        foreach (var v in vector2Ints)
        {
            negatedList.Add(NegY(v)); 
        }
        return negatedList;
    }

    public Vector2Int NegY(Vector2Int v)
    {
        return new Vector2Int(v.x, -v.y);
    }

    #endregion

    #region Display 2D Texture 
    private void RenderModelToTexture()
    {
        Destroy(screenTexture);
        screenTexture = new Texture2D(1024, 1024);
        screenRender.material.mainTexture = screenTexture;
        angle += 1;
        //print(angle);
        Matrix4x4 matrix4X4 = Matrix4x4.TRS(new Vector3(0, 0, -10), Quaternion.AngleAxis(angle, Vector3.up), Vector3.one);
        Matrix4x4 mrot = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(angle, Vector3.right), Vector3.one);
        Matrix4x4 superMatrix = mrot * matrix4X4;
        List<Vector4> verts = applyTransformation(convertToHomg(myModel.vertices), superMatrix);

        for (int i = 0; i <myModel.faces.Count; i++)
        {
            Vector3Int face = myModel.faces[i];
            Vector3Int texture = myModel.texture_index_list[i];

            Vector2 a_t = myModel.texture_coordinates[texture.x];
            Vector2 b_t = myModel.texture_coordinates[texture.y];
            Vector2 c_t = myModel.texture_coordinates[texture.z];

            Vector3 a = verts[face.x]; 
            Vector3 b = verts[face.y];  
            Vector3 c = verts[face.z]; 

            a2 = pixelize(Project(a));
            b2 = pixelize(Project(b));
            c2 = pixelize(Project(c));

            A = b2 - a2;
            B = c2 - a2;

            if (Vector3.Cross(b2-a2, c2-b2).z<0)
            {
                EdgeTable edgeTable = new EdgeTable();
                Process(verts[face.x], verts[face.y], edgeTable);
                Process(verts[face.y], verts[face.z], edgeTable);
                Process(verts[face.z], verts[face.x], edgeTable);

                DrawScanLines(edgeTable);
            }
        }
        screenTexture.Apply();
    }

    private void DrawScanLines(EdgeTable edgeTable)
    {
        foreach(var item in edgeTable.edgeTable)
        {
            int y = item.Key;
            int xMin = item.Value.start;
            int xMax = item.Value.end;


            for(int x =xMin; x <= xMax; x++)
            {
                Color color = getColorfromTexture(x,y);
                screenTexture.SetPixel(x, y, color);
            }
        }
    }

    private Color getColorfromTexture(float x_p, int y_p)
    {
        float x = x_p - a2.x;
        float y = y_p - a2.y;

        float r = (x * B.y - y * B.x) / (A.x * B.y - A.y * B.x);
        float s = (A.x * y - x * A.y) / (A.x * B.y - A.y * B.x);

        return Color.red; //change
    }

    private void Process(Vector4 start4D, Vector4 end4D, EdgeTable edgeTable)
    {
        Vector2 start = Project(start4D);
        Vector2 end = Project(end4D);

        //Clipping
        if (LineClipping(ref start, ref end))
        {
            Vector2Int startPix = pixelize(start);
            Vector2Int endPix = pixelize(end);

            List<Vector2Int> points = Bresh(startPix, endPix);
            //setPixels(points);
            edgeTable.Add(points);
        
        }
    }

    private void setPixels(List<Vector2Int> points)
    {
        foreach (Vector2Int point in points)
        {
            screenTexture.SetPixel(point.x, point.y, Color.red);
        }
    }

    private bool IsVisible(Vector4 vector4)
    {
        return vector4.z < 0;
    }

    private Vector2Int pixelize(Vector2 start)
    {
        return new Vector2Int((int)Math.Round((start.x + 1) * 1023 / 2, 0), (int)Math.Round((start.y + 1) * 1023 / 2, 0));
    }

    private Vector2 Project(Vector4 start4D)
    {
        return new Vector2(start4D.x / start4D.z, start4D.y / start4D.z);
    }

    private bool IsBackFace(Vector3 a, Vector3 b)
    {
        return Vector3.Cross(a, b).z>0;
    }
    #endregion
}
