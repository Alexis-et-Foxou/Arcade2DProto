using UnityEngine;

public class JumpState : State {
    public JumpState(PlayerController playerController) : base(playerController) {
    }

    public override void OnEnter() {
        PlayerController.Rigidbody2D.AddForce(Vector2.up * PlayerController.JumpForce,  ForceMode2D.Impulse);
    }

    public override void OnUpdate() {
        //PlayerController.transform.Translate(playerController.Move * (Time.deltaTime * playerController.Speed));
    }
}