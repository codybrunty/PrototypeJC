using UnityEngine;
public class InputHandler : MonoBehaviour {

    private CommandExecutor executor;
    private ICharacter character;
    private PlayerInput playerInput;

    private void Awake() {
        executor = new CommandExecutor();
        character = GetComponent<ICharacter>(); 
        playerInput = new PlayerInput();
    }
    private void Update() {
        if (UnityEngine.InputSystem.Keyboard.current.uKey.wasReleasedThisFrame) {
            UndoAll();
        }
        if (UnityEngine.InputSystem.Keyboard.current.zKey.wasReleasedThisFrame) {
            UndoLastCommand();
        }
    }
    private void OnEnable() {
        playerInput.CharacterControls.Enable();
        playerInput.CharacterControls.Move.performed += OnMovePerformed;
        playerInput.CharacterControls.Move.canceled += OnMoveCanceled;
        playerInput.CharacterControls.Run.started += OnRunStarted;
        playerInput.CharacterControls.Run.canceled += OnRunCanceled; 
        playerInput.CharacterControls.Jump.started += OnJumpStarted;
        playerInput.CharacterControls.Jump.canceled += OnJumpCanceled; // <--- Add this

    }
    private void OnDisable() {
        playerInput.CharacterControls.Move.performed -= OnMovePerformed;
        playerInput.CharacterControls.Move.canceled -= OnMoveCanceled;
        playerInput.CharacterControls.Run.started -= OnRunStarted;
        playerInput.CharacterControls.Run.canceled -= OnRunCanceled; 
        playerInput.CharacterControls.Jump.started -= OnJumpStarted;
        playerInput.CharacterControls.Jump.canceled -= OnJumpCanceled; // <--- Add this

        playerInput.CharacterControls.Disable();
    }
    private void OnMovePerformed(UnityEngine.InputSystem.InputAction.CallbackContext ctx) {
        executor.ExecuteCommand(new MoveCommand(character, ctx.ReadValue<Vector2>()));
    }

    private void OnMoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext ctx) {
        executor.ExecuteCommand(new MoveCommand(character, Vector2.zero));
    }

    private void OnRunStarted(UnityEngine.InputSystem.InputAction.CallbackContext ctx) {
        executor.ExecuteCommand(new RunCommand(character, true));
    }

    private void OnRunCanceled(UnityEngine.InputSystem.InputAction.CallbackContext ctx) {
        executor.ExecuteCommand(new RunCommand(character, false));
    }

    private void OnJumpStarted(UnityEngine.InputSystem.InputAction.CallbackContext ctx) {
        executor.ExecuteCommand(new JumpCommand(character, true));
    }
    private void OnJumpCanceled(UnityEngine.InputSystem.InputAction.CallbackContext ctx) {
        executor.ExecuteCommand(new JumpCommand(character, false));
    }
    public void UndoAll() {
        executor.UndoAllCommands();
    }
    public void UndoLastCommand() {
        executor.UndoLastCommand();
    }
}
