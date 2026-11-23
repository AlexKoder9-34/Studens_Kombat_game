using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IControllable
{
    [SerializeField] private float verticalVelocity;
    [SerializeField] private float gravityForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
        
    private CharacterController _controller;
    private Vector3 _moveDirection;
        
        
    private void Awake()
    {
        _controller  = GetComponent<CharacterController>();
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
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2 * gravityForce);
        }
    }
    private void RotatePlayer()
    {
        if (_moveDirection.x > 0)
        {
            transform.rotation = Quaternion.Euler(0,90,0);
        }
        else if (_moveDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(0,-90,0);
        }
    }
}
