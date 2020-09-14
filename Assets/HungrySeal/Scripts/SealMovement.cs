using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealMovement : MonoBehaviour
{

    public Rigidbody playerRd;
    public Transform groundCheck;
    public LayerMask groundMask;

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

        // Input Controll

        //dash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            horizontalForce = 20000f;
            verticalForce = 20000f;
        }else
        {
            horizontalForce = originalHorizontalForce;
            verticalForce = originalVerticalForce;
        }


        float InputX = Input.GetAxis("Horizontal");
        float InputY = Input.GetAxis("Vertical");


        if (Input.GetKey("w")) //up
        {
            if (isWater)
            {
                playerRd.AddForce(0, verticalForce * Time.deltaTime, 0);
                verticalRotate -= 4f;
                verticalRotate = Mathf.Clamp(verticalRotate, -60f, 60f);
                transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
            }
            else
            {
                centerPitch();
            }            
        }

        if (Input.GetKey("s")) //down
        {
            if (isWater)
            {
                playerRd.AddForce(0, -verticalForce * Time.deltaTime, 0);
                verticalRotate += 4f;
                verticalRotate = Mathf.Clamp(verticalRotate, -60f, 60f);
                transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
            }
            else
            {
                centerPitch();
            }           
        }

        if (Input.GetKey("a")) //left
        {            
            playerRd.AddForce(-horizontalForce * Time.deltaTime, 0, 0);
            horizontalRotate -= 10f;
            horizontalRotate = Mathf.Clamp(horizontalRotate, -90f, 90f);
            transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
        }

        if (Input.GetKey("d")) // right
        {            
            playerRd.AddForce(horizontalForce * Time.deltaTime, 0, 0);
            horizontalRotate += 10f;
            horizontalRotate = Mathf.Clamp(horizontalRotate, -90f, 90f);
            transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
        }        

        // centerPitch
        if (!Input.GetKey("w") && !Input.GetKey("s"))
        {
            centerPitch();
        }

   
    }  

    private void OnTriggerEnter(Collider other)
    {
        isWater = true;
        jumpCnt = 1;
        Physics.gravity = new Vector3(0, oceanGravity, 0);
    }

    private void OnTriggerExit(Collider other)
    {
        isWater = false;
        jump();
        jumpCnt -= 1;
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
