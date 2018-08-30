﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class EnemyHealthBar : MonoBehaviour
{
    RawImage HealthBarRawImage;
    Enemy TheEnemy;

    // Use this for initialization
    void Start()
    {
        TheEnemy = GetComponentInParent<Enemy>();
        HealthBarRawImage = GetComponent<RawImage>();
        TheEnemy.HealthChange += OnHealthChange;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnHealthChange(float currentHealth)
    {
        float xValue = -(TheEnemy.HealthAsPercentage / 2.0f) - 0.5f;
        HealthBarRawImage.uvRect = new Rect(xValue, 0.0f, 0.5f, 1.0f);
    }
}