using ScriptableObjects;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IControllable
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private float verticalVelocity;
    [SerializeField] private float gravityForce;
    
    private float _moveSpeed;
    private float _jumpHeight;
    
    private CharacterController _controller;
    private Vector3 _moveDirection;
    private Animator _playerAnimator;
        
        
    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _controller  = GetComponent<CharacterController>();
        
        _moveSpeed = playerData.MoveSpeed;
        _jumpHeight = playerData.JumpHeight;
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
        RotatePlayer();
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
    private void RotatePlayer()
    {
        if (_moveDirection.x > 0)
        {
            _playerAnimator.Play("Walk_B");
        }
        else if (_moveDirection.x < 0)
        {
            _playerAnimator.Play("Walk_F");
        }
    }
}
