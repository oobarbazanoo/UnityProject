using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Vector3 MoveBy;
    public float movingSpeed = 0.2f;
    public float waitTime = 4f;

    private float _waitTime;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Transform rabbitTransform;

    Vector3 pointA;
    Vector3 pointB;
    Vector3 target;
    Vector3 destinationVector;

    protected virtual void Start()
    {
        initializeNeededComponents();
        initializePointsToMoveToAndFrom();
        setFirstTargetToInitializedPointB();
        setFirstDestinationVector();
        setFirstWaitTime();
    }

    private void initializeNeededComponents()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        animator = this.gameObject.GetComponent<Animator>();
        rabbitTransform = LevelController.getRabbit().GetComponent<Transform>();
    }

    private void initializePointsToMoveToAndFrom()
    {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
    }

    private void setFirstTargetToInitializedPointB()
    {
        this.target = this.pointB;
    }

    private void setFirstDestinationVector()
    {
        Vector3 _destinationVector = this.target - this.transform.position;
        _destinationVector.z = 0;
        this.destinationVector = Vector3.Normalize(_destinationVector);
    }

    private void setFirstWaitTime()
    {
        _waitTime = waitTime;
    }

    protected virtual void Update()
    {
        moveAndIfArrivedWait();
        ifRabbitNearAttack();
    }

    private void ifRabbitNearAttack()
    {
        if (ifRabbitNear())
        {AttackTheRabbit(LevelController.getRabbit()); }
    }

    private bool ifRabbitNear()
    {
        if(itIsBetween(rabbitTransform.position.x, pointB.x, pointA.x))
        {return true;}

        return false;
        
    }

    private bool itIsBetween(float it, float left, float right)
    {
        return (left < it) && (it < right);
    }

    private void moveAndIfArrivedWait()
    {
        if (isArrived(this.transform.position, target))
        {
            if (_waitTime == waitTime)
            { stopWalkigAndStartBeingIdle(); }

            _waitTime -= Time.deltaTime;
            if (_waitTime <= 0)
            {
                turnAround();
                changeTarget();
                resetWaitTimeForFutureWaitOnTheArriving();
                stopBeingIdleAndStartWalkig();
            }
        }
        else
        {
            this.transform.Translate(destinationVector * movingSpeed);
        }
    }

    private void stopBeingIdleAndStartWalkig()
    {
        AnimateContiniously("idle", false);
        AnimateContiniously("walking", true);
    }

    private void stopWalkigAndStartBeingIdle()
    {
        AnimateContiniously("walking", false);
        AnimateContiniously("idle", true);
    }

    private void resetWaitTimeForFutureWaitOnTheArriving()
    {
        _waitTime = waitTime;
    }

    private void changeTarget()
    {
        target = (target == pointA) ? pointB : pointA;
    }

    private void turnAround()
    {
        destinationVector = (-1) * destinationVector;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.06f;
    }

    private void AnimateContiniously(string what, bool toAnimate)
    {
        animator.SetBool(what, toAnimate);
    }

    protected virtual void AttackTheRabbit(GameObject rabbit)
    {
        Debug.Log("In a base class");
    }
}
