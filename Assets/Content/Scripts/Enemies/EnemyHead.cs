using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        //Намагаємося отримати компонент кролика
        RabbitController rabit = coll.gameObject.GetComponent<RabbitController>();
        //Впасти міг не тільки кролик
        if (rabit == null)
        { return; Debug.Log("Not dead!"); }

        this.gameObject.transform.parent.gameObject.GetComponent<Enemy>().DieFromRabbit();
        Debug.Log("Dead!");
    }


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
