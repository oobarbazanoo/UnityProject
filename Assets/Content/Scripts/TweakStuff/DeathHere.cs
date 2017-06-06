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
            LevelController.current.cameraWhichLooksForRabbit.followRabbit = false;
            RabbitStats stats = rabit.gameObject.GetComponent<RabbitStats>();
            if (stats.rabbitSize == 1)
            {
                stats.rabbitSize = 0;
                makeRabbitSmaller(rabit);
            }

            StartCoroutine(moveAtTheBeginningLater(rabit));
        }
    }

    IEnumerator moveAtTheBeginningLater(RabbitController rabit)
    {
        yield return new WaitForSeconds(1.5f);
        LevelController.current.cameraWhichLooksForRabbit.followRabbit = true;
        LevelController.current.onRabitDeath(rabit);
    }

    private void makeRabbitSmaller(RabbitController rabit)
    {
        Transform transgormToChange = rabit.gameObject.GetComponent<Transform>();

        transgormToChange.localScale = new Vector3(transgormToChange.localScale.x / 2, transgormToChange.localScale.y / 2, transgormToChange.localScale.z);
    }
}
