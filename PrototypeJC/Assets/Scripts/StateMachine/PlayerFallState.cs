using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState, IRootState {
    public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) {
        IsRootState = true;
    }

    public override void CheckSwitchStates() {
        if (Ctx.CharacterController.isGrounded) {
            SwitchState(Factory.Grounded());
        }
    }

    public override void EnterState() {
        InitializeSubState();
        Ctx.Animator.SetBool(Ctx.IsFallingHash,true);
    }

    public override void ExitState() {
        Ctx.Animator.SetBool(Ctx.IsFallingHash, false);
    }

    public void HandleGravity() {
        float prevVelo = Ctx.CurrentMovementY;
        Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.Gravity * Ctx.FallMultiplier * Time.deltaTime);
        Ctx.AppliedMovementY = Mathf.Max((prevVelo + Ctx.CurrentMovementY) * .5f, -20f);
    }

    public override void UpdateState() {
        HandleGravity();
        CheckSwitchStates();
    }

    public override void InitializeSubState() {
        if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed) {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed) {
            SetSubState(Factory.Walk());
        }
        else {
            SetSubState(Factory.Run());
        }
    }
}
