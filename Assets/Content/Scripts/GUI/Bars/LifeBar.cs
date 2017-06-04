using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
     void Awake()
    {
        LevelController.SetLifeBar(this.gameObject);
    }


}
