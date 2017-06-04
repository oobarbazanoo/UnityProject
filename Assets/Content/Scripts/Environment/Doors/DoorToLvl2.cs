using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToLvl2 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        RabbitController rabit = collider.GetComponent<RabbitController>();
        if (rabit != null)
        {
            SceneManager.LoadScene("level2");
        }
    }
}
