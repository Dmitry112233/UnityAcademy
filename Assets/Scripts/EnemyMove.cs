using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float Speed;
    public float pingPongLenth;

    private float intitialX;

    private void Start()
    {
        intitialX = transform.localPosition.x;
    }

    private void Update()
    {
        var lenght = Mathf.PingPong(Time.time * Speed, pingPongLenth);
        var position = transform.localPosition;
        position.x = intitialX + lenght;
        transform.localPosition = position;
    }
}
