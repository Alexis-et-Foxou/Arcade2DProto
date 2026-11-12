using System.Transactions;
using UnityEngine;

public class WalkState : State {
    public WalkState(GameObject host, float duration = -1) : base(host, duration) { }

    public override void OnEnter() {
        base.OnEnter();
        Animator.SetBool("IsMoving", true);
    }

    public override void OnUpdate() {
        base.OnUpdate();
        if(InputController.Move.x != 0)PlayerController.SpriteRenderer.flipX = InputController.Move.x < 0;
        PlayerController.transform.Translate(InputController.Move * 
                                             (Time.deltaTime * PlayerController.Speed));
    }
    
    public override void OnExit() {
        base.OnExit();
        Animator.SetBool("IsMoving", false);
    }
}