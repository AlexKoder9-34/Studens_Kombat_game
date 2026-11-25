using ScriptableObjects;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IControllable
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private float verticalVelocity;
    [SerializeField] private float gravityForce;
    
    private float _beginMoveSpeed;
    private float _moveSpeed;
    private float _speedBoost;
    private float _jumpHeight;
    private bool _isSprinting;
    
    private CharacterController _controller;
    private Vector3 _moveDirection;
    private Animator _playerAnimator;
        
        
    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _controller  = GetComponent<CharacterController>();
        
        _speedBoost = playerData.SpeedBoost;
        _beginMoveSpeed = playerData.MoveSpeed;
        _jumpHeight = playerData.JumpHeight;
        _moveSpeed = _beginMoveSpeed;
    }

    void Update()
    {
        DoGravity();
        MovePlayer();
    }
        
    public void Move(Vector3 direction)
    {
        _moveDirection = direction;
    }

    private void MovePlayer()
    {
        Vector3 movement = _moveDirection * _moveSpeed;

        movement.y = verticalVelocity;
            
        _controller.Move(movement * Time.deltaTime);
        AnimateMoving();
    }
        
    private void DoGravity()
    {
        if (_controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -5;
        }

        verticalVelocity += gravityForce * Time.deltaTime;
    }

    public void Jump()
    {
        if (_controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(_jumpHeight * -2 * gravityForce);
        }
    }
    private void AnimateMoving()
    {
        if (_moveDirection.x > 0)
        {
            if (_isSprinting)
            {
                _playerAnimator.SetInteger("RunBack", 0);
                _playerAnimator.SetInteger("RunFoward", 1);
            }
            else
            {
                _playerAnimator.SetInteger("GoBack", 0);
                _playerAnimator.SetInteger("GoFoward", 1);
            }
        }
        else if (_moveDirection.x < 0)
        {
            if (_isSprinting)
            {
                _playerAnimator.SetInteger("RunBack", 1);
                _playerAnimator.SetInteger("RunFoward", 0);
            }
            else
            {
                _playerAnimator.SetInteger("GoBack", 1);
                _playerAnimator.SetInteger("GoFoward", 0);
            }
        }
        else
        {
            _moveSpeed = _beginMoveSpeed;
            _playerAnimator.SetInteger("GoBack", 0);
            _playerAnimator.SetInteger("GoFoward", 0);
            _playerAnimator.SetInteger("RunBack", 0);
            _playerAnimator.SetInteger("RunFoward", 0);
        }
    }

    public void Sprint()
    {
        _isSprinting = !_isSprinting;
        _moveSpeed *= _speedBoost;
    }
}
