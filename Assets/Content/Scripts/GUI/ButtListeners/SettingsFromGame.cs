using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsFromGame : MonoBehaviour
{
    public ClickTrigger clickTrigger;
    public GameObject settingsPrefab;

    void Start()
    {
        clickTrigger = this.gameObject.GetComponent<ClickTrigger>();
        clickTrigger.signalOnClick.AddListener(this.whenClickOccured);
    }

    void whenClickOccured()
    {
        GameObject parent = UICamera.first.transform.parent.gameObject;
        GameObject obj = NGUITools.AddChild(parent, settingsPrefab);
        PopUpWindowConfig popup = obj.GetComponent<PopUpWindowConfig>();
    }
}