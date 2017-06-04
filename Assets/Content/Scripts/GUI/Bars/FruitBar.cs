using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBar : MonoBehaviour
{
    public int numberOfFruitsInScene;

    private UILabel label;

    void Awake()
    {
        LevelController.SetFruitBar(this.gameObject);
        setLabel();
        writeToLabel("0/"+numberOfFruitsInScene);
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