using UnityEngine.Events;
using UnityEngine;

public class ClickTrigger : MonoBehaviour
{
    public UnityEvent signalOnClick = new UnityEvent();

    public void feelClick()
    {
        this.signalOnClick.Invoke();
    }
}