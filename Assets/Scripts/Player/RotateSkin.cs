using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RotateSkin : MonoBehaviour
{
	[SerializeField] private Camera cam;
	//[SerializeField] private Animator animator;

	//private float speed = 0;

	void Update()
	{
		if (!gameObject.transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
			return;

		Rotate();
		//animator.SetFloat("Speed", speed);
	}

	void Rotate()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition = cam.ScreenToWorldPoint(mousePosition);

		Vector2 direction = new Vector2(
			-(mousePosition.y - transform.position.y),
			mousePosition.x - transform.position.x - .1f
		);

		transform.up = direction;
	}

	/*void SetSpeed(float s) {
		speed = s;
	}

	void SetShooting(bool shoot) {
		animator.SetBool("IsRifleShoot", shoot);
	}*/
}
