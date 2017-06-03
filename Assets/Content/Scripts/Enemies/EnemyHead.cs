using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        //Намагаємося отримати компонент кролика
        RabbitController rabit = coll.gameObject.GetComponent<RabbitController>();
        //Впасти міг не тільки кролик
        if (rabit == null)
        { return;  }


        if (rabit.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
        { return; }

        this.gameObject.transform.parent.gameObject.GetComponent<Enemy>().DieFromRabbit();
    }

    void Start ()
    {
       // EdgeCollider2D headCollider = this.gameObject.GetComponent<EdgeCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
