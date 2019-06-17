using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GunSpawnManager : NetworkBehaviour
{
	private float timer = 0f;
	private float waitTime;
	[SerializeField] private int minTime;
	[SerializeField] private int maxTime;

	[SerializeField] private List<GameObject> guns = new List<GameObject>();
	[SerializeField] private List<GameObject> spawners = new List<GameObject>();
	private GameObject[] spawnedGuns;

	[SerializeField] private NetworkManager networkManager;

	public override void OnStartServer()
	{
		waitTime = Random.Range(minTime, maxTime);
		spawnedGuns = new GameObject[spawners.Count];
		int i = 0;
		foreach (GameObject spawn in spawners)
		{
			GameObject gun = Instantiate(guns[Random.Range(0, guns.Count)], spawn.transform.position, transform.rotation);
			spawnedGuns[i] = gun;
			NetworkServer.Spawn(gun);
			i++;
		}
	}

	void Update()
	{
		timer += Time.deltaTime;
		if (timer > waitTime)
		{
			for (int i = 0; i < spawnedGuns.Length; i++)
			{
				if (spawnedGuns[i] == null)
				{
					spawnedGuns[i] = Instantiate(guns[Random.Range(0, guns.Count)], spawners.ElementAt(i).transform.position, transform.rotation);
					NetworkServer.Spawn(spawnedGuns[i]);
				}
			}
			waitTime = Random.Range(5, 10);
			timer = 0;
		}
		if (!NetworkServer.active)
		{
			foreach (GameObject gun in spawnedGuns)
				NetworkServer.Destroy(gun);
			this.gameObject.SetActive(false);
		}
	}
}
