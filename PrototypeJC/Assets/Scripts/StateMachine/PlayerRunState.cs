using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState {

    public PlayerRunState(MovingEntityStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void CheckSwitchStates() {
        if (!Ctx.IsMovementPressed) {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed) {
            SwitchState(Factory.Walk());
        }
    }

    public override void EnterState() {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, true);
    }

    public override void ExitState() {
    }

    public override void InitializeSubState() {
    }

    public override void UpdateState() {
        Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x * Ctx.RunSpeed;
        Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y * Ctx.RunSpeed;
        CheckSwitchStates();
    }
}
