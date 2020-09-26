using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMovement : MonoBehaviour
{

    public Transform playerTf;
    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    [SerializeField]
    private Color waterColor = new Color(0, 0.4f, 0.7f, 0.6f);
    [SerializeField]
    private float waterFogDensity;

    private Color defaultFogColor;
    private float defaultFogDensity;
    private bool defaultFog;
    private Material defaultSkybox;
    private Material noSkybox;


    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - playerTf.position;

        defaultFogColor = RenderSettings.fogColor;
        defaultFogDensity = RenderSettings.fogDensity;
        defaultSkybox = RenderSettings.skybox;
        defaultFog = RenderSettings.fog;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = playerTf.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (transform.position.y < 0)
        {
            waterCam();
        }
        else
        {
            groundCam();
        }
    }


    private void waterCam()
    {        
        RenderSettings.fog = true;
        RenderSettings.fogColor = waterColor;
        RenderSettings.fogDensity = waterFogDensity;
        RenderSettings.skybox = noSkybox;
    }

    private void groundCam()
    {
        RenderSettings.fog = defaultFog;
        RenderSettings.fogColor = defaultFogColor;
        RenderSettings.fogDensity = defaultFogDensity;
        RenderSettings.skybox = defaultSkybox;
    }
}
