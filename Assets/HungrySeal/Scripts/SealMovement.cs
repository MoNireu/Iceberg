using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealMovement : MonoBehaviour
{

    public Rigidbody playerRd;
    public Transform playerTf;

    private float upForce = 3000f;
    private float downForce = -1000f;
    private float rightForce = 2500f;
    private float leftForce = -2500f;


    private bool isWater;
    private int jumpCnt = 0;

    private float horizontalRotate = -80f;
    private float verticalRotate = 0f;


    //// Start is called before the first frame update
    void Start()
    {
    }

    //// Update is called once per frame
    void Update()
    {


        if (Input.GetKey("w")) //up
        {
            if (isWater)
            {
                playerRd.AddForce(0, upForce * Time.deltaTime, 0);
                verticalRotate -= 4f;
                verticalRotate = Mathf.Clamp(verticalRotate, -60f, 60f);                
                playerTf.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
            }
        }

        if (Input.GetKey("s")) //down
        {
            playerRd.AddForce(0, downForce * Time.deltaTime, 0);
            verticalRotate += 4f;
            verticalRotate = Mathf.Clamp(verticalRotate, -60f, 60f);            
            playerTf.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);

        }

        if (Input.GetKey("a")) //left
        {
            playerRd.AddForce(leftForce * Time.deltaTime, 0, 0);
            horizontalRotate -= 10f;
            horizontalRotate = Mathf.Clamp(horizontalRotate, -90f, 90f);            
            playerTf.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);

        }

        if (Input.GetKey("d")) // right
        {
            playerRd.AddForce(rightForce * Time.deltaTime, 0, 0);
            horizontalRotate += 10f;
            horizontalRotate = Mathf.Clamp(horizontalRotate, -90f, 90f);            
            playerTf.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);

        }


        if (!Input.GetKey("w") && !Input.GetKey("s"))
        {
            if (verticalRotate > 0)
            {
                verticalRotate -= 1f;
                verticalRotate = Mathf.Clamp(verticalRotate, -60f, 60f);
                playerTf.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
            }
            else if (verticalRotate < 0)
            {
                verticalRotate += 1f;
                verticalRotate = Mathf.Clamp(verticalRotate, -60f, 60f);
                playerTf.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
            }
            else
            {
                playerTf.rotation = Quaternion.Euler(0, horizontalRotate, 0);
            }
        }






    }  

    private void OnTriggerEnter(Collider other)
    {
        isWater = true;
        jumpCnt = 1;
    }

    private void OnTriggerExit(Collider other)
    {
        isWater = false;
        jump();
        jumpCnt -= 1;
        Debug.Log("notWater");
    }

    private void jump()
    {
        if (jumpCnt == 1)
        {
            playerRd.AddForce(0, 500f, 0);
        }
    }
}
