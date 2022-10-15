using Assets.Scripts;
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

    public Timer GetTimer(float time)
    {
        var timer = new Timer(time);
        timers.Add(timer);
        return timer;
    }

    public void RemoveTimer(Timer timer)
    {
        timers.Remove(timer);
    } 


    void Update()
    {
        foreach(var timer in timers) 
        {
            timer.Update();
            print("Here" + Time.deltaTime);
        }
    }
}
