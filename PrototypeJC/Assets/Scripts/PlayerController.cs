using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {

    private MovingEntityStateMachine stateMachine;
    private CharacterController characterController;
    private Animator animator;
    
    [Header("Movement Config")]
    public float moveSpeed = 7f;
    public float runSpeed = 10f;
    public float rotationSpeed = 20f;

    [Header("Jump Config")]
    public float maxJumpHeight = 3.5f;
    public float maxJumpTime = 0.65f;
    public float jumpFallMultiplier = 2f;



    private void Awake() {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        stateMachine = new MovingEntityStateMachine(
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
        stateMachine.Tick();
    }

    private void OnDisable() {
        stateMachine.DisableInput();
    }
    private void OnEnable() {
        stateMachine.EnableInput();
    }
    private void OnDestroy() {
        stateMachine.Dispose();
    }
}
