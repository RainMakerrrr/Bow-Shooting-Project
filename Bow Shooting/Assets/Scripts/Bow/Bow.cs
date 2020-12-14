using BowController.Arrows;
using ObjectPool;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace BowController
{
    public class Bow : MonoBehaviour
    {
        private Arrow _currentArrow;
        private BowRope _bowRope;

        private ButtonControl _buttonControl;
        

        private void Start() => _bowRope = GetComponentInChildren<BowRope>();
        
        private void Update()
        {
            _buttonControl = Mouse.current.leftButton;

            if (_buttonControl == null) return;
            
            if (_buttonControl.wasPressedThisFrame)
                SpawnArrow();

            if (_buttonControl.isPressed)
            {
                _bowRope.StartStreachingRope();
                _currentArrow.SetToRope(_bowRope.transform);
            }

            if (_currentArrow != null && _buttonControl.wasReleasedThisFrame)
            {
                StartCoroutine(_bowRope.StopStreachingRope());

                _currentArrow.Shot(_bowRope.Stretching);
                _bowRope.Stretching = 0f;
            }
        }

        private void SpawnArrow()
        {
            var arrow = ArrowPool.Instance.Get();
            arrow.transform.position = transform.position - Vector3.back;
            arrow.GetComponent<Rigidbody>().isKinematic = true;
            
            arrow.gameObject.SetActive(true);
            _currentArrow = arrow;
        }
    }
}