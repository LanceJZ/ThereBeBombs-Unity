using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float Speed = 10;
    int Damage;

	void Start ()
	{

	}

	void Update ()
	{

	}

    void OnTriggerEnter(Collider otherCollider)
    {
        print(otherCollider.ToString());

        Component damageable = otherCollider.gameObject.GetComponent(typeof(IDamagable));

        print(damageable);

        if (damageable)
        {
            (damageable as IDamagable).TakeDamage(Damage);
        }
    }

    public void Fire(Vector3 start, Vector3 target, int damage)
    {
        Damage = damage;
        transform.position = start;
        GetComponent<Rigidbody>().velocity = (target-transform.position).normalized * Speed;
    }
}
