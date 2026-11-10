using UnityEngine;

public class FallState : State {
    public FallState(GameObject host, float duration = -1) : base(host, duration) { }

    public override void OnUpdate() {
        base.OnUpdate();
        
        Animator.SetBool("IsFalling", true);
        PlayerController.SpriteRenderer.flipX = InputController.Move.x < 0;
        float velocity = InputController.Move.x * (PlayerController.AirControl * Time.deltaTime);
        PlayerController.Rigidbody2D.linearVelocity = new Vector2(velocity, PlayerController.Rigidbody2D.linearVelocity.y);
    }

    public override void OnExit() {
        base.OnExit();
        
        Animator.SetBool("IsFalling", false);
    }
}