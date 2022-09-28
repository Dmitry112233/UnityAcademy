using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public Vector3 initPosition;

    void Start()
    {
        initPosition = transform.position;
    }

    void Update()
    {
        transform.position = transform.position + new Vector3(0.05f, 0f, 0f);
    }
}
