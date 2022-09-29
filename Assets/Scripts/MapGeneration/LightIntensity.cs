using UnityEngine;

public class LightIntensity : MonoBehaviour
{
    public float intensity;

    private Light lamp;
    public Light Lamp { get { return lamp = lamp ?? GetComponent<Light>(); } }

    private float blinkTime = 0.5f;
    private float time = 0.0f;
    private bool isSwitchedOn = false;

    void Update()
    {
        time += Time.deltaTime;
        if(time >= blinkTime) 
        {
            time -= blinkTime;
            if (!isSwitchedOn) 
            {
                Lamp.intensity = intensity;
            }
            else 
            {
                Lamp.intensity = 0;
            }
            isSwitchedOn = !isSwitchedOn;
        }
    }
}
