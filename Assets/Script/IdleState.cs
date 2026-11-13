using UnityEngine;

public class IdleState : State {
    public IdleState(GameObject host, float duration = -1) : base(host, duration) { }

    public override void OnEnter() {
        base.OnEnter();
        
        //Animator.SetBool("IsIdle", true);
    }

    public override void OnUpdate() {
        base.OnUpdate();
        
        PlayerController.transform.Translate(InputController.Move * 
                                             (Time.deltaTime * PlayerController.Speed));
    }

    public override void OnExit() {
        base.OnExit();
        //Animator.SetBool("IsIdle", false);
    }
}