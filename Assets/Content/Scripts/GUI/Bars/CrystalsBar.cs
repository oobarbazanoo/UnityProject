using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalsBar : MonoBehaviour
{
    void Awake()
    {
        LevelController.SetCrystalsBar(this.gameObject);
    }
}
