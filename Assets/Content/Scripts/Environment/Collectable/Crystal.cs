using UnityEngine;

public class Crystal : Collectable
{
    public enum TypeOfCrystal
    {
        Blue, Red, Green
    }

    [SerializeField]
    private TypeOfCrystal type;


    protected override void OnRabitHit(RabbitController rabit)
    {
        CollectedHide();
        LevelController.current.crystalWasCollected(type);
    }
}