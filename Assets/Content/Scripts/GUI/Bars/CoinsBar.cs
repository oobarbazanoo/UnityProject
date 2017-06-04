using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsBar : MonoBehaviour
{
    void Awake()
    {
        LevelController.SetCoinsBar(this.gameObject);
    }
}
