using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMovement : MonoBehaviour
{

    public Transform playerTf;
    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    private Color originColor;
    private float originFogDensity;

    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - playerTf.position;

        originColor = RenderSettings.fogColor;
        originFogDensity = RenderSettings.fogDensity;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = playerTf.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
    }  
}
