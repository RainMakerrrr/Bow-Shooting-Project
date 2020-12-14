using UnityEngine;

namespace PlayerController
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    { 
        [SerializeField] private float _playerSpeed;
        
        private Vector3 _playerVelocity;
        private bool _isGrounded;
        private CharacterController _characterController;
        private PlayerMovementInput _playerInput;
        private Camera _mainCamera;
        
        private const float _gravity = -9.81f;
        
        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _playerInput = GetComponent<PlayerMovementInput>();

            _mainCamera = Camera.main;
        }

        private void Update()
        {
            _isGrounded = _characterController.isGrounded;

            if (_isGrounded && _playerVelocity.y < 0f)
                _playerVelocity.y = 0f;

            var movement = _playerInput.PlayerMovement;
            var currentMove = new Vector3(movement.x, 0f, movement.y);
            currentMove.y = 0f;
            
            currentMove = _mainCamera.transform.forward * currentMove.z + _mainCamera.transform.right * currentMove.x;
            
            _characterController.Move( currentMove * _playerSpeed * Time.deltaTime);
            

            _playerVelocity.y += _gravity * Time.deltaTime;
            _characterController.Move(_playerVelocity * Time.deltaTime);


        }
    }
}