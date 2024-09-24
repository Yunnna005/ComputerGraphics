using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsPipeline : MonoBehaviour
{
    Model myModel;

    void Start()
    {
        myModel = new Model();
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
        displayVert(imageAfterTransformation);

    }

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
}
