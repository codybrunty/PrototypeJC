using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour{

    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 appliedMovement;

    bool isMovementPressed = false;
    bool isRunPressed = false;
    bool isJumpedPressed = false;

    public float moveSpeed = 1f;
    public float runSpeed = 1f;
    public float rotationSpeed = 1f;
    private float gravity = -9.8f;
    private float groundedGravity = -0.5f;

    private bool isJumping = false;
    private float initalJumpVelocity;
    public float maxJumpHeight = 1f;
    public float maxJumpTime = 0.5f;
    public float fallMultiplier = 2f;
    private bool isJumpAnimating = false;



    private void Awake() {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");
        isRunningHash = Animator.StringToHash("IsRunning");
        isJumpingHash = Animator.StringToHash("IsJumping");
        characterController = GetComponent<CharacterController>();
        playerInput = new PlayerInput();
        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;
        playerInput.CharacterControls.Jump.started += OnJump;
        playerInput.CharacterControls.Jump.canceled += OnJump;
        SetupJumpVariables();
    }

    private void SetupJumpVariables() {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initalJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    private void OnMovementInput(InputAction.CallbackContext context) {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }
    
    private void OnJump(InputAction.CallbackContext context) {
        isJumpedPressed = context.ReadValueAsButton();
    }

    private void OnRun(InputAction.CallbackContext context) {
        isRunPressed = context.ReadValueAsButton();
    }

    private void Update() {
        HandleRotation();
        HandleAnimation();
        HandleMovement();
        HandleGravity();
        HandleJump();
    }

    private void HandleGravity() {
        bool isFalling = currentMovement.y <= 0f || !isJumpedPressed;

        if (characterController.isGrounded) {
            if (isJumpAnimating) {
                animator.SetBool(isJumpingHash, false);
                isJumpAnimating = false;
            }
            currentMovement.y = groundedGravity;
            appliedMovement.y = groundedGravity;
        }
        else if (isFalling) {
            float prevVelo = currentMovement.y;
            currentMovement.y = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            appliedMovement.y = Mathf.Max((prevVelo + currentMovement.y) * .5f,-20f);

        }
        else {
            float prevVelo = currentMovement.y;
            currentMovement.y = currentMovement.y + (gravity * Time.deltaTime);
            appliedMovement.y = (prevVelo + currentMovement.y) * .5f;

        }
    }

    private void HandleMovement() {
        appliedMovement.x = currentMovement.x;
        appliedMovement.z = currentMovement.z;

        if (isRunPressed) {
            Vector3 m = appliedMovement;
            m.x = m.x * runSpeed;
            m.z = m.z * runSpeed;
            characterController.Move(m * Time.deltaTime);
        }
        else {
            Vector3 m = appliedMovement;
            m.x = m.x * moveSpeed;
            m.z = m.z * moveSpeed;
            characterController.Move(m * Time.deltaTime);
        }
    }

    private void HandleJump() {
        if(!isJumping && characterController.isGrounded && isJumpedPressed) {
            animator.SetBool(isJumpingHash, true);
            isJumpAnimating = true;
            isJumping = true;
            currentMovement.y = initalJumpVelocity;
            appliedMovement.y = initalJumpVelocity;
        }
        else if(!isJumpedPressed && isJumping && characterController.isGrounded) {
            isJumping = false;
        }
    }

    private void HandleRotation() {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;
        if (isMovementPressed) {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation,targetRotation,rotationSpeed*Time.deltaTime);
        }

    }

    private void HandleAnimation() {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if(isMovementPressed && !isWalking) {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!isMovementPressed && isWalking) {
            animator.SetBool(isWalkingHash, false);
        }

        if ((isMovementPressed && isRunPressed) && !isRunning) {
            animator.SetBool(isRunningHash, true);
        }
        else if ((!isMovementPressed||!isRunPressed) && isRunning) {
            animator.SetBool(isRunningHash,false);
        }
    }

    private void OnEnable() {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable() {
        playerInput.CharacterControls.Disable();
    }
}
