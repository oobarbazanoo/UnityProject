using System;
using UnityEngine;

public class Saver : Collectable
{
    protected override void OnRabitHit(RabbitController rabit)
    {
        LevelController.current.setStartPosition(rabit.transform.position);
        CollectedHide();
    }
}

