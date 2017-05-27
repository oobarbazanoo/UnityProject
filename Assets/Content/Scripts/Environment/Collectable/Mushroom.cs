using System;
using UnityEngine;

public class Mushroom : Collectable
{
    protected override void OnRabitHit(RabbitController rabit)
    {
        CollectedHide();

        RabbitStats stats = rabit.gameObject.GetComponent<RabbitStats>();
        if (stats.rabbitSize == 1)
        { return; }
        else
        {
            stats.rabbitSize = 1;
            makeRabbitBigger(rabit);
        }
    }

    private void makeRabbitBigger(RabbitController rabit)
    {
        Transform transgormToChange = rabit.gameObject.GetComponent<Transform>();

        transgormToChange.localScale = new Vector3(transgormToChange.localScale.x * 2, transgormToChange.localScale.y*2, transgormToChange.localScale.z);
    }
}

