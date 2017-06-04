using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRabbitController : MonoBehaviour
{
	void Start ()
    {
        LevelController.getRabbit().GetComponent<RabbitController>().enabled = false;	
	}
}
