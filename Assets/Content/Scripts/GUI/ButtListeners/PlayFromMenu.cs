using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayFromMenu : MonoBehaviour
{
    public ClickTrigger clickTrigger;
    public GameObject prefabAfterSceneGUI;

    void Start()
    {
        clickTrigger = this.gameObject.GetComponent<ClickTrigger>();
        clickTrigger.signalOnClick.AddListener(this.whenClickOccured);
    }

    void whenClickOccured()
    {
        SceneManager.LoadScene("chooseLevel");
    }
}
