using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{

	[SerializeField] private float fireRateShotgun;
	[SerializeField] private float fireRateHandgun;
	[SerializeField] private float fireRateKnife;
	[SerializeField] private float fireRateRifle;
	[SerializeField] private Camera cam;
	[SerializeField] private GameObject camObject;
	[SerializeField] private List<Sprite> sprites;

	private SpriteRenderer spriteRenderer;

	//Gestiscono i numeri di colpi.
	[SerializeField] private int numberOfShootsHandgun;
	[SerializeField] private int numberOfShootsRifle;
	[SerializeField] private int numberOfShootsShotgun;
	private int currentShoots;

	//Gestiscono i tempi di ricarica.
	[SerializeField] private int timeReloadHandgun;
	[SerializeField] private int timeReloadRifle;
	[SerializeField] private int timeReloadShotgun;
	private int currentTimeReloading;
	private float currentTime;
	private bool isReloading;

	//Serve per scrivere il numero di colpi.
	[SerializeField] private Text writeShoots;

	//Gestiscono la visualizzazione del tempo di ricarica.
	[SerializeField] private GameObject loadingCircle;
	[SerializeField] private GameObject loadingText;
	private float currentAmount;

	private HandGun handGun;
	private Rifle rifle;
	private ShotGun shotGun;
	private Knife knife;

	// Gun switch
	private bool isTouchingGun = false;
	private string touchedGun;
	private GameObject touchedGunObject;
	private string currentGun = "Knife";

	[SerializeField] private GameObject bulletSpawner;
	private bool isShooting;
	private float timer;
	private float waitingTime;

	[SyncVar(hook = "OnSkinChange")]
	private int currentSprite;

	void Start()
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		if (!isLocalPlayer)
			return;

		camObject.SetActive(true);

		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		knife = GetComponent<Knife>();
		handGun = GetComponent<HandGun>();
		rifle = GetComponent<Rifle>();
		shotGun = GetComponent<ShotGun>();

		waitingTime = fireRateKnife;

		isReloading = false;
	}

	void Update()
	{
		if (!isLocalPlayer)
			return;

		//Gestisce il tempo di ricarica.
		currentTime -= Time.deltaTime;
		if (currentTime <= 0 && isReloading)
		{
			writeShoots.text = currentShoots + " / " + currentShoots;
			isReloading = false;

			loadingCircle.SetActive(false);
			loadingText.SetActive(false);
		}

		if (isReloading)
		{
			currentAmount += Time.deltaTime;
			if (currentAmount < currentTimeReloading)
			{
				loadingCircle.transform.GetComponent<Image>().fillAmount = currentAmount / currentTimeReloading;
			}
		}

		timer += Time.deltaTime;

		if ((isShooting || Input.GetButtonDown("Fire1")) && currentGun == "Rifle" && !isReloading)
		{
			if (timer > waitingTime)
			{
				if (CanShoot())
				{
					rifle.CmdStartShooting(GetFirePosition(), GetBulletRotation());
					currentShoots--;
					writeShoots.text = currentShoots + " / " + numberOfShootsRifle;

					if (currentShoots <= 0)
					{
						Reload();
						isShooting = false;
					}
				}
				timer = 0;
			}
			isShooting = true;
		}
		else if (Input.GetButtonDown("Fire1") && !isShooting && !isReloading)
		{
			switch (currentGun)
			{
				case "Knife":
					if (timer > waitingTime)
					{
						if (CanShoot())
							knife.StartShooting(GetMousePosition(), GetFirePosition());
						timer = 0;
					}
					break;
				case "Handgun":
					if (timer > waitingTime)
					{
						if (CanShoot())
						{
							handGun.CmdStartShooting(GetFirePosition(), GetBulletRotation());
							currentShoots--;
							writeShoots.text = currentShoots + " / " + numberOfShootsHandgun;
						}
						timer = 0;
					}
					break;
				case "Shotgun":
					if (timer > waitingTime)
					{
						if (CanShoot())
						{
							shotGun.CmdStartShooting(GetFirePosition(), GetBulletRotation());
							currentShoots--;
							writeShoots.text = currentShoots + " / " + numberOfShootsShotgun;
						}
						timer = 0;
					}
					break;
			}

			if (currentShoots <= 0)
			{
				Reload();
			}

			isShooting = true;
		}

		if (Input.GetButtonUp("Fire1") && isShooting)
		{
			isShooting = false;
		}

		if (Input.GetButtonDown("PickGun") && isTouchingGun)
		{
			SwitchGun();
		}

		if (Input.GetButtonDown("Reload") && !isReloading)
		{
			Reload();
		}
	}

	private void Reload()
	{
		switch (currentGun)
		{
			case "Handgun":
				currentShoots = numberOfShootsHandgun;
				currentTime = timeReloadHandgun;
				break;

			case "Rifle":
				currentShoots = numberOfShootsRifle;
				currentTime = timeReloadRifle;
				break;

			case "Shotgun":
				currentShoots = numberOfShootsShotgun;
				currentTime = timeReloadShotgun;
				break;
		}
		isReloading = true;

		currentTimeReloading = (int)currentTime;
		currentAmount = 0;
		loadingCircle.transform.GetComponent<Image>().fillAmount = 0;
		loadingCircle.SetActive(true);
		loadingText.SetActive(true);
	}

	private void SwitchGun()
	{
		if (currentGun != touchedGun)
		{
			ResetScripts();
			switch (touchedGun)
			{
				case "Handgun":
					spriteRenderer.sprite = sprites[1];
					CmdOnSkinChange(1);
					handGun.enabled = true;
					waitingTime = fireRateHandgun;
					break;
				case "Rifle":
					spriteRenderer.sprite = sprites[2];
					CmdOnSkinChange(2);
					rifle.enabled = true;
					waitingTime = fireRateRifle;
					break;
				case "Shotgun":
					spriteRenderer.sprite = sprites[3];
					CmdOnSkinChange(3);
					shotGun.enabled = true;
					waitingTime = fireRateShotgun;
					break;
			}
			currentGun = touchedGun;
			Reload();
			currentTime = 0;
		}
		else
		{
			// RICARICA
		}
	}

	[Command]
	public void CmdOnSkinChange(int skinNum)
	{
		spriteRenderer.sprite = sprites[skinNum];
		currentSprite = skinNum;
		NetworkServer.Destroy(touchedGunObject);
	}

	public void OnSkinChange(int num)
	{
		if (isLocalPlayer)
			return;

		currentSprite = num;
		spriteRenderer.sprite = sprites[currentSprite];
	}

	public override void OnNetworkDestroy()
	{
		if (isLocalPlayer)
		{
			NetworkManager.singleton.StopClient();
			Destroy(this.gameObject);
		}

	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.layer == 8)
		{
			isTouchingGun = true;
			touchedGun = collider.gameObject.tag;
			touchedGunObject = collider.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.layer == 8)
		{
			isTouchingGun = false;
			touchedGun = "";
			touchedGunObject = null;
		}
	}

	private void ResetScripts()
	{
		knife.enabled = false;
		handGun.enabled = false;
		rifle.enabled = false;
		shotGun.enabled = false;
	}

	private Vector3 GetMousePosition()
	{
		return cam.ScreenToWorldPoint(Input.mousePosition);
	}

	private Vector2 GetFirePosition()
	{
		return new Vector2(bulletSpawner.transform.position.x, bulletSpawner.transform.position.y);
	}

	private bool CanShoot()
	{
		Vector2 firePosition = GetFirePosition();
		if (Vector2.Distance(GetMousePosition(), firePosition) < Vector2.Distance(transform.position, firePosition))
			return false;

		return true;
	}

	private Quaternion GetBulletRotation()
	{
		Vector3 difference = GetMousePosition() - bulletSpawner.transform.position;
		difference.Normalize();
		float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		return Quaternion.Euler(0f, 0f, rotationZ);
	}
}
