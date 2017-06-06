using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Mode
    {
        OnGuard,
        Attack
    }

    public enum Turned
    {
        Left,
        Right
    }

    public Mode currentMode;
    public Turned turned;

    public Vector3 MoveBy;
    public float walkingSpeed = 0.15f;
    public float runningSpeed = 0.2f;

    public float minimumWaitTime, maximumWaitTime;

    private double _waitTime;
    private double waitTime;

    protected SpriteRenderer spriteRenderer;
    private Animator animator;
    private Transform rabbitTransform;

    Vector3 pointA;

    protected bool running;
    protected bool attacking;

    internal void StopAllAnimations()
    {
        AnimateContiniously("idle", false);
        AnimateContiniously("walking", false);
        AnimateContiniously("running", false);
    }

    public void stopAttacking()
    { attacking = false;}

    public void stopRunning()
    { running = false; }

    Vector3 pointB;
    Vector3 target;
    Vector3 destinationVector;

    protected virtual void Start()
    {
		initializePointsToMoveToAndFrom();
        initializeNeededComponents();
        initializeNeededValues();
        setFirstValues();
    }

    private void initializeNeededValues()
    {
        minimumWaitTime = 2;
        maximumWaitTime = 5;
    }

    private double getRandomDouble(double fromInclusive, double toExclusive)
    {
        return NextDouble(new System.Random(), fromInclusive, toExclusive);
    }

    double NextDouble(System.Random rng, double min, double max)
    {
        return min + (rng.NextDouble() * (max - min));
    }

    private void setFirstValues()
    {
        setFirstTargetToInitializedPointB();
        setFirstDestinationVector();
        setFirstWaitTime();
        setFirstMode();
        setFirstTurned();
    }

    private void setFirstTurned()
    {
        this.turned = Turned.Left;
    }

    private void setFirstMode()
    {
        this.currentMode = Mode.OnGuard;
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

    private double GetRandomTime()
    { return getRandomDouble(minimumWaitTime, maximumWaitTime); }

    private void setFirstWaitTime()
    {
        waitTime = GetRandomTime();
        _waitTime = waitTime;
    }

    protected virtual void Update()
    {
        if (InFight())
        {
            if(RabbitNotNear())
            {
                ContinueToBeOnGuard();
            }
            else
            {
                LevelController.current.cameraWhichLooksForRabbit.playSoundEnemyAttacks();
                DoFight();
                return;
            }
        }

        moveAndIfArrivedWait();
        ifRabbitNearStartAttack();
    }

    private void ContinueToBeOnGuard()
    {
        turnAccordingWithVectorAlongX(destinationVector);
        this.currentMode = Mode.OnGuard;
        StopAllAnimations();
        AnimateContiniously("walking", true);
    }

    private bool RabbitNotNear()
    {
        return !ifRabbitNear();
    }

    private void DoFight()
    { AttackTheRabbit(LevelController.getRabbit()); }

    private bool InFight()
    {
        return this.currentMode == Mode.Attack;
    }

    private void ifRabbitNearStartAttack()
    {
        if (ifRabbitNear())
        {
            resetWaitTimeForFutureWaitOnTheArriving();
            StartAttackTheRabbit();
        }
    }

    private void StartAttackTheRabbit()
    {
        this.currentMode = Mode.Attack;
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
            this.transform.Translate(destinationVector * walkingSpeed);
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
        waitTime = GetRandomTime();
        _waitTime = waitTime;
    }

    private void changeTarget()
    {
        target = (target == pointA) ? pointB : pointA;
    }

    private void turnAround()
    {
        this.turned = (this.turned == Turned.Left) ? Turned.Right : Turned.Left;
        destinationVector = (-1) * destinationVector;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;

		pos.y = 0;
		target.y = 0;

        return Vector3.Distance(pos, target) < 0.06f;
    }

    public void AnimateContiniously(string what, bool toAnimate)
    {
        animator.SetBool(what, toAnimate);
    }

    public void turnAccordingWithVectorAlongX(Vector3 _destinationVector)
    {
        if (_destinationVector.x < 0)
        {
            if (this.turned == Turned.Right)
            {
                this.spriteRenderer.flipX = !this.spriteRenderer.flipX;
                this.turned = Turned.Left;
            }
        }
        else if (_destinationVector.x > 0)
        {
            if (this.turned == Turned.Left)
            {
                this.spriteRenderer.flipX = !this.spriteRenderer.flipX;
                this.turned = Turned.Right;
            }
        }
    }

    public void AnimateOnce(string what)
    { animator.SetTrigger(what); }


    protected void Die()
    {
        StopAllAnimations();
        Destroy(this.gameObject);
    }

    protected virtual void AttackTheRabbit(GameObject rabbit)
    {
        Debug.Log("In a base class");
    }

    public virtual void DieFromRabbit()
    {
        Debug.Log("In a base class");
    }

    public virtual void StopAttack()
    {
        Debug.Log("In a base class");
    }
}
