using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsPipeline : MonoBehaviour
{
    Model model;
    // Start is called before the first frame update
    void Start()
    {
        model = new Model();

        model.CreateUnityGameObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
