using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontIceMovment : MonoBehaviour
{
    public Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(30000f * Time.deltaTime, 0, 0);
    }
}
