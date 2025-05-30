using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState {

    public PlayerIdleState(MovingEntityStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void CheckSwitchStates() {
        if(Ctx.IsMovementPressed && Ctx.IsRunPressed) {
            SwitchState(Factory.Run());
        }
        else if(Ctx.IsMovementPressed){
            SwitchState(Factory.Walk());
        }
    }

    public override void EnterState() {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
    }

    public override void ExitState() {
    }

    public override void InitializeSubState() {
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }
}
