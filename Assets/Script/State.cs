using UnityEngine;

public abstract class State {
    protected InputController InputController;
    protected PlayerController PlayerController;
    protected Animator Animator;
    protected float Duration;
    private float _timer;
    
    public bool IsFinished => _timer is > -1 and <= 0f;
    
    protected State(GameObject host, float duration) {
        InputController = host.GetComponent<InputController>();
        PlayerController = host.GetComponent<PlayerController>();
        Animator = host.GetComponent<Animator>();
        Duration = duration;
    }

    public virtual void OnEnter() {
        _timer = Duration;
    }

    public virtual void OnUpdate() {
        if (Duration > 0f) _timer -= Time.deltaTime;
    }
    public virtual void OnExit() { }
}