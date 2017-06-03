using System;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    Vector3 startingPosition;
    private static GameObject rabbitObj;

    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }

    public static void SetRabbit(GameObject rabbit)
    { rabbitObj = rabbit;  }

    public void onRabitDeath(MenuRabbitController rabit)
    {
        //При смерті кролика повертаємо на початкову позицію
        rabit.transform.position = this.startingPosition;
        RabbitStats rabbitStats = rabit.gameObject.GetComponent<RabbitStats>();
        rabbitStats.isDead = false;
    }

    public static GameObject getRabbit()
    { return rabbitObj; }

    public static MenuController current;
    void Awake()
    {
        current = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(rabbitObj != null)
        {disableController(rabbitObj, false); }
    }

    private void disableController(GameObject rabbitObj, bool disable)
    {rabbitObj.GetComponent<MenuRabbitController>().enabled = disable;}
}
