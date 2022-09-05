using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 0.0f;

    private float rotationValue = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        rotationValue += Time.deltaTime * rotationSpeed;
        var rotation = Quaternion.Euler(0.0f, 0.0f, rotationValue);
        transform.rotation = rotation;
    }
}
