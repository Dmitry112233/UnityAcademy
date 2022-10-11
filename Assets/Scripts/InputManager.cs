using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance = null;

    public delegate void HorizontalHandler(float horizontal);
    public delegate void JumpHandler(bool isJump);

    public event HorizontalHandler NotifyHorizontalUpdate;
    public event HorizontalHandler NotifyHorizontalFixedUpdate;
    public event JumpHandler NotifyJump;


    public float Horizontal { get; private set; }
    public bool IsJump { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("There is an already existed Input Manager");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static InputManager GetInstance()
    {
        if (instance == null)
        {
            throw new System.NullReferenceException("Input manager is null");
        }
        return instance;
    }

    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");

        if (Horizontal != 0f) 
        {
            NotifyHorizontalUpdate?.Invoke(Horizontal);
        }

        IsJump = Input.GetKeyDown(KeyCode.W);
        NotifyJump?.Invoke(IsJump);
    }

    private void FixedUpdate()
    {
        if (Horizontal != 0f)
        {
            NotifyHorizontalFixedUpdate?.Invoke(Horizontal);
        }
    }


}
