using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float _speed;
    [SerializeField] private float _airControl;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _groundLayer;
    
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private InputController _inputController;
    private State _currentState;
    private Animator _animator;
    
    private Sprite _LastSprite;
    
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public float Speed => _speed;
    public float JumpForce => _jumpForce;
    public float AirControl => _airControl;
    public bool IsGrounded {
        get {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _groundLayer);
            return hit.collider != null;
        }
    }

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _inputController =  GetComponent<InputController>();
        _animator = GetComponent<Animator>();
        Debug.Log(gameObject);
        SwitchState(new MoveState(gameObject));
    }

    private void Update() {
        _currentState.OnUpdate();
        
        // Transitions
        if (_currentState is MoveState && _inputController.IsJumping && IsGrounded) {
            SwitchState(new JumpState(gameObject));
        }

        if (_currentState is JumpState && !_inputController.IsJumping && IsGrounded) {
            SwitchState(new MoveState(gameObject));
        }

        if (_currentState is MoveState && _inputController.IsAttacking.Value) {
            SwitchState(new AttackState(gameObject, 1));
        }

        if (_currentState is AttackState { IsFinished: true }) {
            SwitchState(new MoveState(gameObject));
        }
        
        Debug.Log(_currentState.GetType());
    }

    public void OnAttackActive() {
        Sprite sprite = _spriteRenderer.sprite;
        if (sprite == _lastSprite) return;

        _LastSprite = sprite;

        int shapeCount = sprite != null ? sprite.GetPhysicsShapeCount() : 0;
        _hurtBox. pathCount = shapeCount;

        for (int i = 0; i < shapeCount; i++)
        {
            sprite.GetPhysicsShape(i, _points);

            for (int p = 0; p < _points.Count; p++)
            {
                points[p] = new Vector2(-_ points[p].x, _points[p].y);
                int nextIndex = p + 1;
                
                Debug.DrawLine(_points[p], _points[nextIndex], Color.red);
            }
            
            _hurtBox.SetPath(i, points);
        }

        Debug.Log($"[{Time.frameCount}] sprite={sprite ?. name} shapes={shapeCount} " +
                  $"points lus={CountPoints(sprite)} points collider={_hurtBox.GetTotalPointCount()}");
            
    }
    

    private void SwitchState(State state) {
        _currentState?.OnExit();
        _currentState = state;
        _currentState?.OnEnter();
    }
}