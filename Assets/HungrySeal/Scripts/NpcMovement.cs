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
    


    void Start()
    {
        targetPosition = getNewRandomPosition();        
        trashRotation = new Vector3(0, 0, 0);
        trashRotateSpeed = Random.Range(60f, 120f);
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

      
        if (gameObject.tag == "Fish")
        {
            changeHeading();
        }
        else if (gameObject.tag == "JellyFish")
        {
            float angle = Mathf.Atan2(
            targetPosition.y - currentPosition.y,
            targetPosition.x - currentPosition.x) * 180 / Mathf.PI;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }
        else // trash
        {
            Debug.Log("trashRotation = " + trashRotation);
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
        }

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


    void changeHeading()
    {
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
    }   
}
