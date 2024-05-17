using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState {

    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
        : base(currentContext, playerStateFactory) {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState() {
        HandleJump();
    }
    public override void UpdateState() {
        CheckSwitchStates();
        HandleGravity();
    }
    public override void ExitState() {
        Ctx.Animator.SetBool(Ctx.IsJumpingHash, false);
        if (Ctx.IsJumpedPressed) {
            Ctx.RequireNewJumpPress = true;
        }
    }

    public override void InitializeSubState() {}

    public override void CheckSwitchStates() {
        if (Ctx.CharacterController.isGrounded) {
            SwitchState(Factory.Grounded());
        }
    }


    private void HandleJump() {
        Ctx.Animator.SetBool(Ctx.IsJumpingHash, true);
        Ctx.IsJumping = true;
        Ctx.CurrentMovementY = Ctx.InitialJumpVelocity;
        Ctx.AppliedMovementY = Ctx.InitialJumpVelocity;
    }
    private void HandleGravity() {
        bool isFalling = Ctx.CurrentMovementY <= 0f || !Ctx.IsJumpedPressed;
        if (isFalling) {
            float prevVelo = Ctx.CurrentMovementY;
            Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.Gravity * Ctx.FallMultiplier * Time.deltaTime);
            Ctx.AppliedMovementY = Mathf.Max((prevVelo + Ctx.CurrentMovementY) * .5f, -20f);

        }
        else {
            float prevVelo = Ctx.CurrentMovementY;
            Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.Gravity * Time.deltaTime);
            Ctx.AppliedMovementY = (prevVelo + Ctx.CurrentMovementY) * .5f;

        }
    }
}
