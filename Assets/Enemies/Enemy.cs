using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{
	[SerializeField] public int Health = 10; // For testing.
	[SerializeField] public int HealthMax = 10; //For testing.
	[SerializeField] public float AttackRadius = 8;

	public delegate void OnHealthChange(float healthChange);
	public event OnHealthChange HealthChange;
	ThirdPersonCharacter TPCaracter;
	AICharacterControl AIControl;
	GameObject ThePlayer;

	public float HealthAsPercentage
	{
		get
		{
			float healthPercent = CurrentHealth / (float)MaxHealth;
			return healthPercent;
		}
	}

	int LastHealth;
	int CurrentHealth = 10;
	int MaxHealth = 10;

	void Start ()
	{
		ThePlayer = GameObject.FindGameObjectWithTag("Player");
		TPCaracter = GetComponent<ThirdPersonCharacter>();
		AIControl = GetComponent<AICharacterControl>();
	}

	void Update ()
	{
		CurrentHealth = Health; //For testing.
		MaxHealth = HealthMax; //For testing.

		if (LastHealth != CurrentHealth)
		{
			HealthChange(CurrentHealth);
			LastHealth = CurrentHealth;
		}

		float distToPlayer = Vector3.Distance(ThePlayer.transform.position, transform.position);

		if (distToPlayer <= AttackRadius)
		{
			AIControl.SetTarget(ThePlayer.transform);
		}
		else
		{
			AIControl.SetTarget(transform);
		}
	}
}