using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
	[SerializeField] private float speed = 5f;

	private float horizontalMove = 0f;
	private float verticalMove = 0f;

	void Update()
	{
		if (!isLocalPlayer)
			return;

		horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
		verticalMove = Input.GetAxisRaw("Vertical") * speed;
	}

	void FixedUpdate()
	{
		if (!isLocalPlayer)
			return;

		transform.Translate(horizontalMove * Time.deltaTime, 0, 0);
		transform.Translate(0, verticalMove * Time.deltaTime, 0);
	}
}
