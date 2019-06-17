using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Knife : NetworkBehaviour
{

	[SerializeField] private int damage;
	[SerializeField] private float range;

	void Start()
	{

	}

	public void StartShooting(Vector3 mousePosition, Vector2 firePosition)
	{
		RaycastHit2D raycast = Physics2D.Raycast(firePosition, mousePosition, range);
		if (raycast.collider != null)
		{
			Collider2D collision = raycast.collider;
			GameObject hit = collision.gameObject;
			PlayerHealth health = hit.GetComponent<PlayerHealth>();
			if (health != null)
			{
				health.TakeDamage(this.damage);
			}
		}
	}
}
