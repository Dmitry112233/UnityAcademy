using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 2.0f;
    public float sprintSpeed = 5.0f;
    public float rotationSpeed = 100f;
    public float jumpSpeed = 7.0f;

    public AnimationClip spawnAnimationClip;
    public AnimatorOverrideController[] overrideControllers;

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
    private bool isSpawnTime = false; 
    private bool isAlive = false; 
    private bool isAttack= false; 


    void Start()
    {
        StartCoroutine(PlaySpawn());
        isAlive = true;
        animator.fireEvents = false;
    }

    public void SetAnimations(AnimatorOverrideController overrideController) 
    {
        Animator.runtimeAnimatorController = overrideController;
    }

    private IEnumerator PlaySpawn() 
    {
        Animator.SetTrigger("Spawn");
        yield return new WaitForSeconds(spawnAnimationClip.length);
        isSpawnTime = true;
    }

    private IEnumerator PlayAttack()
    {
        Animator.SetTrigger("Attack");
        isAttack = true;
        var clip = new List<AnimationClip>(Animator.runtimeAnimatorController.animationClips).Find(x => x.name.Contains("Combo"));
        yield return new WaitForSeconds(clip.length);
        SetAnimations(overrideControllers[Random.Range(0, 4)]);
        isAttack = false;
    }

    private IEnumerator WaitUntilJumpIsFinishedAndDie()
    {
        yield return new WaitForSeconds(1.5f);
        Animator.SetTrigger("Die");
        isAlive = false;

    }

    void Update()
    {
        if(isSpawnTime && isAlive && !isAttack) 
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");


            if (Input.GetMouseButtonDown(0) && !isJumping)
            {
                StartCoroutine(PlayAttack());
            }

            if (Input.GetKeyDown(KeyCode.E))
            { 
                if (isJumping) 
                {
                    StartCoroutine(WaitUntilJumpIsFinishedAndDie());
                }
                else{
                    Animator.SetTrigger("Die");
                    isAlive = false;
                }
            }

            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                isJumping = true;
                Animator.SetTrigger("Jump");
                speedY += jumpSpeed;
            }
            if (!Controller.isGrounded)
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