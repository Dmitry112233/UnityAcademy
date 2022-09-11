using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleMaterial : MonoBehaviour
{
    private Renderer render;
    private Color defaultValue;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        defaultValue = render.material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (render != null)
            {
                render.material.SetColor("_Color", Color.red);
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            if (render != null)
            {
                render.material.SetColor("_Color", defaultValue);
            }
        }
    }
}
