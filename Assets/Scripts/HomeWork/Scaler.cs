using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField]
    private float scaleLimit = 30.0f;
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float startScale = 5.0f;

    private float scaleValue = 0.0f;
    
    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    private void Move() 
    {
        scaleValue += Time.deltaTime * speed;
        if(scaleValue > startScale && scaleValue < scaleLimit)
        {
            transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
        }
    }
}
