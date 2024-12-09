using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientController : MonoBehaviour
{
    [SerializeField] Gradient bgGradient;
    Color bgColor;

    void Update()
    {
        bgColor = bgGradient.Evaluate(ConvertDepthToGradientRange(Depth.DepthValue));
        Camera.main.backgroundColor = bgColor;
    }

    float ConvertDepthToGradientRange(float depth) //the range is 0 - 1
    {
        //Debug.Log("DEPTH: " + depth);
        switch (depth)
        {
            
            case < -2000:
                return 1;
            case > 0:
                return 0;
            default:
                return depth / -2000.0f;
                
        }
    }
}
