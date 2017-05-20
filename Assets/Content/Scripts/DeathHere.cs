﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHere : MonoBehaviour {

    //Стандартна функція, яка викличеться,
    //коли поточний об’єкт зіштовхнеться із іншим
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Намагаємося отримати компонент кролика
        RabbitController rabit = collider.GetComponent<RabbitController>();
        //Впасти міг не тільки кролик
        if (rabit != null)
        {
            //Повідомляємо рівень, про смерть кролика
            LevelController.current.onRabitDeath(rabit);
        }
    }
}
