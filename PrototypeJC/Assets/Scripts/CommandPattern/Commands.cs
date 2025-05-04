using UnityEngine;
public class MoveCommand : ICommand {
    private readonly ICharacter character;
    private readonly Vector2 input;
    private Vector3 previousPos;

    public MoveCommand(ICharacter character, Vector2 input) {
        this.character = character;
        this.input = input;
    }

    public void Execute() {
        previousPos = character.GetPosition();
        character.Move(input);
    }

    public void Undo() {
        character.Teleport(previousPos);
    }
}
public class RunCommand : ICommand {
    private readonly ICharacter character;
    private readonly bool isRunning;

    public RunCommand(ICharacter character, bool isRunning) {
        this.character = character;
        this.isRunning = isRunning;
    }

    public void Execute() {
        character.SetRun(isRunning);
    }

    public void Undo() {

    }
}

public class JumpCommand : ICommand {
    private readonly ICharacter character;
    private readonly bool isJumping;

    public JumpCommand(ICharacter character, bool isJumping) {
        this.character = character;
        this.isJumping = isJumping;
    }

    public void Execute() {
        character.Jump(isJumping);
    }

    public void Undo() {

    }
}