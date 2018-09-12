using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] float Speed = 10;
	int Damage;
    GameObject ObjectFrom;
    Vector3 StartLocation;

	void Start ()
	{

	}

	void Update ()
	{
        if (Vector3.Distance(StartLocation, transform.position) > 100)
            Destroy(gameObject);
	}

	void OnTriggerEnter(Collider otherCollider)
	{
		//print(otherCollider.ToString());

        if (otherCollider.gameObject == ObjectFrom)
            return;

		Component damageable = otherCollider.gameObject.GetComponent(typeof(IDamagable));

		print(damageable);

		if (damageable)
		{
			(damageable as IDamagable).TakeDamage(Damage);
		}

        Destroy(gameObject);
	}

	public void Fire(Vector3 start, Vector3 target, int damage, GameObject from)
	{
        ObjectFrom = from;
		Damage = damage;
        StartLocation = start;
		transform.position = start;
        target += new Vector3(0, 2, 0);
		GetComponent<Rigidbody>().velocity = (target-transform.position).normalized * Speed;
	}
}
