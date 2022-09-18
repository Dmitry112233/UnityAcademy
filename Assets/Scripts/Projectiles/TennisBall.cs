using UnityEngine;

public class TennisBall : Projectile
{
    public float decreaseBounceValue = 0.09f;

    private int currentCollisionNumber = 0;
    private int numberOfCollisions = 0;
    Collider myCollider;

    void Start()
    {
        myCollider = gameObject.GetComponent<Collider>();
        StartCoroutine(WaitAndDestroy());
    }

    private void Update()
    {
        RecalculateBallBounce();
    }

    private void RecalculateBallBounce()
    {
        if(currentCollisionNumber != numberOfCollisions) 
        {
            if (myCollider.material.bounciness >= decreaseBounceValue) 
            {
                myCollider.material.bounciness -= decreaseBounceValue;
            }
            else 
            {
                myCollider.material.bounciness = 0.0f;
            }
            currentCollisionNumber = numberOfCollisions;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        numberOfCollisions++;
    }
}
