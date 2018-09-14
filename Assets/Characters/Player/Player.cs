using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour, IDamagable
{
	[SerializeField] int EnemyLayer = 9;
	[SerializeField] int HealthMax = 20;
	[SerializeField] int DamagePerHit = 5;
	[SerializeField] float MeleeRange = 2;
	[SerializeField] float RangedAttack = 10;
	[SerializeField] float AttackCooldown = 0.5f;
	[SerializeField] Weapon EquippedWeapon;
	//[SerializeField] GameObject WeaponSocket;

	public delegate void OnHealthChange(float healthChange);
	public event OnHealthChange HealthChange;

	Timer CoolDown;
	GameObject CurrentTarget;
	CameraRaycaster RayCaster;
	int Health;

	public float HealthAsPercentage
	{
		get
		{
			return Health / (float)HealthMax;
		}
	}

	void Start ()
	{
		RayCaster = FindObjectOfType<CameraRaycaster>();
		RayCaster.NotifyMouseClickObservers += OnMouseClick;
		CoolDown = new Timer(AttackCooldown);
		Health = HealthMax;

		EqipWeapon();
	}

	void Update ()
	{

	}

	public void TakeDamage(int damage)
	{
		Health = (int)Mathf.Clamp((float)Health - damage, 0, HealthMax);
		HealthChange(Health);
	}

	void EqipWeapon()
	{
		var weaponPrefab = EquippedWeapon.GetWeaponPrefab();
		GameObject weaponSocket = RequestWeaponHand();
		var weapon = Instantiate(weaponPrefab, weaponSocket.transform);
		weapon.transform.localPosition = EquippedWeapon.GripTransform.localPosition;
		weapon.transform.localRotation = EquippedWeapon.GripTransform.localRotation;
	}

	GameObject RequestWeaponHand()
	{
		var weaponHand = GetComponentsInChildren<WeaponHand>();
		int numberOfWeaponHands = weaponHand.Length;
		Assert.IsFalse(numberOfWeaponHands < 1, "No weapon hand. Add one to player.");
		Assert.IsFalse(numberOfWeaponHands > 1, "More than one weapon hand found. Only one is supported at this time.");
		return weaponHand[0].gameObject;
	}

	void OnMouseClick(RaycastHit raycastHit, int layerHit)
	{
		if (Health < 1)
			return;

		if (layerHit == EnemyLayer && CoolDown.Expired)
		{
			GameObject enemy = raycastHit.collider.gameObject;
			float distToTarget = Vector3.Distance(enemy.transform.position, transform.position);

			if (distToTarget < MeleeRange)
			{
				CurrentTarget = enemy;
				enemy.GetComponent<Enemy>().TakeDamage(DamagePerHit);
				CoolDown.Reset();
			}
		}
	}
}