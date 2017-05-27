using UnityEngine;

public class Fruit : Collectable
{
    protected override void OnRabitHit(RabbitController rabit)
    {
        CollectedHide();
    }
}
