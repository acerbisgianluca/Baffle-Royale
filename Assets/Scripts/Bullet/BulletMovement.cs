using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletMovement : NetworkBehaviour
{

	[SerializeField] private float speed;

	void FixedUpdate()
	{
		transform.Translate(Vector3.right * Time.deltaTime * speed);
	}
}
