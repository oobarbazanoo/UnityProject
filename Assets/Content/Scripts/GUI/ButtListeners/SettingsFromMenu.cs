using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsFromMenu : MonoBehaviour
{
    public ClickTrigger clickTrigger;

    public GameObject settingsPrefab;

    public static SettingsFromMenu current;

    private void Awake()
    {
        current = this;
    }

    void Start()
    {
        clickTrigger = this.gameObject.GetComponent<ClickTrigger>();
        clickTrigger.signalOnClick.AddListener(this.whenClickOccured);
    }

    void whenClickOccured()
    {
        showSettings();
    }

    public void showSettings()
    {
        GameObject parent = UICamera.first.transform.parent.gameObject;
        GameObject obj = NGUITools.AddChild(parent, settingsPrefab);
        PopUpWindowConfig popup = obj.GetComponent<PopUpWindowConfig>();
    }
}
