using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private State _currentState;
    private Rigidbody2D _rigidbody2d;
    private bool _isJumping;
    private Vector2 _move;
    
    public Vector2 Move => _move;
    public float Speed => _speed;
    public float JumpForce => _jumpForce;
    public Rigidbody2D Rigidbody2D => _rigidbody2d;

    public bool IsGrounded {
        get {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f);
                LayerMask.GetMask("Ground");
            return hit.collider != null;
        }
    }
    
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private void Awake() {
        
        _rigidbody2d = GetComponent<Rigidbody2D>();
        SwitchState(new MouvState(this));
    }

    private void Update() {
        _currentState.OnUpdate();

        if (_currentState is MouvState && _isJumping && IsGrounded)
        {
            SwitchState(new JumpState(this));
        }

        if (_currentState is JumpState && !_isJumping && IsGrounded)
        {
            SwitchState(new MouvState(this));
        }
    }

    public void SwitchState(State state) {
        _currentState?.OnExit();
        _currentState = state;
        _currentState.OnEnter();
    }

    public void OnMove(InputValue value) {
        _move = value.Get<Vector2>();
    }

    public void OnJump(InputValue value) {
        _isJumping = value.isPressed;
    }
    
    
}