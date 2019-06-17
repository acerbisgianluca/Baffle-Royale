using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShotGun : NetworkBehaviour
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
		//objectInstance.SendMessage("StartMoving", mousePosition);
		NetworkServer.Spawn(objectInstance);
		Destroy(objectInstance, 5);
	}
}
