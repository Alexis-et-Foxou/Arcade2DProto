using UnityEngine;

namespace Script
{
    public class Player:MonoBehaviour
    {
        private int _health = 500;
        private bool _isDead;
        private SpriteRenderer _renderer;
        private Rigidbody2D _rigidbody2D;
        
        public string PlayerName;
        public Color PlayerColor;
        
        private void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _renderer.color = PlayerColor;
        }

        public bool IsDead {
            get => _isDead;
        }

        public int Health {
            get => _health;
        }

        public void DoDamage(int damage)
        {
            Debug.Log(damage + " " + _health);
            _health -= damage;
            Debug.Log(_health);
            GetComponent<SpriteRenderer>().color = Color.red;
            if (_health <= 0) _isDead = true;
            Invoke(nameof(RollbackColor), 0.25f);
            
        }
        private void RollbackColor() {
            _renderer.color = PlayerColor;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            
            if (other.gameObject.CompareTag("HurtBox"))
            {
                PlayerController player = other.transform.parent.GetComponent<PlayerController>();
                
                DoDamage(player.NextAction.Damage);
                _rigidbody2D.AddForce(Vector2.up * player.NextAction.Y_Kb, ForceMode2D.Impulse);
                Debug.Log("Hurt Box Hit and make " + player.NextAction.Damage + " damage");
            }
        }
    }
}