using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamagable
{
	[SerializeField] int HealthMax = 10;
	[SerializeField] int DamagePerMeleeAttack = 2;
	[SerializeField] int DamagePerRangeAttack = 4;
	[SerializeField] float MeleeAttackRadius = 2;
	[SerializeField] float RangeAttackRadius = 16;
	[SerializeField] float DetectRadius = 5;
	[SerializeField] float ChaseRadius = 20;
	[SerializeField] bool HasRangedAttack;
	[SerializeField] float FireCooldown = 1;
	[SerializeField] GameObject Projectile;
	[SerializeField] Vector3 AimOffset = new Vector3(0, 1, 0);

	public delegate void OnHealthChange(float healthChange);
	public event OnHealthChange HealthChange;

	Timer ShotTimer;
	//ThirdPersonCharacter TPCaracter;
	AICharacterControl AIControl;
	GameObject ThePlayer;
	Player ThePlayerScript;
	Transform SpawnPoint;
	int Health;
	bool PlayerSighted;

	public float HealthAsPercentage
	{
		get
		{
			return Health / (float)HealthMax;
		}
	}

	void Start ()
	{
		Health = HealthMax;
		ThePlayer = GameObject.FindGameObjectWithTag("Player");
		ThePlayerScript = ThePlayer.GetComponent<Player>();
		//TPCaracter = GetComponent<ThirdPersonCharacter>();
		AIControl = GetComponent<AICharacterControl>();
		SpawnPoint = transform.Find("ProjectileSpawnPoint");

		ShotTimer = new Timer(FireCooldown);
	}

	void Update ()
	{

		float distToPlayer = Vector3.Distance(ThePlayer.transform.position, transform.position);

		if (PlayerSighted)
		{
			if (HasRangedAttack && ShotTimer.Expired)
			{
				if (distToPlayer >= RangeAttackRadius)
				{
					print("Enemy moves to within Ranged Attack Radius.");
					AIControl.SetTarget(ThePlayer.transform);
				}
				else
				{
					print("Enemy shots projectile!");
					GameObject shot = Instantiate(Projectile);
					shot.GetComponent<Projectile>().Fire(SpawnPoint.position,
						ThePlayer.transform.position + AimOffset, DamagePerRangeAttack, gameObject);
					AIControl.SetTarget(transform);
					ShotTimer.Reset();
				}
			}
			else
			{
				if (distToPlayer <= ChaseRadius)
				{
					AIControl.SetTarget(ThePlayer.transform);
				}

				if (distToPlayer <= DetectRadius && distToPlayer >= MeleeAttackRadius)
				{
					print("Enemy spots player!");
					AIControl.SetTarget(ThePlayer.transform);
					PlayerSighted = true;
				}
			}

			if (distToPlayer >= ChaseRadius)
			{
				print("Enemy lost sight of player.");
				AIControl.SetTarget(transform);
				PlayerSighted = false;
			}
		}
		else
		{
			if (distToPlayer <= DetectRadius)
			{
				print("Enemy spots player!");
				PlayerSighted = true;
			}
		}

		if (distToPlayer <= MeleeAttackRadius)
		{
			print("Enemy melee attacks Player!");
			AIControl.SetTarget(transform);
			ThePlayerScript.TakeDamage(DamagePerMeleeAttack);
		}
	}

	public void TakeDamage(int damage)
	{
		Health = (int)Mathf.Clamp((float)Health - damage, 0, HealthMax);
		HealthChange(Health);

		if (Health < 1)
		{
			Destroy(gameObject);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, DetectRadius);

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, ChaseRadius);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, MeleeAttackRadius);

		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, RangeAttackRadius);
	}
}