using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] int EnemyLayer = 9;
	[SerializeField] int HealthMax = 20;
    [SerializeField] int DamagePerHit = 5;
    [SerializeField] float MeleeRange = 2;
    [SerializeField] float RangedAttack = 10;
    [SerializeField] float AttackCooldown = 0.5f;

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
	}

	void Update ()
	{

    }

    public void TakeDamage(int damage)
    {
        Health = (int)Mathf.Clamp((float)Health - damage, 0, HealthMax);
        HealthChange(Health);
    }

    void OnMouseClick(RaycastHit raycastHit, int layerHit)
    {
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