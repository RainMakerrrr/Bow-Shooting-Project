using ObjectPool;
using UnityEngine;

namespace BowController.Arrows
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private float _constSpeed;
        [SerializeField] private TrailRenderer _trail;
        private Rigidbody _rigidbody;
        
        private void Start() => _rigidbody = GetComponent<Rigidbody>();
        

        public void SetToRope(Transform ropeTransform)
        {
            transform.parent = ropeTransform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            _trail.enabled = false;
        }

        public void Shot(float velocity)
        {
            transform.parent = null;
            
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = transform.forward * velocity * _constSpeed;

            _trail.Clear();
            _trail.enabled = true;
        }

        private void OnCollisionEnter(Collision other) => ArrowPool.Instance.ReturnToPool(this);
        
    }
}