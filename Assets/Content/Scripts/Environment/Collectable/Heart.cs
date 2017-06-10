using System;
using UnityEngine;

public class Heart : Collectable
{
    protected override void OnRabitHit(RabbitController rabit)
    {
        LevelController.current.addALife();
        CollectedHide();
    }
}
