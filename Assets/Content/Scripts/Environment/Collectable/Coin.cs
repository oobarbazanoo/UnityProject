using UnityEngine;

public class Coin : Collectable
{
    protected override void OnRabitHit(RabbitController rabit)
    {
        CollectedHide();
    }
}
