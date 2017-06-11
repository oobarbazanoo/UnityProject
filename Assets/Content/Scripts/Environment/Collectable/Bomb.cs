using System;
using System.Collections;
using UnityEngine;

public class Bomb : Collectable
{
    private AnimateController animateController = null;

    protected override void OnRabitHit(RabbitController rabit)
    {
        animateController = rabit.gameObject.GetComponent<AnimateController>();
        RabbitStats stats = rabit.gameObject.GetComponent<RabbitStats>();

        if (stats.isDead)
        { return; }

        if (rabit.isVulnerable)
        {
            CollectedHide();
            if (stats.rabbitSize == 0)
            {
                stats.isDead = true;
                die();
            }
            else
            {
                stats.rabbitSize = 0;
                makeRabbitSmaller(rabit);
                rabit.isVulnerable = false;
                rabit.gameObject.GetComponent<FlashObject>().startBlinking();
            }
        }
    }

    private void die()
    {
        animateController.animate("run", false);
        animateController.animate("jump", false);

        animateController.animate("die", true);
    }

    private void makeRabbitSmaller(RabbitController rabit)
    {
        Transform transgormToChange = rabit.gameObject.GetComponent<Transform>();

        transgormToChange.localScale = new Vector3(transgormToChange.localScale.x / 2 , transgormToChange.localScale.y / 2, transgormToChange.localScale.z);
    }
}
