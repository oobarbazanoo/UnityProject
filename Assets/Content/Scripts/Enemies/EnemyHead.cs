using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject rabbit = coll.gameObject;

        //Намагаємося отримати компонент кролика
        RabbitController rabbitController = rabbit.GetComponent<RabbitController>();
        //Впасти міг не тільки кролик
        if (rabbitController == null)
        { return;  }


        if (rabbit.GetComponent<Rigidbody2D>().velocity.y >= 0)
        { return; }

        this.gameObject.transform.parent.gameObject.GetComponent<Enemy>().DieFromRabbit();
        LevelController.current.cameraWhichLooksForRabbit.stopSoundEnemyAttacks();
        rabbit.GetComponent<Rigidbody2D>().AddForce(Vector3.up*12, ForceMode2D.Impulse);
    }

    void Start ()
    {
       // EdgeCollider2D headCollider = this.gameObject.GetComponent<EdgeCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
