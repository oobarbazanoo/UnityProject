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
        Time.timeScale = 0;
        GameObject parent = UICamera.first.transform.parent.gameObject;
        GameObject obj = NGUITools.AddChild(parent, wonMenuPrefab);
        WonPanel wonWindow = obj.GetComponent<WonPanel>();
    }
}
