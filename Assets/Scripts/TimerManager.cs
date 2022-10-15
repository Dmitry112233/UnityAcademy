using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private static TimerManager instance = null;

    private List<Timer> timers = new List<Timer>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static TimerManager GetInstance()
    {
        if (instance == null)
        {
            throw new System.NullReferenceException("Timer manager is null");
        }
        return instance;
    }

    public void AddTimer(float delay, Action action)
    {
        timers.Add(new Timer(delay, action));
    }

    void Update()
    {
        for(int i = 0; i < timers.Count; i++) 
        {
            if(timers[i].executionTime < Time.time) 
            {
                timers[i].onTime?.Invoke();
                timers.RemoveAt(i);
            }
        }
    }
}
