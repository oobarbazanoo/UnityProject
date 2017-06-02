using System;
using UnityEngine;

public class GreenEnemy : Enemy
{
    private AnimateController animateController;
    private RabbitStats rabbitStats;
    private RabbitController rabbitController;
    private bool isDead;

    protected override void Start()
    {
        base.Start();
        initEverythingBeforehand();
    }

    private void initEverythingBeforehand()
    {
        rabbitStats = LevelController.getRabbit().GetComponent<RabbitStats>();
        animateController = LevelController.getRabbit().GetComponent<AnimateController>();
        rabbitController = LevelController.getRabbit().GetComponent<RabbitController>();
    }

    protected override void Update ()
    {
        base.Update();
    }

    protected override void AttackTheRabbit(GameObject rabbit)
    {
        if (isDead)
        { return; }

        Vector3 rabbitPosition = rabbit.GetComponent<Transform>().position;

        if (RabbitIsNotReachedOnX(rabbitPosition))
        {
            RunToRabbit(rabbitPosition);
        }
        else if (RabbitIsNotReachedOnY(rabbitPosition))
        {
            AnimateOnce("die");
            isDead = true;
        }
        else if(!rabbitStats.isDead)
        {
            PunchRabbit(rabbit);
        }
    }

    private void PunchRabbit(GameObject rabbit)
    {
        base.StopAllAnimations();
        base.AnimateOnce("attack");

        if (rabbitController.isVulnerable)
        {
            if (rabbitStats.rabbitSize == 0)
            { die(); }
            else
            {
                rabbitStats.rabbitSize = 0;
                makeRabbitSmaller(rabbitController);
                rabbitController.isVulnerable = false;
                rabbitController.gameObject.GetComponent<FlashObject>().startBlinking();
            }
        }
    }

    private void die()
    {
        animateController.animate("run", false);
        animateController.animate("jump", false);

        animateController.animate("die", true);
        rabbitStats.isDead = true;
    }

    private void makeRabbitSmaller(RabbitController rabit)
    {
        Transform transgormToChange = rabit.gameObject.GetComponent<Transform>();

        transgormToChange.localScale = new Vector3(transgormToChange.localScale.x / 2, transgormToChange.localScale.y / 2, transgormToChange.localScale.z);
    }

    private bool RabbitIsNotReachedOnY(Vector3 rabbitPosition)
    {
        return Math.Abs(this.transform.position.y - rabbitPosition.y) > 1;
    }

    private bool RabbitIsNotReachedOnX(Vector3 rabbitPosition)
    {
        return Math.Abs(this.transform.position.x - rabbitPosition.x) > 1;
    }

    private void RunToRabbit(Vector3 rabbitPosition)
    {
        base.StopAllAnimations();
        base.AnimateContiniously("running", true);

        Vector3 _destinationVector = rabbitPosition - this.transform.position;
        _destinationVector.y = 0;
        _destinationVector = _destinationVector.normalized;
        this.transform.Translate(_destinationVector * runningSpeed);

        base.turnAccordingWithVectorAlongX(_destinationVector);
    }
}
