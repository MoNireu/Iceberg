using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealMovement : MonoBehaviour
{

    public Rigidbody playerRd;        
    private Joystick joystick;
    private Joybutton joybutton;


    //force
    [SerializeField]
    private float originalHorizontalForce = 2500f;
    [SerializeField]
    private float originalVerticalForce = 3000f;
    [SerializeField]
    private float dashForce = 10000f;
    [SerializeField]
    private float rotateSpeed = 1500f;
    [SerializeField]
    private float originalDrag = 2f;


    // forceParam
    private float horizontalForce;
    private float verticalForce;


    // check
    private bool isWater;
    private bool isGround;


    // rotate
    private float horizontalRotate = -80f;
    private float verticalRotate = 0f;    


    // gravity
    [SerializeField]
    public float groundGravity = -20f;
    [SerializeField]
    public float oceanGravity = -1f;
    [SerializeField]
    


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
        // Z축 밀림방지.
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            0);

        // 조이스틱 입력
        float inputX = joystick.Horizontal;
        float inputY = joystick.Vertical;

        //inputX = Input.GetAxis("Horizontal");
        //inputY = Input.GetAxis("Vertical");
       

            // 대쉬 기능.
        if (Input.GetKeyDown(KeyCode.Space) || joybutton.Pressed)
        {
            horizontalForce = dashForce;
            verticalForce = dashForce;
            Debug.Log("Pressed!");
        }else
        {
            horizontalForce = originalHorizontalForce;
            verticalForce = originalVerticalForce;
        }
         
        // 횡 이동
        playerRd.AddForce(inputX * horizontalForce * Time.deltaTime, 0, 0);
        horizontalRotate += rotateSpeed * inputX * Time.deltaTime;
        horizontalRotate = Mathf.Clamp(horizontalRotate, -90f, 90f);
        transform.rotation = Quaternion.Euler(this.verticalRotate, horizontalRotate, 0);



        // 물일 경우
        if (isWater)
        {
            horizontalForce = originalHorizontalForce; // 횡 입력값 복구.
            playerRd.drag = originalDrag; //저항값 복구.

            // 피치 결정.
            setSealPitch(inputX, inputY);

            // 종 이동
            playerRd.AddForce(0, inputY * verticalForce * Time.deltaTime, 0);            
            transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);            

            // 종 입력이 없을 경우
            if (inputY == 0) // Pitch 중앙 정렬.
            {
                waterCenterPitch();
            }

            
        }
        else if (isGround) // 땅일 경우
        {
            horizontalForce = originalHorizontalForce; // 횡 입력값 복구.
            groundCenterPitch();
            playerRd.drag = 1f;
        }

        else if (!isGround && !isWater)// 공중일 경우
        {
            Debug.Log("Air!");
            horizontalForce = 1000f; // 횡 입력값 감소.
            playerRd.drag = 1f;
            downPitch(); // Pitch 하강.
        }            
    }


    private void waterCenterPitch()
    {
        float verticalRotateRate = 2f;
        if (verticalRotate > 0)
        {
            verticalRotate -= verticalRotateRate;
            verticalRotate = Mathf.Clamp(verticalRotate, -89f, 89f);
            transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
        }
        else if (verticalRotate < 0)
        {
            verticalRotate += verticalRotateRate;
            verticalRotate = Mathf.Clamp(verticalRotate, -89f, 89f);
            transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, horizontalRotate, 0);
        }
    }

    private void groundCenterPitch()
    {
        verticalRotate = 0;
        transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
    }

    private void downPitch()
    {
        float verticalRotateRate = 1.5f;
        verticalRotate += verticalRotateRate;
        verticalRotate = Mathf.Clamp(verticalRotate, -89f, 89f);
        transform.rotation = Quaternion.Euler(verticalRotate, horizontalRotate, 0);
    }


    // Callback Methods
    private void OnTriggerEnter(Collider collider)
    {
        // 땅
        if (collider.gameObject.layer == 8)
        {
            isGround = true;
            Debug.Log("isGround");
        }
        else 
        {
            if (collider.tag == "Water") //물
            {
                isWater = true;
                isGround = false;
                Physics.gravity = new Vector3(0, oceanGravity, 0);
            }            
        }              
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.layer == 8)
        {
            isGround = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == 8)
        {
            isGround = false;
            Debug.Log("notGround");
        }
        else 
        {
            if (collider.tag == "Water")
            {
                isWater = false;
                Physics.gravity = new Vector3(0, groundGravity, 0);
            }                
        }
        
    }


    private void setSealPitch(float x, float y)
    {
        float joystickAngle = (Mathf.Atan2(y, x) * 180 / Mathf.PI);

        if (joystickAngle > -180 && joystickAngle < 0)
        {
            this.verticalRotate = joystickAngle + 180; //우,상            
            if (verticalRotate > 90 && verticalRotate < 180) //우,하
            {
                this.verticalRotate -= 180;
                verticalRotate *= -1;
            }
        }
        if (joystickAngle > 0 && joystickAngle < 180)
        {
            this.verticalRotate = joystickAngle - 180; //좌,상            
            if (verticalRotate > -180 && verticalRotate < -90) //좌,하
            {
                verticalRotate += 180;
                verticalRotate *= -1;
            }
        }

    }




}
