using Cinemachine;
using UnityEngine;

namespace PlayerController.CameraExtension
{
    public class CinemachinePOVExtension : CinemachineExtension
    {
        [SerializeField] private float _clampedAngle = 80f;
        [SerializeField] private float _horizontalSpeed = 7f;
        [SerializeField] private float _verticalSpeed = 7f;

        private PlayerMovementInput _movementInput;
        private Vector3 _startRotation;

        protected override void Awake()
        {
            if (_startRotation == Vector3.zero)
                _startRotation = transform.localRotation.eulerAngles;

            _movementInput = FindObjectOfType<PlayerMovementInput>();
            Cursor.visible = false;
            base.Awake();
        }

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (vcam.Follow)
            {
                if (stage == CinemachineCore.Stage.Aim)
                {
                    var delta = _movementInput.MouseDelta;

                    _startRotation.x += delta.x * _verticalSpeed * Time.deltaTime;
                    _startRotation.y += delta.y * _horizontalSpeed * Time.deltaTime;

                    _startRotation.y = Mathf.Clamp(_startRotation.y, -_clampedAngle, _clampedAngle);

                    state.RawOrientation = Quaternion.Euler(-_startRotation.y, _startRotation.x, 0f);
                }
            }
        }
    }
}