using Unity.VisualScripting;
using UnityEngine;

public class AttackState : State {
    public GameplayAction _currentAction;
    public AttackState(GameObject host, float duration = -1) : base(host, duration) { }
    
    public override void OnEnter() {
        base.OnEnter();
        _currentAction = PlayerController.NextAction;
        
        InputController.IsAttacking.Stop();
        Animator.runtimeAnimatorController = _currentAction.animatorOverrideController;
        Animator.SetBool("IsAttacking", true);
        
    }

    public override void OnUpdate() {
        base.OnUpdate();
        
    }

    public override void OnExit() {
        base.OnExit();

        if (PlayerController.IsHitSomeone)
        {
            int index = PlayerController.Actions.IndexOf(PlayerController.NextAction) +1;
            if (index >= PlayerController.Actions.Count) index = 0;
            PlayerController.NextAction = PlayerController.Actions[index];
            PlayerController.IsHitSomeone = false;
        }
        
        Animator.SetBool("IsAttacking", false);
    }
    
}