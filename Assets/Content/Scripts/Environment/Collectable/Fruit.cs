using System;
using UnityEngine;

public class Fruit : Collectable
{
    public bool collected;

    private void Start()
    {
        collected = false;
    }

    protected override void OnRabitHit(RabbitController rabit)
    {
        collected = true;
        LevelController.current.fruitWasCollected();
        CollectedHide();
    }

    internal void makeTranslucent()
    {
        Color currentColour = gameObject.GetComponent<Renderer>().material.color;
        currentColour.a = 0.5f;
        gameObject.GetComponent<Renderer>().material.color = currentColour;
    }
}
