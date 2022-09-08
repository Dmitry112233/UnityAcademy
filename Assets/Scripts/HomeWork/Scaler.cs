using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField]
    private float scaleLimit = 30.0f;
    
    [SerializeField]
    private Vector3 scaleSpeed = new (0.01f, 0.01f, 0.01f);

    void Start()
    {
    }

    void Update()
    {
        Scale();
    }

    private void Scale() 
    {
        if(transform.localScale.x < scaleLimit) 
        {
            transform.localScale += scaleSpeed;
        }
    }
}
