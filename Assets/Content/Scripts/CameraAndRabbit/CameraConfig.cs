using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfig : MonoBehaviour
{
    public RabbitController objToFollow;

	// Use this for initialization
	void Start()
    {
		
	}

	// Update is called once per frame
	void Update()
    {
        Transform rabit_transform = objToFollow.transform;
        Transform camera_transform = this.transform;

        Vector3 rabit_position = rabit_transform.position;
        Vector3 camera_position = camera_transform.position;

        camera_position.x = rabit_position.x;
        camera_position.y = rabit_position.y;

        camera_transform.position = camera_position;
    }
}
