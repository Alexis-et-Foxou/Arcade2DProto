using System;
using System.Collections.Generic;
using System.Linq;
using Script;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float _speed;
    [SerializeField] private float _airControl;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private PolygonCollider2D _hurtBox;
    [SerializeField] private GameplayAction _nextAction;
    [SerializeField] private GameObject _shield;
    [SerializeField] private bool _startingToAttack;
    
    
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private SpriteRenderer _spriteRenderer;
    private State _currentState;
    private InputController _inputController;
    private Sprite _lastSprite;
    private readonly List<Vector2> _points = new();
    private Player _player;
    private bool _playerHit;
    private bool _isHitSomeone;
    
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    public bool IsShiledUp => _inputController.IsBlock;
    public Collider2D Collider2D => _collider2D;
    public GameObject Shield => _shield;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public float Speed => _speed;
    public float JumpForce => _jumpForce;
    public PolygonCollider2D HurtBox => _hurtBox;

    public bool PlayerHit
    {
        get =>  _playerHit;
        set => _playerHit = value;
    }
    public bool IsHitSomeone
    {
        get => _isHitSomeone;
        set => _isHitSomeone = value;
    }

    public float AirControl => _airControl;
    [SerializeField] private List<GameplayAction> _actions;
    public List<GameplayAction>Actions => _actions;
    public bool IsGrounded {
        get {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _groundLayer);
            return hit.collider != null;
        }
    }
    
    public GameplayAction NextAction
    {
        get => _nextAction;
        set => _nextAction = value;
    }
    
    private void Start() {
        _nextAction = _actions.First();
        
    }

    private void Awake() {
        _player = GetComponent<Player>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D =  GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _inputController = GetComponent<InputController>();
        
        SwitchState(new WalkState(gameObject));
    }

    private void Update() {
        _currentState.OnUpdate();
        Debug.DrawRay(transform.position, Vector2.down * _groundCheckDistance, Color.red);
        Debug.Log(IsShiledUp);
        
        // Transitions

        if (_inputController.DebugPress)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (_player.IsDead)
        {
            SwitchState(new DeathState(gameObject));
        }

        if (_playerHit)
        {
            SwitchState(new HitstunState(gameObject, 0.5f));
            OnAttackFinished();
        }
        if (_currentState is HitstunState { IsFinished: true })
        {
            SwitchState(new IdleState(gameObject));
        }
        
        //if (_currentState is AttackState && _startingToAttack)

        if (_currentState is WalkState && _inputController.IsAttacking.Value && !_playerHit) {
            SwitchState(new AttackState(gameObject, _nextAction.Speed));
        }
        if (_currentState is IdleState && _inputController.IsAttacking.Value && !_playerHit) {
            SwitchState(new AttackState(gameObject, _nextAction.Speed));
        }
        
        if (_currentState is AttackState { IsFinished: true }) {
            SwitchState(new IdleState(gameObject));
        }

        if (_currentState is IdleState && _inputController.IsBlock)
        {
            SwitchState(new BlockState(gameObject));
        }
        if (_currentState is WalkState && _inputController.IsBlock)
        {
            SwitchState(new BlockState(gameObject));
        }

        if (_currentState is BlockState && !_inputController.IsBlock)
        {
            SwitchState(new IdleState(gameObject));
        }

        // Transitions -- Jump
        
        if (_currentState is WalkState && _inputController.IsJumping.Value && IsGrounded) {
            SwitchState(new JumpState(gameObject, 0.5f));
        }
        if (_currentState is IdleState && _inputController.IsJumping.Value && IsGrounded) {
            SwitchState(new JumpState(gameObject, 0.5f));
        }

        if (_currentState is DashState { IsFinished: true })
        {
            SwitchState(new FallState(gameObject));
        }

        if (_currentState is WalkState && !IsGrounded && !_inputController.IsJumping.Value)
        {
            SwitchState(new FallState(gameObject));
        }
        
        if (_currentState is FallState && IsGrounded)
        {
            SwitchState(new LandState(gameObject, 0.3f));
        }
        
        if (_currentState is JumpState && IsGrounded && !_inputController.IsJumping.Value)
        {
            SwitchState(new LandState(gameObject, 0.3f));
        }
        
        if (_currentState is JumpUpState && IsGrounded)
        {
            SwitchState(new LandState(gameObject, 0.3f));
        }

        if (_currentState is LandState { IsFinished: true })
        {
            SwitchState(new WalkState(gameObject));
        }
        
        if (_currentState is LandState { IsFinished: true })
        {
            SwitchState(new IdleState(gameObject));
        }

        if (_currentState is JumpState { IsFinished: true })
        {
            SwitchState(new JumpUpState(gameObject));
        }

        if (_currentState is JumpUpState && _rigidbody2D.linearVelocity.y <= -1)
        {
            SwitchState(new FallState(gameObject));
        }
        if (_currentState is FallState && _inputController.IsDash.Value)
        {
            SwitchState(new DashState(gameObject, 0.5f));
        }
        if (_currentState is JumpUpState && _inputController.IsDash.Value)
        {
            SwitchState(new DashState(gameObject, 0.5f));
        }

        if (_currentState is DashState && IsGrounded)
        {
            SwitchState(new LandState(gameObject, 0.3f));
        }
        
        // Transition -- move

        if (_currentState is IdleState && _inputController.IsMoving)
        {
            SwitchState(new WalkState(gameObject));
        }
        if (_currentState is WalkState && !_inputController.IsMoving)
        {
            SwitchState(new IdleState(gameObject));
        }
        if (_currentState is JumpState && !_inputController.IsJumping.Value && IsGrounded) {
            SwitchState(new WalkState(gameObject));
        }
    }
    
    private void SwitchState(State state) {
        _currentState?.OnExit();
        _currentState = state;
        _currentState?.OnEnter();
    }

    private bool _hurtBoxActive;

    public void OnAttackActive() {
        _startingToAttack = false;
        _hurtBoxActive = true;
        _lastSprite = null;
        _hurtBox.enabled = true;
    }

    public void OnAttackFinished() {
        _hurtBoxActive = false;
        _hurtBox.enabled = false;
    }

    public void OnAttackStart()
    {
        _startingToAttack = true;
    }

    private void LateUpdate() {
        if (!_hurtBoxActive) return;
        
        Sprite sprite = _spriteRenderer.sprite;
        if (sprite == _lastSprite) return;
        _lastSprite = sprite;

        int shapeCount = sprite != null ? sprite.GetPhysicsShapeCount() : 0;
        _hurtBox.pathCount = shapeCount;

        for (int i = 0; i < shapeCount; i++) {
            sprite.GetPhysicsShape(i, _points);

            if (_spriteRenderer.flipX)
                for (int p = 0; p < _points.Count; p++)
                    _points[p] = new Vector2(-_points[p].x, _points[p].y);

            _hurtBox.SetPath(i, _points);
        }
    }
}