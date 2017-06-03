using System;
using UnityEngine;

public class BrownEnemy : Enemy
{
	private RabbitStats rabbitStats;
	private bool isDead;
	public GameObject prefabCarrot;
	private Vector3 directionToThrowCarrot;
    private float timeWhenLastCarrotWasThrown;

    [SerializeField]
    private float throwCarrotLapsDuration;

	protected override void Start()
	{
		base.Start();
		initEverythingBeforehand();
	}

	private void initEverythingBeforehand()
	{
		rabbitStats = Level1Controller.getRabbit().GetComponent<RabbitStats>();
        this.timeWhenLastCarrotWasThrown = 0;
        this.throwCarrotLapsDuration = 1.7f;
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
			TurnToRabbitAndSetDirectionForCarrot (rabbitPosition);

			if(!rabbitStats.isDead)
			{
                if (Time.time - timeWhenLastCarrotWasThrown > throwCarrotLapsDuration)
                {
                    timeWhenLastCarrotWasThrown = Time.time;
                    ThrowCarrot(rabbit);
                }
			}
		}
	}

    public override void DieFromRabbit()
    {
        AnimateOnce("die");
        isDead = true;
    }

	private void TurnToRabbitAndSetDirectionForCarrot(Vector3 rabbitPosition)
	{
		base.StopAllAnimations();
		base.AnimateContiniously("idle", true);

		Vector3 _destinationVector = rabbitPosition - this.transform.position;
		_destinationVector.y = 0;
		_destinationVector = _destinationVector.normalized;
		base.turnAccordingWithVectorAlongX(_destinationVector);
		directionToThrowCarrot = _destinationVector;
	}

	private void ThrowCarrot(GameObject rabbit)
	{
		base.StopAllAnimations();
		base.AnimateOnce("attack");

		//Створюємо копію Prefab
		GameObject obj = GameObject.Instantiate(this.prefabCarrot);
		//Розміщуємо в просторі
		Vector3 positionForCarrot = this.transform.position;

		positionForCarrot.y += 0.75f;

		obj.transform.position = positionForCarrot;
		//Запускаємо в рух
		Carrot carrot = obj.GetComponent<Carrot> ();
        if(directionToThrowCarrot.x < 0)
        { carrot.gameObject.GetComponent<SpriteRenderer>().flipX = true; }
		carrot.launch (directionToThrowCarrot);
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
}
