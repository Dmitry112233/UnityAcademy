using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : ObjectMove
{
    private Renderer renderer;

    private Renderer Renderer { get { return renderer = renderer ?? GetComponent<Renderer>(); } }
    private List<Color> colors = new List<Color>() { Color.white, Color.black, Color.red, Color.green, Color.blue, Color.gray };

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Renderer.material.SetColor("_Color", colors[Random.Range(0, colors.Count - 1)]);
        }
    }
}
