using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamagable
{
	[SerializeField] int Health = 10;
	[SerializeField] int HealthMax = 10;
    [SerializeField] int DamagePerAttack = 2;
	[SerializeField] float AttackRadius = 2;
    [SerializeField] float RangeAttackRadius = 16;
    [SerializeField] float DetectRadius = 5;
    [SerializeField] float ChaseRadius = 20;
    [SerializeField] bool HasRangedAttack;
    [SerializeField] GameObject Projectile;

	public delegate void OnHealthChange(float healthChange);
	public event OnHealthChange HealthChange;

    Timer ShotTimer;
	ThirdPersonCharacter TPCaracter;
	AICharacterControl AIControl;
	GameObject ThePlayer;
    Transform SpawnPoint;
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
		ThePlayer = GameObject.FindGameObjectWithTag("Player");
		TPCaracter = GetComponent<ThirdPersonCharacter>();
		AIControl = GetComponent<AICharacterControl>();
        SpawnPoint = transform.Find("ProjectileSpawnPoint");

        ShotTimer = new Timer(1);
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
                    shot.GetComponent<Projectile>().Fire(SpawnPoint.position, ThePlayer.transform.position, DamagePerAttack);
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

                if (distToPlayer <= DetectRadius && distToPlayer >= AttackRadius)
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

        if (distToPlayer <= AttackRadius)
        {
            print("Enemy melee attacks Player!");
            AIControl.SetTarget(transform);
        }
    }

    public void TakeDamage(int damage)
    {
        Health = (int)Mathf.Clamp((float)Health - damage, 0, HealthMax);
        HealthChange(Health);
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.grey;
        //Gizmos.DrawLine(transform.position, CurrentClickTarget);
        //Gizmos.DrawSphere(CurrentClickTarget, 0.1f);
        //Gizmos.DrawSphere(ClickPoint, 0.15f);

        //Draw Melee attack sphere.
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, DetectRadius);

        //Draw Melee attack sphere.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ChaseRadius);

        //Draw Melee attack sphere.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);

        //Draw Range attack sphere.
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, RangeAttackRadius);
    }
}