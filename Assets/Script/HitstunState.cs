using UnityEngine;

public class HitstunState : State
{
    public HitstunState(GameObject host, float duration = -1) : base(host, duration) { }

    public override void OnEnter() {
        base.OnEnter();
        
        //PlayerController.SpriteRenderer.flipX = ;
        
        Animator.SetBool("IsStun", true);
    }

    public override void OnUpdate() {
        base.OnUpdate();
    }

    public override void OnExit() {
        base.OnExit();
        
        PlayerController.PlayerHit = false;
        Animator.SetBool("IsStun", false);
    }
}