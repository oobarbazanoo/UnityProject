using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHere : MonoBehaviour
{
    //Стандартна функція, яка викличеться,
    //коли поточний об’єкт зіштовхнеться із іншим
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Намагаємося отримати компонент кролика
        RabbitController rabit = collider.GetComponent<RabbitController>();
        //Впасти міг не тільки кролик
        if (rabit != null)
        {
            RabbitStats stats = rabit.gameObject.GetComponent<RabbitStats>();
            if (stats.rabbitSize == 1)
            {
                stats.rabbitSize = 0;
                makeRabbitSmaller(rabit);
            }

            LevelController.current.onRabitDeath(rabit);
        }
    }

    private void makeRabbitSmaller(RabbitController rabit)
    {
        Transform transgormToChange = rabit.gameObject.GetComponent<Transform>();

        transgormToChange.localScale = new Vector3(transgormToChange.localScale.x / 2, transgormToChange.localScale.y / 2, transgormToChange.localScale.z);
    }
}
