using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
	GameObject carrot;
	Transform carrotTransform;
	Vector3 direction;

    [SerializeField]
    private float carrotFlySpeed;

    [SerializeField]
    private float destroyItAfterThis;

    private AnimateController animateController = null;

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Намагаємося отримати компонент кролика
        RabbitController rabit = collider.GetComponent<RabbitController>();
        //Впасти міг не тільки кролик
        if (rabit == null)
        {return;}

        animateController = rabit.gameObject.GetComponent<AnimateController>();
        RabbitStats stats = rabit.gameObject.GetComponent<RabbitStats>();

        if (rabit.isVulnerable)
        {
            if (stats.rabbitSize == 0)
            { die(); }
            else
            {
                stats.rabbitSize = 0;
                makeRabbitSmaller(rabit);
                rabit.isVulnerable = false;
                rabit.gameObject.GetComponent<FlashObject>().startBlinking();
            }
        }

    }

    private void die()
    {
        animateController.animate("run", false);
        animateController.animate("jump", false);

        animateController.animate("die", true);
    }

    private void makeRabbitSmaller(RabbitController rabit)
    {
        Transform transgormToChange = rabit.gameObject.GetComponent<Transform>();

        transgormToChange.localScale = new Vector3(transgormToChange.localScale.x / 2, transgormToChange.localScale.y / 2, transgormToChange.localScale.z);
    }

    void Start () 
	{
		carrot = this.gameObject;

		carrotTransform = carrot.GetComponent<Transform> ();

        this.carrotFlySpeed = 0.2f;

        this.destroyItAfterThis = 1f;

        StartCoroutine(destroyLater());
    }

    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(destroyItAfterThis);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () 
	{
		if(direction == Vector3.zero)
		{return;}

		carrotTransform.Translate (direction* carrotFlySpeed);
	}

	public void launch(Vector3 direction)
	{
		this.direction = direction;
	}
}
