using UnityEngine;

namespace PlayerController
{
    public class PlayerMovementInput : MonoBehaviour
    {
        private PlayerControl _playerControl;

        public Vector2 PlayerMovement => _playerControl.Player.Movement.ReadValue<Vector2>();
        public Vector2 MouseDelta => _playerControl.Player.Look.ReadValue<Vector2>();

        private void Awake() =>_playerControl = new PlayerControl();
        
        private void OnEnable() => _playerControl.Enable();
        private void OnDisable() => _playerControl.Disable();
        

    }
}