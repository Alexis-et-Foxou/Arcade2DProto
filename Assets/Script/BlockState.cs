using UnityEngine;

public class BlockState : State {
    public BlockState(GameObject host, float duration = -1) : base(host, duration) { }

    public override void OnEnter() {
        base.OnEnter();
        PlayerController.Shield.SetActive(true);
        Animator.SetBool("IsBlock", true);
    }

    public override void OnUpdate() {
        base.OnUpdate();
        
        PlayerController.transform.Translate(InputController.Move * 
                                             (Time.deltaTime * PlayerController.Speed/4));
    }

    public override void OnExit() {
        PlayerController.Shield.SetActive(false);
        base.OnExit();
        Animator.SetBool("IsBlock", false);
    }
}