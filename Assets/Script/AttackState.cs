using UnityEngine;

public class AttackState : State {
    public AttackState(GameObject host, float duration = -1) : base(host, duration) { }

    public override void OnEnter() {
        base.OnEnter();

        InputController.IsAttacking.Stop();
        Animator.SetBool("IsAttacking", true);
    }

    public override void OnExit() {
        base.OnExit();
        PlayerController.SpriteRenderer.color = Color.white;
        Animator.SetBool("IsAttacking", false);
    }
    
}