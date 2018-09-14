using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Follower : MonoBehaviour, IDamagable
{
    [SerializeField] int HealthMax = 20;
    [SerializeField] int HealAmount = 2;
    [SerializeField] float FollowRadius = 5;
    [SerializeField] float HealCooldown = 4;

    public delegate void OnHealthChange(float healthChange);
    public event OnHealthChange HealthChange;

    public float HealthAsPercentage
    {
        get
        {
            return Health / (float)HealthMax;
        }
    }

    Timer HealTimer;
    AICharacterControl AIControl;
    GameObject ThePlayer;
    Player ThePlayerScript;
    int Health;
    bool WithinRange;

    void Start()
    {
        Health = HealthMax;
        ThePlayer = GameObject.FindGameObjectWithTag("Player");
        ThePlayerScript = ThePlayer.GetComponent<Player>();
        AIControl = GetComponent<AICharacterControl>();
        HealTimer = new Timer(HealCooldown);
    }

    void Update ()
    {
        float distToPlayer = Vector3.Distance(ThePlayer.transform.position, transform.position);

        if (distToPlayer >= FollowRadius)
        {
            //print("Follower moves to within Healing Radius.");
            AIControl.SetTarget(ThePlayer.transform);
            WithinRange = false;
        }
        else
        {
            //print("Follower within Healing Radius.");
            AIControl.SetTarget(transform);
            WithinRange = true;
        }

        if (WithinRange)
        {
            if (HealTimer.Expired)
            {
                HealTimer.Reset();
                ThePlayerScript.TakeDamage(-HealAmount);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, FollowRadius);
    }

    public void TakeDamage(int damage)
    {
        Health = (int)Mathf.Clamp((float)Health - damage, 0, HealthMax);
        HealthChange(Health);

        if (Health < 1)
        {
            //Disable until end of battle.
        }
    }
}
