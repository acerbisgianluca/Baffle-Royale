using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletController : NetworkBehaviour
{

	private int damage;

	public void SetDamage(int damage)
	{
		this.damage = damage;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject hit = collision.gameObject;
		PlayerHealth health = hit.GetComponent<PlayerHealth>();
		if (health != null)
		{
			health.TakeDamage(this.damage);
			NetworkServer.Destroy(this.gameObject);
		}
		if (collision.gameObject.tag == "solid")
		{
			NetworkServer.Destroy(this.gameObject);
		}
	}
}
