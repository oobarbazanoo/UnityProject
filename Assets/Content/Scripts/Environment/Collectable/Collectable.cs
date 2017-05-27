using UnityEngine;

public class Collectable : MonoBehaviour
{
    protected virtual void OnRabitHit(RabbitController rabit)
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        RabbitController rabit = collider.GetComponent<RabbitController>();
        if (rabit != null)
        {
            this.OnRabitHit(rabit);
        }
    }

    public void CollectedHide()
    {
        Destroy(this.gameObject);
    }
}
