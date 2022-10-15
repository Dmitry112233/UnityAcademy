using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Timer
    {
        public Action onTime;

        public float executionTime;
        public float delay;

        public Timer(float delay, Action action) 
        {
            executionTime = Time.time + delay;
            onTime = action;
            this.delay = delay;
        }
    }
}
