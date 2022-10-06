using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance = null;

    public float Horizontal { get; private set; }
    public bool IsJump { get; private set; }

    private void Awake()
    {
        if(instance != null && instance != this) 
        {
            Debug.LogWarning("There is an already existed Input Manager");
            Destroy(this);
        }
        else 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static InputManager GetInstance() 
    {
        if(instance == null)
        {
            throw new System.NullReferenceException("Input manager is null");
        }
        return instance;
    }

    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        IsJump = Input.GetKeyDown(KeyCode.W);
    }
}
