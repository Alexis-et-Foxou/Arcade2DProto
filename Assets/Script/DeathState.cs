using Unity.VisualScripting;
using UnityEngine;

public class DeathState : State {
    public DeathState(GameObject host, float duration = -1) : base(host, duration) { }

    public override void OnEnter() {
        base.OnEnter();
        Animator.SetBool("IsDeath", true);
        if (PlayerController.Rigidbody2D.linearVelocityY == 0)
        {
            PlayerController.Collider2D.enabled = false;
            PlayerController.Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    public override void OnUpdate() {
        base.OnUpdate();
    }
    
    public override void OnExit() {
        base.OnExit();
    }
}