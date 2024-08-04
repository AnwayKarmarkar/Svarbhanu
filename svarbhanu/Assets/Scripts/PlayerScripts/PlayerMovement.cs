using UnityEngine;

namespace Assets.Scripts.PlayerScripts {
    public class PlayerMovement : MonoBehaviour {
        public Rigidbody2D Rb;
        public float MovementSpeed = 5f;

        private Vector2 _movement;
        private Vector2 _mousePos;
        public Camera Camera;

        // Update is called once per frame
        void Update() {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

            _mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(_mousePos);
        }

        public void FixedUpdate() {
            Rb.MovePosition(Rb.position + _movement * MovementSpeed * Time.fixedDeltaTime);

            var lookDir = _mousePos - Rb.position;
            var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            Rb.rotation = angle;
        }
    }
}
