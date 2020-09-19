using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    [SerializeField] private float randomHorizontalRange = 50;
    [SerializeField] private float randomVerticalRange = -50;
    [SerializeField] private float speed;
    [SerializeField] private float moveDelayTime;    
    private float moveDelaytimer = 0;


    private Vector3 trashRotation;
    private float trashRotateSpeed;
    
    private bool isMoveComplete;
    private Vector3 targetPosition;

    private Vector3 clockRotation;
    float clockRotateSpeedX = 10f;
    float clockRotateSpeedY = 20f;



    void Start()
    {
        targetPosition = getNewRandomPosition();        

        trashRotation = new Vector3(0, 0, 0);
        trashRotateSpeed = Random.Range(60f, 120f);

        clockRotation = new Vector3(0, -90, 0);
    }


    void Update()
    {
        if (isMoveComplete)
        {
            moveDelaytimer += Time.deltaTime;
            if (moveDelaytimer > moveDelayTime)
            {
                targetPosition = getNewRandomPosition();                

                moveDelaytimer = 0f; //타이머 초기화
            }
        }
        else
        {            
            goToNewPosition();            
        }

        checkMoveComplete();
    }


    Vector3 getNewRandomPosition()
    {
        float posX = Random.Range(-randomHorizontalRange, randomHorizontalRange);
        float posY = Random.Range(randomVerticalRange, -10);
        Vector3 position = new Vector3(posX, posY, 0);
        return position;
    }

    void goToNewPosition()
    {
        Vector3 currentPosition = transform.position;
        transform.position = Vector3.MoveTowards(
            currentPosition,
            targetPosition,
            speed * Time.deltaTime);

        objectMovingAction(currentPosition);
    }

    void checkMoveComplete()
    {
        if (targetPosition == transform.position)
        {
            isMoveComplete = true;
        }
        else
        {
            isMoveComplete = false;
        }
    }


    private void objectMovingAction(Vector3 currentPosition)
    {
        switch(gameObject.tag)
        {
            case "Fish":
                float currentX = transform.position.x;
                float targetX = targetPosition.x;

                if (currentX < targetX) //우측으로 이동시
                {
                    transform.localScale = new Vector3(
                        transform.localScale.x,
                        transform.localScale.x,
                        transform.localScale.x);
                }
                else //좌측으로 이동시 
                {
                    transform.localScale = new Vector3(
                        transform.localScale.x,
                        transform.localScale.x,
                        -transform.localScale.x);
                }
                break;

            case "JellyFish":
                // 이동 방향으로 각도 조절.
                float angle = Mathf.Atan2(
                targetPosition.y - currentPosition.y,
                targetPosition.x - currentPosition.x) * 180 / Mathf.PI;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
                break;

            case "Trash":
                if (trashRotation.x >= 360) // Reset trashRotation
                {
                    trashRotation = new Vector3(0, 0, 0);
                }

                trashRotation = new Vector3(
                    trashRotateSpeed * Time.deltaTime + trashRotation.x,
                    trashRotateSpeed * Time.deltaTime + trashRotation.y,
                    trashRotateSpeed * Time.deltaTime + trashRotation.z
                    );

                transform.rotation = Quaternion.Euler(trashRotation);
                break;

            case "Clock":
                float xDefault = 0f;
                float xRange = 20f;

                float yDefault = -90f;
                float yRange = 20f;
                
                if ((clockRotation.x >= xDefault + xRange) ||
                        (clockRotation.x <= xDefault - xRange))
                {
                    clockRotateSpeedX *= -1;
                }
                                
                if ((clockRotation.y >= yDefault + yRange) ||
                        (clockRotation.y <= yDefault - yRange))
                {
                    clockRotateSpeedY *= -1;
                }

                clockRotation.x += clockRotateSpeedX * Time.deltaTime;
                clockRotation.y += clockRotateSpeedY * Time.deltaTime;
                transform.rotation = Quaternion.Euler(clockRotation.x, clockRotation.y, 0);

                break;
        }        
    }
}
