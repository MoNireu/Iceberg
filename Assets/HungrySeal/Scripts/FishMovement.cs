using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishMovement : MonoBehaviour
{

    [SerializeField] private float randomHorizontalRange;
    [SerializeField] private float randomVerticalRange;
    [SerializeField] private float speed = 2;
    [SerializeField] private float timeForNewPath;
    [SerializeField] private float delayTime;
    private float timer = 0;


    private float defaultScale;
    private bool isMoveComplete;
    private Vector3 targetPosition;

    
    void Start()
    {                
        targetPosition = getNewRandomPosition();
        defaultScale = transform.localScale.x;        
    }

    
    void Update()
    {               
        if (isMoveComplete)
        {
            timer += Time.deltaTime;
            if (timer > delayTime)
            {
                targetPosition = getNewRandomPosition();
                changHeading();

                timer = 0f; //타이머 초기화
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
        //navMeshAgent.SetDestination(getNewRandomPosition());
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime);        
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


    void changHeading()
    {
        float currentX = transform.position.x;
        float targetX = targetPosition.x;

        if (currentX < targetX) //우측으로 이동
        {
            transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);
        }
        else //좌측으로 이동
        {
            transform.localScale = new Vector3(defaultScale, defaultScale, -defaultScale);
        }
    }
    
}
