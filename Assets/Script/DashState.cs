using UnityEngine;

    public class DashState : State
{
    public DashState(GameObject host, float duration = -1) : base(host, duration) { }

    public override void OnEnter() {
        base.OnEnter();
        
        //PlayerController.SpriteRenderer.flipX = ;
        
        InputController.IsDash.Stop();
        PlayerController.Rigidbody2D.AddForce(InputController.Move * PlayerController.JumpForce,  ForceMode2D.Impulse);
        Animator.SetBool("IsDash", true);
    }

    public override void OnExit() {
        base.OnExit();
        
        Animator.SetBool("IsDash", false);
    }
}