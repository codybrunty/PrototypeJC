using UnityEngine;

public class MovingEntityCommandController {

    private readonly CharacterController characterController;
    private readonly Animator animator;
    private readonly Transform transform;

    private readonly int isWalkingHash;
    private readonly int isRunningHash;
    private readonly int isJumpingHash;
    private readonly int isFallingHash;

    private Vector2 currentMovementInput;
    private Vector3 currentVelocity;
    private Vector3 appliedMovement;

    private bool isMovementPressed = false;
    private bool isRunning = false;
    private bool isJumpPressed = false;

    private bool isActuallyGrounded = false;
    private float groundedTimer = 0f;
    private readonly float groundedGraceTime = 0.2f;

    private float jumpHoldTime = 0f;
    private readonly float maxJumpHoldTime = 0.25f;


    private float moveSpeed;
    private float runSpeed;
    private float rotationSpeed;
    private float gravity;
    private float jumpFallMultiplier;
    private float initialJumpVelocity;

    public MovingEntityCommandController(CharacterController characterController,Animator animator,Transform transform,float moveSpeed,float runSpeed,float rotationSpeed, float maxJumpHeight,float maxJumpTime,float jumpFallMultiplier) {
        this.characterController = characterController;
        this.animator = animator;
        this.transform = transform;
        this.moveSpeed = moveSpeed;
        this.runSpeed = runSpeed;
        this.rotationSpeed = rotationSpeed;
        this.jumpFallMultiplier = jumpFallMultiplier;

        float timeToApex = maxJumpTime / 2f;
        gravity = (-2f * maxJumpHeight) / Mathf.Pow(timeToApex, 2f);
        initialJumpVelocity = (2f * maxJumpHeight) / timeToApex;

        isWalkingHash = Animator.StringToHash("IsWalking");
        isRunningHash = Animator.StringToHash("IsRunning");
        isJumpingHash = Animator.StringToHash("IsJumping");
        isFallingHash = Animator.StringToHash("IsFalling");
    }

    public void Tick() {
        UpdateGroundedState();
        ApplyGravity();
        ApplyMovement();
        ApplyRotation();
        UpdateAnimation();
    }

    private void UpdateGroundedState() {
        if (characterController.isGrounded) {
            groundedTimer = groundedGraceTime;
        }
        else {
            groundedTimer -= Time.deltaTime;
        }

        isActuallyGrounded = groundedTimer > 0f;
    }

    public void SetMoveInput(Vector2 input) {
        currentMovementInput = input;
        isMovementPressed = input.sqrMagnitude > 0.01f;
    }

    public void SetRun(bool isRunning) {
        this.isRunning = isRunning;
    }

    public void Jump(bool isPressed) {
        isJumpPressed = isPressed;

        if (isPressed && isActuallyGrounded) {
            currentVelocity.y = initialJumpVelocity;
            groundedTimer = 0f;
            jumpHoldTime = 0f;
        }
    }
    public bool CanJump() {
        return isActuallyGrounded;
    }
    private void ApplyGravity() {
        if (isActuallyGrounded && currentVelocity.y < 0f) {
            currentVelocity.y = -2f;
        }
        else {
            bool holdingJump = isJumpPressed && jumpHoldTime < maxJumpHoldTime;

            if (holdingJump && currentVelocity.y > 0f) {
                jumpHoldTime += Time.deltaTime;
                currentVelocity.y += gravity * Time.deltaTime;
            }
            else {
                currentVelocity.y += gravity * jumpFallMultiplier * Time.deltaTime;
            }
        }
    }

    private void ApplyMovement() {
        float speed = isRunning ? runSpeed : moveSpeed;

        Vector3 inputDirection = new Vector3(currentMovementInput.x, 0f, currentMovementInput.y);
        Vector3 move = ConvertToCameraSpace(inputDirection.normalized) * speed;

        appliedMovement = new Vector3(move.x, currentVelocity.y, move.z);
        characterController.Move(appliedMovement * Time.deltaTime);
    }
    private Vector3 ConvertToCameraSpace(Vector3 vec) {
        Transform cam = Camera.main.transform;
        Vector3 forward = cam.forward; forward.y = 0;
        Vector3 right = cam.right; right.y = 0;
        return vec.z * forward.normalized + vec.x * right.normalized;
    }
    private void ApplyRotation() {
        if (!isMovementPressed) return;

        Vector3 lookDirection = ConvertToCameraSpace(new Vector3(currentMovementInput.x, 0f, currentMovementInput.y));
        if (lookDirection.sqrMagnitude > 0.01f) {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void UpdateAnimation() {
        bool isJumping = !isActuallyGrounded;
        animator.SetBool(isWalkingHash, isMovementPressed);
        animator.SetBool(isRunningHash, isRunning && isMovementPressed);
        animator.SetBool(isJumpingHash, isJumping && currentVelocity.y > 0.1f);
        animator.SetBool(isFallingHash, isJumping && currentVelocity.y < 0f);
    }
}
