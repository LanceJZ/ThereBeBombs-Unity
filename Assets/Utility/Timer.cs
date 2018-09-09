using System;
using UnityEngine;

public class Timer
{
    float timerAmount;
    float timerTime;
    bool paused;

    public Timer(float length)
    {
        timerAmount = length;
    }

    public Timer()
    {

    }

    public float Amount
    {
        get { return timerAmount; }

        set
        {
            Reset(value);
        }
    }

    public bool Expired
    {
        get
        {
            return Time.time > timerTime;
        }
    }

    public bool Paused { get { return paused; } }

    public void SetPause(bool pause)
    {
        paused = pause;

        if (paused)
        {

        }
        else
        {

        }
    }

    public void Reset(float length)
    {
        timerAmount = length;
        timerTime = timerAmount + Time.time;
    }

    public void Reset()
    {
        timerTime = timerAmount + Time.time;
    }
}