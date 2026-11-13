using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputController : MonoBehaviour {

    public Vector2 Move;
    public bool DebugPress;
    public Trigger IsJumping;
    public Trigger IsAttacking;
    public Trigger IsDash;
    public bool IsBlock;
    [FormerlySerializedAs("IsMove")] public bool IsMoving;

    public void OnMove(InputValue value) {
        Move = value.Get<Vector2>();
        IsMoving = Move != Vector2.zero;
    }

    public void OnJump(InputValue value) {
        if (value.isPressed) IsJumping.Launch();
    }

    public void OnAttack(InputValue value) {
        if (value.isPressed) IsAttacking.Launch();
    }

    public void OnDash(InputValue value) {
        if (value.isPressed)
        { IsDash.Launch(); }
    }

    public void OnBlock(InputValue value) {
        
        IsBlock = value.isPressed;
       
        
    }

    public void OnEscape(InputValue value) {
        DebugPress =  value.isPressed;
    }
}