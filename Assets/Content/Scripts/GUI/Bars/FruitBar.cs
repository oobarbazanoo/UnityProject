using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBar : MonoBehaviour
{
    private UILabel label;

    void Awake()
    {
        LevelController.SetFruitBar(this.gameObject);
        setLabel();
    }

    private void Start()
    {
        writeToLabel("0/" + LevelController.current.allFruitsOnTheLvl.Length);
    }

    private void setLabel()
    {
        label = this.transform.Find("Label").gameObject.GetComponent<UILabel>();
    }

    public void writeToLabel(string what)
    {
        label.text = what;
    }
}