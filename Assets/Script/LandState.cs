using UnityEngine;

public class LandState : State {
    public LandState(GameObject host, float duration = -1) : base(host, duration) { }
    
    public override void OnEnter() {
        base.OnEnter();
        
        Animator.SetBool("IsLanding", true);
    }

    public override void OnUpdate() {
        base.OnUpdate();
        
        PlayerController.transform.Translate(InputController.Move * 
                                             (Time.deltaTime * PlayerController.Speed/10));
    }

    public override void OnExit() {
        base.OnExit();
        
        Animator.SetBool("IsLanding", false);
    }
}