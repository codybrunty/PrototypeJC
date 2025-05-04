using UnityEngine;

public interface ICharacter {
    void Move(Vector2 direction);
    void SetRun(bool isRunning);
    void Jump(bool isPressed); // replaces Jump() + SetJumpInput()
    bool CanJump();
    void Teleport(Vector3 pos);
    Vector3 GetPosition();
}
