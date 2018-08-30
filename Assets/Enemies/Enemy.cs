using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] public int Health = 10; // For testing.
	[SerializeField] public int HealthMax = 10; //For testing.

	public delegate void OnHealthChange(float healthChange);
	public event OnHealthChange HealthChange;

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
	}
}