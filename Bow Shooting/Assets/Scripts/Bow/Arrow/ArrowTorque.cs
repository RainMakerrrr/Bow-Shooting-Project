using UnityEngine;

namespace BowController.Arrows
{
    public class ArrowTorque : MonoBehaviour
    {
        [SerializeField] private float _torqueVelocity;
        [SerializeField] private float _torqueAngularVelocity;
        
        private Rigidbody _rigidbody;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            var cross = Vector3.Cross(transform.forward, _rigidbody.velocity.normalized);
            
            _rigidbody.AddTorque(cross * _rigidbody.velocity.magnitude * _torqueVelocity);
            _rigidbody.AddTorque(
                (-_rigidbody.angularVelocity + Vector3.Project(_rigidbody.angularVelocity, transform.forward)) *
                _rigidbody.velocity.magnitude * _torqueAngularVelocity);
        }
    }
}