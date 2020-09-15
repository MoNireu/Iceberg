using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public static bool isWater = false;

    [SerializeField]
    private Color waterColor = new Color(0, 0.4f, 0.7f, 0.6f);
    [SerializeField]
    private float waterFogDensity;

    private Color defaultFogColor = RenderSettings.fogColor;    
    private float defaultFogDensity = RenderSettings.fogDensity;
    private bool defaultFog = RenderSettings.fog;
    private Material defaultSkybox = RenderSettings.skybox;
    private Material noSkybox;


    // Start is called before the first frame update
    void Start()
    {
        defaultSkybox = RenderSettings.skybox;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetInWater(other);            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GetOutWater(other);            
        }
    }


    private void GetInWater(Collider _player)
    {
        isWater = true;

        RenderSettings.fog = true;
        RenderSettings.fogColor = waterColor;
        RenderSettings.fogDensity = waterFogDensity;
        RenderSettings.skybox = noSkybox;
    }

    private void GetOutWater(Collider _player)
    {
        isWater = false;

        RenderSettings.fog = false;
        RenderSettings.fogColor = defaultFogColor;
        RenderSettings.fogDensity = defaultFogDensity;
        RenderSettings.skybox = defaultSkybox;
    }
}
