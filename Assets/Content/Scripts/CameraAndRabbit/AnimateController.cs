﻿using UnityEngine;

public class AnimateController : MonoBehaviour
{
    private Animator animator;
    private RabbitController rabbit;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        rabbit = GetComponent<RabbitController>();
	}

    public void animate(string what, bool toAnimate)
    { animator.SetBool(what, toAnimate); }

    public void disableRabbitMovements()
    {
        rabbit.enabled = false;
    }

    public void whenAnimationOfDeathEnds()
    {
        animate("die", false);
        rabbit.enabled = true;
        LevelController.current.onRabitDeath(rabbit);
    }
}
