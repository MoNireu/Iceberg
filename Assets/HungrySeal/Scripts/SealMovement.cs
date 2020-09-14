﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealMovement : MonoBehaviour
{

    public Rigidbody playerRd;
    public Transform groundCheck;
    public LayerMask groundMask;
    private Joystick joystick;
    private Joybutton joybutton;

    [SerializeField]
    private float originalHorizontalForce = 2500f;
    [SerializeField]
    private float originalVerticalForce = 3000f;

    private float horizontalForce;
    private float verticalForce;


    private bool isWater;
    private bool isGround;

    private float horizontalRotate = -80f;
    private float verticalRotate = 0f;


    // gravity
    [SerializeField]
    public float groundGravity = -15f;
    [SerializeField]
    public float oceanGravity = -1f;
    [SerializeField]
    public float groundCheckRadius = 0.4f;


    //// Start is called before the first frame update
    void Start()
    {
        horizontalForce = originalHorizontalForce;
        verticalForce = originalVerticalForce;

        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
    }

    //// Update is called once per frame
    void Update()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);


        // 공중시 횡입력 감소
        if (!isWater && !isGround)
        {
            horizontalForce = 1000f;
        }
        else
        {
            horizontalForce = originalHorizontalForce;
        }
        
        // 대쉬 기능.
        if (Input.GetKeyDown(KeyCode.Space) || joybutton.Pressed)
        {
            horizontalForce = 20000f;
            verticalForce = 20000f;
            Debug.Log("Pressed!");
        }else
        {
            horizontalForce = originalHorizontalForce;
            verticalForce = originalVerticalForce;
        }

        // 조이스틱 입력
        float inputX = joystick.Horizontal;
        float inputY = joystick.Vertical;
        //float inputX = Input.GetAxis("Horizontal");
        //float inputY = Input.GetAxis("Vertical");

        // 종 이동.
        if (isWater)
        {
            playerRd.AddForce(0, inputY * verticalForce * Time.deltaTime, 0);
            verticalRotate -= 4f * inputY;
            verticalRotate = Mathf.Clamp(verticalRotate, -60f, 60f);
            transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
        }
        else
        {
            centerPitch();
        }

        // 횡 이동
        playerRd.AddForce(inputX * horizontalForce * Time.deltaTime, 0, 0);
        horizontalRotate += 10f * inputX;
        horizontalRotate = Mathf.Clamp(horizontalRotate, -90f, 90f);
        transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);


        // Pitch 중앙 정렬.
        if(inputY == 0)
        {
            centerPitch();
        }


        //if (Input.GetKey("w")) //up
        //{
        //    if (isWater)
        //    {
        //        playerRd.AddForce(0, verticalForce * Time.deltaTime, 0);
        //        verticalRotate -= 4f;
        //        verticalRotate = Mathf.Clamp(verticalRotate, -60f, 60f);
        //        transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
        //    }
        //    else
        //    {
        //        centerPitch();
        //    }            
        //}

        //if (Input.GetKey("s")) //down
        //{
        //    if (isWater)
        //    {
        //        playerRd.AddForce(0, -verticalForce * Time.deltaTime, 0);
        //        verticalRotate += 4f;
        //        verticalRotate = Mathf.Clamp(verticalRotate, -60f, 60f);
        //        transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
        //    }
        //    else
        //    {
        //        centerPitch();
        //    }           
        //}

        //if (Input.GetKey("a")) //left
        //{            
        //    playerRd.AddForce(-horizontalForce * Time.deltaTime, 0, 0);
        //    horizontalRotate -= 10f;
        //    horizontalRotate = Mathf.Clamp(horizontalRotate, -90f, 90f);
        //    transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
        //}

        //if (Input.GetKey("d")) // right
        //{            
        //    playerRd.AddForce(horizontalForce * Time.deltaTime, 0, 0);
        //    horizontalRotate += 10f;
        //    horizontalRotate = Mathf.Clamp(horizontalRotate, -90f, 90f);
        //    transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
        //}        

        // centerPitch
        //if (!Input.GetKey("w") && !Input.GetKey("s"))
        //{
        //    centerPitch();
        //}

   
    }  

    private void OnTriggerEnter(Collider other)
    {
        isWater = true;     
        Physics.gravity = new Vector3(0, oceanGravity, 0);
    }

    private void OnTriggerExit(Collider other)
    {
        isWater = false;        
        Debug.Log("notWater");
        Physics.gravity = new Vector3(0, groundGravity, 0);
    }

    private void centerPitch()
    {
        float verticalRotateRate = 2f;
        if (verticalRotate > 0)
        {
            verticalRotate -= verticalRotateRate;
            verticalRotate = Mathf.Clamp(verticalRotate, -60f, 60f);
            transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
        }
        else if (verticalRotate < 0)
        {
            verticalRotate += verticalRotateRate;
            verticalRotate = Mathf.Clamp(verticalRotate, -60f, 60f);
            transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, horizontalRotate, 0);
        }
    } 

    
}
