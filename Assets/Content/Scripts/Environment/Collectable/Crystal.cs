using UnityEngine;

public class Crystal : Collectable
{
    protected override void OnRabitHit(RabbitController rabit)
    {
        CollectedHide();
    }
}