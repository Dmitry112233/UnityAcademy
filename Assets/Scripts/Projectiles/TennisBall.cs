using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : Projectile
{
    void Start()
    {
        StartCoroutine(WaitAndDestroy());
    }
}
