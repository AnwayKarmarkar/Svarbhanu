using UnityEngine;

namespace Assets.Scripts {
    public class CameraSmoothFollow : MonoBehaviour {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _damping;

        public Transform Target;
        private Vector3 _velocity = Vector3.zero;

        private void FixedUpdate() {
            var targetPosition = Target.position + _offset;
            targetPosition.z = transform.position.z;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _damping);
        }
    }
}
