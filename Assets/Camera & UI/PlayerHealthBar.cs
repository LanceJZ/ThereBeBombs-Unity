using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class PlayerHealthBar : MonoBehaviour
{
    RawImage HealthBarRawImage;
    Player ThePlayer;

    // Use this for initialization
    void Start()
    {
        ThePlayer = FindObjectOfType<Player>();
        HealthBarRawImage = GetComponent<RawImage>();
        ThePlayer.HealthChange += OnHealthChange;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnHealthChange(float currentHealth)
    {
        float xValue = -(ThePlayer.HealthAsPercentage / 2.0f) - 0.5f;
        HealthBarRawImage.uvRect = new Rect(xValue, 0.0f, 0.5f, 1.0f);
    }
}
