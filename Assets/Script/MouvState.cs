using UnityEngine;

public class MouvState : State
{

    public MouvState(PlayerController playerController) : base(playerController) {
    }

    public override void OnEnter() { }

    public override void OnUpdate() {
        PlayerController.transform.Translate(PlayerController.Move * (Time.deltaTime * PlayerController.Speed));
    }
    public override void OnExit() { }
}