using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToLvl2 : MonoBehaviour
{
    private bool secondLvlIsAvailable;

    private void Start()
    {
        secondLvlIsAvailable = LevelStats.Instance.isSecondLevelAvailable();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(!secondLvlIsAvailable)
        {
            return;
        }

        RabbitController rabit = collider.GetComponent<RabbitController>();
        if (rabit != null)
        {
            SceneManager.LoadScene("level2");
        }
    }
}
