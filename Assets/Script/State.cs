public abstract class State {
    protected PlayerController PlayerController;

    protected State(PlayerController playerController) {
        PlayerController = playerController;
    }
    
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
}