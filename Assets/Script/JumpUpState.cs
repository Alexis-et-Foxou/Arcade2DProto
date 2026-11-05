using UnityEngine;

public class JumpUpState : State {
    public JumpUpState(GameObject host, float duration = -1) : base(host, duration) { }
    
    public override void OnUpdate() {
        base.OnUpdate();
        
        Animator.SetBool("IsJumped", true);
        float velocity = InputController.Move.x * (PlayerController.AirControl * Time.deltaTime);
        PlayerController.Rigidbody2D.linearVelocity = new Vector2(velocity, PlayerController.Rigidbody2D.linearVelocity.y);
    }

    public override void OnExit() {
        base.OnExit();
        
        Animator.SetBool("IsJumped", false);
    }
}