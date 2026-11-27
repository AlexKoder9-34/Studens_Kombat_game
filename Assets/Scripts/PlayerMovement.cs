using ScriptableObjects;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, IControllable
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private float moveSpeed;
    
    private float _beginMoveSpeed;
    private float _speedBoost;
    private float _jumpHeight;
    private bool _isSprinting;
    
    private Rigidbody _playerRb;
    private Vector3 _moveDirection;
    private Animator _playerAnimator;
        
        
    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerRb = GetComponent<Rigidbody>();
        
        _speedBoost = playerData.SpeedBoost;
        _beginMoveSpeed = playerData.MoveSpeed;
        _jumpHeight = playerData.JumpHeight;
        moveSpeed = _beginMoveSpeed;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }
        
    public void Move(Vector3 direction)
    {
        _moveDirection = direction;
    }

    private void MovePlayer()
    {
        _playerRb.AddForce(_moveDirection.x * moveSpeed, 0f, 0f);
        AnimateMoving();
    }

    public void Jump()
    {
        _playerRb.AddForce(0f, _jumpHeight, 0f);
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
        {
            _playerAnimator.SetInteger("RunFoward", 0);
            _playerAnimator.SetInteger("GoFoward", 1);
        }
            
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
