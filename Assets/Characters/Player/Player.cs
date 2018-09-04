using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
	[SerializeField] public int Health = 20; // For testing.
	[SerializeField] public int HealthMax = 20; //For testing.

	public delegate void OnHealthChange(float healthChange);
	public event OnHealthChange HealthChange;

	public float HealthAsPercentage
	{
		get
		{
			return Health / (float)HealthMax;
		}
	}

	void Start ()
	{

	}

	void Update ()
	{

    }

    public void TakeDamage(int damage)
    {
        Health = (int)Mathf.Clamp((float)Health - damage, 0, HealthMax);
        HealthChange(Health);
    }
}