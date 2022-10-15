using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Timer
    {
        public event Action<Timer> OnTime;

        private float targetTime;
        private float currentTime;

        public int Index { get; private set; }

        public Timer(float time) 
        {
            targetTime = time;
            currentTime = 0.0f;
        }

        public void Update() 
        {
            currentTime += Time.deltaTime;
            if(currentTime >= targetTime) 
            {
                OnTime(this);
            }
        }
    }
}
