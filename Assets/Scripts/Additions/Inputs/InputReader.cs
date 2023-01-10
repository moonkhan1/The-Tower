using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputReader : MonoBehaviour, IInputReader
{
    public Vector3 Direction {get; private set;}

    public bool isJumpPressed {get; private set;}
    public bool isMovingPressed{get; private set;}

    public bool isRunPressed {get; private set;}

    PlayerInput _playerInput;

    private void Awake() {
        Inputs inputs = new Inputs();
        inputs.Player.Enable();
        inputs.Player.Movements.started += MoveChar;
        inputs.Player.Movements.performed += MoveChar;
        inputs.Player.Movements.canceled += MoveChar;
        inputs.Player.Jump.started += Jump;
        inputs.Player.Jump.canceled += Jump;
        inputs.Player.Run.started += Run;
        inputs.Player.Run.canceled += Run;



    }
   public void MoveChar(InputAction.CallbackContext context)
   {
    Vector2 oldDirection = context.ReadValue<Vector2>();
    isMovingPressed = oldDirection.x !=0 || oldDirection.y !=0;
    Direction = new Vector3(oldDirection.x, 0f, oldDirection.y);

   }

   public void Jump(InputAction.CallbackContext context)
   {
    isJumpPressed = context.ReadValueAsButton();
   }

   public void Run(InputAction.CallbackContext context)
   {
    isRunPressed = context.ReadValueAsButton();
   }
}
