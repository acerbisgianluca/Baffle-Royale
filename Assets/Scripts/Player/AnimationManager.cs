using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {
	[SerializeField] private Animator animator;

	void Start () {
		
	}
	
	void Update () {
		
	}

	void SetSpeed(float speed)
	{
		animator.SetFloat("Speed", speed);
	}

	void SetKnifeMelee(bool melee)
	{
		animator.SetBool("IsKnifeMelee", melee);
	}

	void SetRifleShoot(bool shoot)
	{
		animator.SetBool("IsRifleShoot", shoot);
	}

	void SetGun(int gun)
	{
		animator.SetInteger("Gun", gun);
	}
}
