using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorAtTheEnd : MonoBehaviour
{
    public GameObject wonMenuPrefab;

    void OnTriggerEnter2D(Collider2D collider)
    {
        RabbitController rabit = collider.GetComponent<RabbitController>();
        if (rabit != null)
        {
            showWonPanel();
        }
    }

    public void showWonPanel ()
    {
        checkInAllLevelInfo();
        Time.timeScale = 0;
        GameObject parent = UICamera.first.transform.parent.gameObject;
        NGUITools.AddChild(parent, wonMenuPrefab);
    }

    private void checkInAllLevelInfo()
    {
        LevelStats.Instance.passedLevelNumber(LevelController.current.lvlNumber);
    }
}
