using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts {
    public class PlayerMovement : MonoBehaviour {
        public Rigidbody2D Rb;
        public float MovementSpeed = 5f;

        private Vector2 _movement;
        private Vector2 _mousePos;
        public Camera Camera;

        [Header("DashSettings")] 
        [SerializeField]
        private float _dashDistance = 3f;
        [SerializeField]
        private float _dashCooldownTime = 2f;
        private float _dashCooldown;
        private bool _isDashing;
        private bool _canDash = true;

        void Start() {
            _canDash = true;
        }

        // Update is called once per frame
        void Update() {
            if (_isDashing) return;
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

            _mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);

            if (!_canDash) {
                _dashCooldown -= Time.deltaTime;
                if (_dashCooldown <= 0) {
                    _canDash = true;
                }
            }
            else {
                if (!Input.GetKeyDown(KeyCode.Space)) return;
                Dash();
                _canDash = false;
                _dashCooldown = _dashCooldownTime;
            }
        } 

        public void FixedUpdate() {
            Rb.MovePosition(Rb.position + _movement * MovementSpeed * Time.fixedDeltaTime);

            var lookDir = _mousePos - Rb.position;
            var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            Rb.rotation = angle;
        }

        private void Dash() {

            _canDash = false;
            _isDashing = true;

            Rb.position += _movement * _dashDistance;
            _isDashing = false;

            _canDash = true;
        }
    }
}
