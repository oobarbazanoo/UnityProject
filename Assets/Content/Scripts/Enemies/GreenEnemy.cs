using UnityEngine;

public class GreenEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update ()
    {
        base.Update();
    }

    protected override void AttackTheRabbit(GameObject rabbit)
    {
        Debug.Log("in subclass!");
    }
}
