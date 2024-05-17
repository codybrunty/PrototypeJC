using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour{

    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isFallingHash;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 appliedMovement;

    bool isMovementPressed = false;
    bool isRunPressed = false;
    bool isJumpedPressed = false;

    [Header("Movement")]
    public float moveSpeed = 1f;
    public float runSpeed = 1f;
    public float rotationSpeed = 1f;
    private float gravity = -9.8f;

    [Header("Jumping")]
    private bool isJumping = false;
    private float initialJumpVelocity;
    public float maxJumpHeight = 1f;
    public float maxJumpTime = 0.5f;
    public float jumpingFallMultiplier = 2f;
    private bool requireNewJumpPress = false;



    PlayerBaseState currentState;
    PlayerStateFactory states;

    public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public Animator Animator { get { return animator; } }
    public int IsJumpingHash { get { return isJumpingHash; } }
    public int IsWalkingHash { get { return isWalkingHash; } }
    public int IsFallingHash { get { return isFallingHash; } }
    public int IsRunningHash { get { return isRunningHash; } }
    public bool IsJumpedPressed { get { return isJumpedPressed; } }
    public bool IsJumping { set { isJumping=value; } }
    public bool RequireNewJumpPress { get { return requireNewJumpPress; } set { requireNewJumpPress = value; } }
    public float CurrentMovementY { get { return currentMovement.y; } set { currentMovement.y = value; } }
    public float AppliedMovementX { get { return appliedMovement.x; } set { appliedMovement.x = value; } }
    public float AppliedMovementY { get { return appliedMovement.y; } set { appliedMovement.y = value; } }
    public float AppliedMovementZ { get { return appliedMovement.z; } set { appliedMovement.z = value; } }

    public float InitialJumpVelocity { get { return initialJumpVelocity; } }
    public CharacterController CharacterController { get { return characterController; } }
    public float Gravity { get { return gravity; } }
    public float RunSpeed { get { return runSpeed; } }
    public float MoveSpeed { get { return moveSpeed; } }

    public float FallMultiplier { get { return jumpingFallMultiplier; } }
    public bool IsMovementPressed { get { return isMovementPressed; } }
    public bool IsRunPressed { get { return isRunPressed; } }
    public Vector2 CurrentMovementInput { get { return currentMovementInput; } }

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
        states = new PlayerStateFactory(this);
        currentState = states.Grounded();
        currentState.EnterState();

        isWalkingHash = Animator.StringToHash("IsWalking");
        isRunningHash = Animator.StringToHash("IsRunning");
        isJumpingHash = Animator.StringToHash("IsJumping");
        isFallingHash = Animator.StringToHash("IsFalling");
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
    private void Start() {
        characterController.Move(appliedMovement*Time.deltaTime);
    }
    private void Update() {
        HandleRotation();
        HandleMovement();
        currentState.UpdateStates();
    }

    private void HandleMovement() {
        characterController.Move(appliedMovement * Time.deltaTime);
    }
    private void HandleRotation() {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;
        if (isMovementPressed) {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }
    private void SetupJumpVariables() {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    private void OnMovementInput(InputAction.CallbackContext context) {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void OnJump(InputAction.CallbackContext context) {
        isJumpedPressed = context.ReadValueAsButton();
        requireNewJumpPress = false;
    }

    private void OnRun(InputAction.CallbackContext context) {
        isRunPressed = context.ReadValueAsButton();
    }
    private void OnEnable() {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable() {
        playerInput.CharacterControls.Disable();
    }
}
