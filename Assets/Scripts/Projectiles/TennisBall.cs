using UnityEngine;

public class TennisBall : Projectile
{
    public float decreaseBounceValue = 0.0f;

    public AudioSource tennisAudio;

    private int currentCollisionNumber = 0;

    private int numberOfCollisions = 0;
    
    private Collider myCollider;

    void Start()
    {
        myCollider = gameObject.GetComponent<Collider>();
        StartCoroutine(WaitAndDestroy());
    }

    private void Update()
    {
    }

    private void RecalculateBallBounce()
    {
        if (currentCollisionNumber != numberOfCollisions && myCollider != null) 
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
        if(myCollider != null) 
        {
            if (myCollider.material.bounciness == 0.0f)
            {
                myCollider.material.bounciness = 1.0f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        numberOfCollisions++;

        if (hitEffect != null)
        {
            //AudioManager.Instance.PlayAudio(tennisAudio);
            var contactPosition = collision.GetContact(0).point;
            var direction = contactPosition - PlayerTransform.position;
            direction.Normalize();
            var effect = Instantiate(hitEffect, contactPosition, Quaternion.LookRotation(direction, Vector3.up));
            effect.transform.SetParent(collision.transform);
        }

        RecalculateBallBounce();
    }
}
