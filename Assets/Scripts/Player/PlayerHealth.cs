using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour
{

	public const int maxHealth = 100;

	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;
	public RectTransform healthBar;

	public void TakeDamage(int amount)
	{
		if (!isServer)
		{
			return;
		}

		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			this.gameObject.SetActive(false);
			Destroy(this.gameObject);
		}
	}

	void OnChangeHealth(int health)
	{
		healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
	}
}
/* healthBar.transform.Translate(-(amount / maxHealth), 0, 0); */
