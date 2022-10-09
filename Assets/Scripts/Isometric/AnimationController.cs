using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator anim;

    public string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE"};
    public string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };

    private int lastDirection;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
     
    void Update()
    {
        
    }

    public void SetDirection(Vector2 direction) 
    {
        string[] directionArray = null;

        if(direction.magnitude < 0.01) 
        {
            directionArray = staticDirections;
        }
        else 
        {
            directionArray = runDirections;

            lastDirection = DirectionToIndex(direction);
        }

        anim.Play(directionArray[lastDirection]);
    }

    private int DirectionToIndex(Vector2 direction) 
    {
        direction.Normalize();

        float step = 360 / 8;
        float offset = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, direction);

        angle += offset;
        if(angle < 0) 
        {
            angle += 360;
        }

        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }
}
