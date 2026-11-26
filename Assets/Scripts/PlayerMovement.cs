using ScriptableObjects;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IControllable
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private float verticalVelocity;
    [SerializeField] private float gravityForce;
    [SerializeField] private float moveSpeed;
    
    private float _beginMoveSpeed;
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
        moveSpeed = _beginMoveSpeed;
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
        Vector3 movement = _moveDirection * moveSpeed;

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
        bool isMoving = _moveDirection.x != 0;

        if (!isMoving)
        {
            ResetAnimationMoving();
            return;
        }
        
        RotatePlayer();
        
        if (_isSprinting)
        {
            _playerAnimator.SetInteger("RunFoward", 1);
            _playerAnimator.SetInteger("GoFoward", 1);
        }
        else
            _playerAnimator.SetInteger("GoFoward", 1);
    }

    public void Sprint()
    {
        moveSpeed = _beginMoveSpeed * _speedBoost;
        _isSprinting = true;
    }

    public void StopSprint()
    {
        ResetAnimationMoving();
        moveSpeed = _beginMoveSpeed;
        _isSprinting = false;
    }

    private void ResetAnimationMoving()
    {
        _playerAnimator.SetInteger("GoFoward", 0);
        _playerAnimator.SetInteger("RunFoward", 0);
    }

    private void RotatePlayer()
    {
        if (_moveDirection.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (_moveDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }
}
