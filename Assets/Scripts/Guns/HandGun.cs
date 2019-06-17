using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HandGun : NetworkBehaviour
{

	[SerializeField] private int damage;
	[SerializeField] private GameObject bulletPrefab;

	void Start()
	{

	}

	[Command]
	public void CmdStartShooting(Vector2 firePosition, Quaternion rotation)
	{
		GameObject objectInstance = Instantiate(bulletPrefab, firePosition, rotation) as GameObject;
		objectInstance.GetComponent<BulletController>().SetDamage(this.damage);
		objectInstance.SetActive(true);
		NetworkServer.Spawn(objectInstance);
		//objectInstance.SendMessage("StartMoving", mousePosition);
		Destroy(objectInstance, 5);
	}
}
