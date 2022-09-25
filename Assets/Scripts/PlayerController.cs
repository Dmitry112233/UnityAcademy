using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 2.0f;
    public float sprintSpeed = 5.0f;
    public float rotationSpeed = 100f;
    public float jumpSpeed = 7.0f;

    private float animationBlendSpeed = 0.2f;
    private float targetAnimationSpeed = 0.0f;

    private CharacterController controller;
    private Camera characterCamera;

    private Animator animator;

    public CharacterController Controller { get { return controller = controller ?? GetComponent<CharacterController>(); } }
    public Camera CharacterCamera { get { return characterCamera = characterCamera ?? FindObjectOfType<Camera>(); } }
    public Animator Animator { get { return animator = animator ?? GetComponent<Animator>(); } }


    private bool isSprint = false;

    private float speedY = 0.0f;
    private float gravity = -9.81f;
    private bool isJumping = false;

    private bool isScriptAvailible = false;
    private bool isReadyToDie = false;


    void Start()
    {
        Animator.SetTrigger("Spawn");
    }

    public void SpawnFinish() 
    {
        isScriptAvailible = true;
    }

    void Update()
    {
        if (isScriptAvailible) 
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            //Attack
            if (Input.GetMouseButtonDown(0))
            {
                Animator.SetInteger("RandomAttack", Random.Range(1, 5));
                Animator.SetTrigger("Attack");
            }

            //Die
            if (Input.GetKeyDown(KeyCode.E))
            {
                isReadyToDie = true;
            }
            if (isReadyToDie && speedY == 0.0f)
            {
                Animator.SetTrigger("Die");
                isScriptAvailible = false;
            }

            //Jump
            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                isJumping = true;
                Animator.SetTrigger("Jump");
                speedY += jumpSpeed;
            }
            if (!Controller.isGrounded && isJumping)
            {
                speedY += gravity * Time.deltaTime;
            }
            else if (speedY < 0.0f)
            {
                speedY = 0.0f;
            }

            Animator.SetFloat("SpeedY", speedY / jumpSpeed);
            if (isJumping && speedY < 0.0f)
            {
                if (Physics.Raycast(transform.position, Vector3.down, 1.0f, LayerMask.GetMask("Default")))
                {
                    isJumping = false;
                    Animator.SetTrigger("Land");
                }
            }


            //Move
            isSprint = Input.GetKey(KeyCode.LeftShift);
            Vector3 movement = new Vector3(horizontal, 0.0f, vertical);

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
                targetAnimationSpeed = isSprint ? 1.0f : 0.5f;
            }
            else
            {
                targetAnimationSpeed = 0.0f;
            }

            Animator.SetFloat("Speed", Mathf.Lerp(Animator.GetFloat("Speed"), targetAnimationSpeed, animationBlendSpeed));
        }
    }
}