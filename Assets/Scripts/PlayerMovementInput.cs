using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInput : MonoBehaviour
{
    private GameplayInputActions _gameplayInputActions;
    private IControllable _controllable;

    private void Awake()
    {
        _gameplayInputActions = new GameplayInputActions();
        _controllable = GetComponent<IControllable>();
    }

    private void ReadMove(InputAction.CallbackContext context)
    {
        var moveInput = _gameplayInputActions.Player.Move.ReadValue<Vector2>();
        Vector3 controlDirection = new Vector3(-moveInput.x, 0, 0);
        _controllable.Move(controlDirection);
    }

    private void ReadJump(InputAction.CallbackContext context)
    {
        _controllable.Jump();
    }
    
    private void OnEnable()
    {
        _gameplayInputActions.Player.Jump.performed += ReadJump;
        _gameplayInputActions.Player.Jump.canceled += ReadJump;
        _gameplayInputActions.Player.Move.performed += ReadMove;
        _gameplayInputActions.Player.Move.canceled += ReadMove;
        _gameplayInputActions.Enable();
    }

    private void OnDisable()
    {
        _gameplayInputActions.Player.Jump.performed -= ReadJump;
        _gameplayInputActions.Player.Jump.canceled -= ReadJump;
        _gameplayInputActions.Player.Move.performed -= ReadMove;
        _gameplayInputActions.Player.Move.canceled -= ReadMove;
        _gameplayInputActions.Disable();
    }
}
