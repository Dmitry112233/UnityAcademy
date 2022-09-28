using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private SpriteRenderer spriteRender;

    private SpriteRenderer SpriteRender { get { return spriteRender = spriteRender ?? GetComponent<SpriteRenderer>(); } }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SpriteRender.flipX = !SpriteRender.flipX;
        }
    }
}
