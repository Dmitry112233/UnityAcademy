using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 2.0f;
    public float sprintSpeed = 5.0f;
    public float rotationSpeed = 100f;
    public float jumpSpeed = 7.0f;

    private float animationBlendSpeed = 0.2f;
    private float movementAnimationSpeed = 0.0f;
    private float speedY = 0.0f;
    private float gravity = -9.81f;
    private bool isJumping = false;
    private bool isReadyToDie = false;
    private bool isScriptAvailible = false;

    private CharacterController controller;
    private Camera characterCamera;
    private Animator animator;
    private InputManager inputManager;

    public CharacterController Controller { get { return controller = controller ?? GetComponent<CharacterController>(); } }
    public Camera CharacterCamera { get { return characterCamera = characterCamera ?? FindObjectOfType<Camera>(); } }
    public Animator Animator { get { return animator = animator ?? GetComponent<Animator>(); } }
    public InputManager InputManager { get { return inputManager = inputManager ?? GetComponent<InputManager>(); } }

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        if (isScriptAvailible) 
        {
            if (InputManager.AttackInput)
            {
                Attack();
            }
            
            if (InputManager.DieInput)
            {
                SetReadyToDie();
            }

            if (InputManager.JumpInput && !isJumping)
            {
                Jump();
            }

            RecalculateSpeedY();
            CheckLandingCondition();
            Move();
            CheckDieCondition();
        }
    }

    private void Spawn() 
    {
        Animator.SetTrigger(MyTags.Animations.Spawn);
    }

    public void SpawnFinish()
    {
        isScriptAvailible = true;
    }

    private void Attack() 
    {
        Animator.SetInteger(MyTags.Animations.RandomAttack, Random.Range(1, 5));
        Animator.SetTrigger(MyTags.Animations.Attack);
    }

    private void SetReadyToDie() 
    {
        isReadyToDie = true;
    }

    private void CheckDieCondition()
    {
        if (isReadyToDie && speedY == 0.0f)
        {
            Animator.SetTrigger(MyTags.Animations.Die);
            isScriptAvailible = false;
        }
    }

    private void Jump() 
    {
        isJumping = true;
        Animator.SetTrigger(MyTags.Animations.Jump);
        speedY += jumpSpeed;
    }

    private void RecalculateSpeedY() 
    {
        if (!Controller.isGrounded && isJumping)
        {
            speedY += gravity * Time.deltaTime;
        }
        else if (speedY < 0.0f)
        {
            speedY = 0.0f;
        }
        Animator.SetFloat(MyTags.Animations.SpeedY, speedY / jumpSpeed);
    }

    private void CheckLandingCondition() 
    {
        if (isJumping && speedY < 0.0f)
        {
            if (Physics.Raycast(transform.position, Vector3.down, 1.0f, LayerMask.GetMask(MyTags.Layers.Default)))
            {
                isJumping = false;
                Animator.SetTrigger(MyTags.Animations.Land);
            }
        }
    }

    private void Move() 
    {
        var isSprint = InputManager.SprintInput;
        Vector3 movement = InputManager.MovementInput;

        Vector3 velocity = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement.normalized;
        var rotatedMovement = velocity;
        velocity.y = speedY;

        float currentSpeed = isSprint ? sprintSpeed : movementSpeed;

        Controller.Move(velocity * currentSpeed * Time.deltaTime);

        rotatedMovement.Normalize();

        if (rotatedMovement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(rotatedMovement, Vector3.up);
            Controller.transform.rotation = Quaternion.RotateTowards(Controller.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            movementAnimationSpeed = isSprint ? 1.0f : 0.5f;
        }
        else
        {
            movementAnimationSpeed = 0.0f;
        }

        Animator.SetFloat(MyTags.Animations.Speed, Mathf.Lerp(Animator.GetFloat(MyTags.Animations.Speed), movementAnimationSpeed, animationBlendSpeed));
    }
}