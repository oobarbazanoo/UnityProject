using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActionListener : MonoBehaviour
{
    public ClickTrigger clickTrigger;

    void Start()
    {
        clickTrigger = this.gameObject.GetComponent<ClickTrigger>();
        clickTrigger.signalOnClick.AddListener(this.whenClickOccured);
    }

    void whenClickOccured()
    {
        Debug.Log("play");
    }
}
