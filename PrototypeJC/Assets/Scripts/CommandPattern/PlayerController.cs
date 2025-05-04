using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour, ICharacter {
    private MovingEntityCommandController commandController;
    private CharacterController characterController;
    private Animator animator;

    [Header("Movement Config")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float rotationSpeed = 10f;

    [Header("Jump Config")]
    public float maxJumpHeight = 2f;
    public float maxJumpTime = 0.5f;
    public float jumpFallMultiplier = 2f;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Transform tf = transform;

        commandController = new MovingEntityCommandController(
             characterController,
             animator,
             this.transform,
             moveSpeed,
             runSpeed,
             rotationSpeed,
             maxJumpHeight,
             maxJumpTime,
             jumpFallMultiplier
         );
    }
    private void Update() {
        commandController.Tick();
    }
    public void Move(Vector2 input) {
        commandController.SetMoveInput(input);
    }

    public void SetRun(bool isRunning) {
        commandController.SetRun(isRunning);
    }

    public void Jump(bool isPressed) {
        commandController.Jump(isPressed);
    }

    public bool CanJump() {
        return commandController.CanJump();
    }
}
