using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public static bool isWater = false;

    [SerializeField] private float waterDrag;
    private float originDrag;

    [SerializeField] private Color waterColor;
    [SerializeField] private float waterFogDensity;
    [SerializeField] private float waterGravity;


    private Color originColor;
    private float originFogDensity;
    private float originGravity;

    // Start is called before the first frame update
    void Start()
    {
        originColor = RenderSettings.fogColor;
        originFogDensity = RenderSettings.fogDensity;
        originGravity = -15f;

        originDrag = 1;

        Physics.gravity = new Vector3(0, originGravity, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GetInWater(other);
            Debug.Log("Inside Water!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GetOutWater(other);
            Debug.Log("Outside Water!");
        }
    }


    private void GetInWater(Collider _player)
    {
        isWater = true;
        _player.transform.GetComponent<Rigidbody>().drag = waterDrag;

        RenderSettings.fogColor = waterColor;
        RenderSettings.fogDensity = waterFogDensity;
        Physics.gravity = new Vector3(0, waterGravity, 0);
    }

    private void GetOutWater(Collider _player)
    {
        isWater = false;
        _player.transform.GetComponent<Rigidbody>().drag = originDrag;

        RenderSettings.fogColor = originColor;
        RenderSettings.fogDensity = originFogDensity;
        Physics.gravity = new Vector3(0, originGravity, 0);
    }
}
