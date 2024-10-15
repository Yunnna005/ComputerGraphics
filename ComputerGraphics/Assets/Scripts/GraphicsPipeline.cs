using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GraphicsPipeline : MonoBehaviour
{
    Model myModel;

    void Start()
    {
        /*myModel = new Model();
        List<Vector4> verts = convertToHomg(myModel.vertices);

        //Rotation
        Vector3 axis = (new Vector3(19, 1, 1)).normalized;
        Matrix4x4 rotationMatrix =Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-28, axis), Vector3.one);
        //displayMatrix(rotationMatrix);
        List<Vector4> imageAfterRotation = applyTransformation(verts, rotationMatrix);
        //displayVert(imageAfterRotation);

        //Scale
        Vector3 scale = (new Vector3(3, 1, 1));
        Matrix4x4 scaleMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
        //displayMatrix(scaleMatrix);
        List<Vector4> imageAfterScale = applyTransformation(imageAfterRotation, scaleMatrix);
        //displayVert(imageAfterScale);

        //Translation
        Vector3 translation = (new Vector3(-3, -3, 2));
        Matrix4x4 translationMatrix = Matrix4x4.TRS(translation, Quaternion.identity, Vector3.one);
        //displayMatrix(translationMatrix);
        List<Vector4> imageAfterTranslation = applyTransformation(imageAfterScale, translationMatrix);
        //displayVert(imageAfterTranslation);

        //Single Transformation Matrix
        Matrix4x4 singleTransformationMatrix = translationMatrix * scaleMatrix* rotationMatrix;
        //displayMatrix(singleTransformationMatrix);
        List<Vector4> imageAfterTransformation = applyTransformation(verts, singleTransformationMatrix);
        //displayVert(imageAfterTransformation);

        Vector3 cameraPosition = new Vector3(21, 4, 51);
        Vector3 cameraLootAt = new Vector3(1, 3, 1).normalized;
        Vector3 cameraLootUp = new Vector3(2, 1, 19).normalized;
        Matrix4x4 cameraMatrix = Matrix4x4.LookAt(cameraPosition, cameraLootAt, cameraLootUp);
        //displayMatrix(cameraMatrix);
        List<Vector4> imageAfterCameraMetrix = applyTransformation(imageAfterTranslation, cameraMatrix);
        //displayVert(imageAfterViewingMetrix);

        //Projection z=-1
        Matrix4x4 projectionMatrix = Matrix4x4.Perspective(90, 1, 1, 1000);
        //displayMatrix(projectionMatrix);
        List<Vector4> imageAfterProjection =  applyTransformation(imageAfterCameraMetrix, projectionMatrix);
        //displayVert(imageAfterProjection);

        Matrix4x4 matrixForEverything = projectionMatrix * cameraMatrix* singleTransformationMatrix;
        //displayMatrix(matrixForEverything);
        List<Vector4> imageFinal = applyTransformation(verts, matrixForEverything);
        //displayVert(imageFinal);*/


        #region Test OutCode Script
        OutCode outCode1 = new OutCode(new Vector2(0.6f, 2.2f));
        //outCode1.Display();

        OutCode outCode2 = new OutCode(new Vector2(2.6f, -4.2f));
        //outCode2.Display();

        //print((outCode1 == outCode2));
        #endregion

        #region Test Line Clipping
        Vector2 start = new Vector2(0.1f, 0.1f);
        Vector2 end = new Vector2(0.2f, 0.2f);

        bool result1 = LineClipping(ref start, ref end);
        print(result1);

        Vector2 start2 = new Vector2(-5f, -1f);
        Vector2 end2 = new Vector2(-2f, -2f);

        bool result2 = LineClipping(ref start2, ref end2);
        print(result2);
        #endregion

        #region Test Intersect
        Vector2 start3 = new Vector2(-1.5f, 0.5f);
        Vector2 end3 = new Vector2(0.5f, -1.5f);

        Vector2 result_Up_edge = Intersect(start3, end3, 0);
        Vector2 ecpected_Up = new Vector2(-2, 1);
        print("Result: "+result_Up_edge+ "  Expected: "+ecpected_Up);

        #endregion
    }

    /*if strat_oc == inScrean_oc
     * return LineClip (ref end, ref start);
     * 
     * 
     */

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

        // start is outsude screen

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

    Vector2 Intersect(Vector2 start, Vector2 end, int edge)
    {
        // int edge 0 = up, 1 = down, 2 = left, 3 = right
        float m = (end.y - start.y) / (end.x - start.x);

        // y = mx+c  or  x = y-c/m
        float c = start.y - m * start.x;

        switch (edge)
        {
            case 0:    //up
                return new Vector2((1-c)/m , 1); // y = 1
            case 1:    //down
                return new Vector2((-1-c)/m , -1); // y = -1
            case 2:    //left
                return new Vector2(-1, c-m); // left x = -1
            case 3:    //right
                return new Vector2(1, m+c); //right x = 1
        }

        return new Vector2(0,0);
    }

    #endregion
}
