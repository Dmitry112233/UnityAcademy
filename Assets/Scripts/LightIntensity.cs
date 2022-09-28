using UnityEngine;

public class LightIntensity : MonoBehaviour
{
    private Light lamp;
    public Light Lamp { get { return lamp = lamp ?? GetComponent<Light>(); } }

    private float blinkTime = 0.5f;
    private float time = 0.0f;

    void Start()
    {   
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time >= blinkTime) 
        {
            time -= blinkTime;
            Lamp.intensity = Random.Range(1, 9);
        }
    }
}
