using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private Rigidbody2D stoneBody;

    void Start()
    {
        stoneBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("HIT");
            
            //var playerTransform = collision.GetComponent<Transform>();

            var playerBody = collision.GetComponent<Rigidbody2D>();
            playerBody.AddForce(new Vector2(-1.0f, 1.0f) * 100, ForceMode2D.Impulse);
            
            /*var direction = Quaternion.AngleAxis(45.0f, playerTransform.forward) * playerTransform.right * -1;
            playerBody.AddForce(direction * 10, ForceMode2D.Impulse);*/
            //playerBody.AddForce(new Vector2(-5000.0f, 100.0f), ForceMode2D.Impulse);
        }
    }
}
