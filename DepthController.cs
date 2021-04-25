using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthController : MonoBehaviour
{
    [SerializeField]
    private Gradient depthColors;
    private Camera cam;
    [SerializeField]
    private float depth01;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        depth01 = Mathf.Clamp01(-transform.position.y / 205);        
        cam.backgroundColor = depthColors.Evaluate(depth01);
    }
}
