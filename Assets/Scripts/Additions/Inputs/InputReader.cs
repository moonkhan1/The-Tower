using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputReader : MonoBehaviour, IInputReader
{
 private const string PLAYER_PREFS_BINDINGS = "InputKeyBindings";
    public static InputReader Instance { get; private set; }
    public Vector3 Direction {get; private set;}

    public bool isJumpPressed {get; private set;}
    public bool isPausePressed {get; private set;}
    public bool isInteractionPressed {get; private set;}
    public bool isInteractionPressedOneTime {get; private set;}
    public bool isMovingPressed{get; private set;}

    public bool isRunPressed {get; private set;}


    private PlayerInput _playerInput;
    private Inputs _inputs;
    
    int _pauseIndex;
    int _interactionOneTimeIndex;

    public enum Bindings
    {
     MoveUp,
     MoveDown,
     MoveLeft,
     MoveRight,
     Interaction,
     Sprint,
     Jump,
     Pause
    }
    private void Awake()
    {
     Instance = this;
     
     _inputs = new Inputs();
     if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
     {
      _inputs.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
     }
        
    }

    private void OnEnable()
    {
        _inputs.Player.Enable();
        _inputs.Player.Movements.started += MoveChar;
        _inputs.Player.Movements.performed += MoveChar;
        _inputs.Player.Movements.canceled += MoveChar;
        _inputs.Player.Jump.started += Jump;
        _inputs.Player.Jump.canceled += Jump;
        _inputs.Player.Run.started += Run;
        _inputs.Player.Run.canceled += Run;
        _inputs.Player.Pause.started += Pause;
        _inputs.Player.Pause.performed += Pause;
        _inputs.Player.Pause.canceled += Pause;
        _inputs.Player.Interaction.started += Interaction;
        _inputs.Player.Interaction.performed += Interaction;
        _inputs.Player.Interaction.canceled += Interaction;
        _inputs.Player.Interaction.started += InteractionOneTime;
        _inputs.Player.Interaction.performed += InteractionOneTime;
        _inputs.Player.Interaction.canceled += InteractionOneTime;
    }

    

    private void OnDisable()
    {
     _inputs.Player.Movements.started -= MoveChar;
     _inputs.Player.Movements.performed -= MoveChar;
     _inputs.Player.Movements.canceled -= MoveChar;
     _inputs.Player.Jump.started -= Jump;
     _inputs.Player.Jump.canceled -= Jump;
     _inputs.Player.Run.started -= Run;
     _inputs.Player.Run.canceled -= Run;
     _inputs.Player.Pause.started -= Pause;
     _inputs.Player.Pause.performed -= Pause;
     _inputs.Player.Pause.canceled -= Pause;
     _inputs.Player.Interaction.started -= Interaction;
     _inputs.Player.Interaction.performed -= Interaction;
     _inputs.Player.Interaction.canceled -= Interaction;
     _inputs.Player.Interaction.started -= InteractionOneTime;
     _inputs.Player.Interaction.performed -= InteractionOneTime;
     _inputs.Player.Interaction.canceled -= InteractionOneTime;
     _inputs.Dispose();
    }

    public void MoveChar(InputAction.CallbackContext context)
   {
    Vector2 oldDirection = context.ReadValue<Vector2>();
    isMovingPressed = oldDirection.x !=0 || oldDirection.y !=0;
    Direction = new Vector3(oldDirection.x, 0f, oldDirection.y);

   }

    private void Jump(InputAction.CallbackContext context)
   {
      isJumpPressed = context.ReadValueAsButton();
   }

    private void Pause(InputAction.CallbackContext context)
   {
    if (isPausePressed && context.action.triggered) return;

    StartCoroutine(WaitFrameForPause());
   }
    private void Interaction(InputAction.CallbackContext context)
    {
     isInteractionPressed = context.ReadValueAsButton();
    }
    
    private void InteractionOneTime(InputAction.CallbackContext context)
    {
     if (isInteractionPressedOneTime && context.action.triggered) return; 
     
     StartCoroutine(WaitFrameForInteractionOneTime());
    }
    private void Run(InputAction.CallbackContext context)
   {
    isRunPressed = context.ReadValueAsButton();
   }
    
    IEnumerator WaitFrameForPause()
    {
     isPausePressed = true && _pauseIndex % 2 == 0;
     yield return new WaitForEndOfFrame();
     isPausePressed = false;
     _pauseIndex ++;
    }
    IEnumerator WaitFrameForInteractionOneTime()
    {
     isInteractionPressedOneTime = true && _interactionOneTimeIndex % 2 == 0;
     yield return new WaitForEndOfFrame();
     isInteractionPressedOneTime = false;
     _interactionOneTimeIndex ++;
    }
    public string GetBindingText(Bindings bindings)
    {
     switch (bindings)
     {
      default:
      case Bindings.Interaction:
       return _inputs.Player.Interaction.bindings[0].ToDisplayString();
      case Bindings.Jump:
       return _inputs.Player.Jump.bindings[0].ToDisplayString();
      case Bindings.Pause:
       return _inputs.Player.Pause.bindings[0].ToDisplayString();
      case Bindings.Sprint:
       return _inputs.Player.Run.bindings[0].ToDisplayString();
      case Bindings.MoveUp:
       return _inputs.Player.Movements.bindings[1].ToDisplayString();
      case Bindings.MoveDown:
       return _inputs.Player.Movements.bindings[2].ToDisplayString();
      case Bindings.MoveLeft:
       return _inputs.Player.Movements.bindings[3].ToDisplayString();
      case Bindings.MoveRight:
       return _inputs.Player.Movements.bindings[4].ToDisplayString();
     }
    }
    public void RebindBinding(Bindings bindings, Action onActionRebound)
    {
     _inputs.Player.Disable();
     InputAction inputAction;
     int bindingIndex;
     switch (bindings)
     {
      default:
      case Bindings.MoveUp:
       inputAction = _inputs.Player.Movements;
       bindingIndex = 1;
       break;
      case Bindings.MoveDown:
       inputAction = _inputs.Player.Movements;
       bindingIndex = 2;
       break;
      case Bindings.MoveLeft:
       inputAction = _inputs.Player.Movements;
       bindingIndex = 3;
       break;
      case Bindings.MoveRight:
       inputAction = _inputs.Player.Movements;
       bindingIndex = 4;
       break;
      case Bindings.Interaction:
       inputAction = _inputs.Player.Interaction;
       bindingIndex = 0;
       break;
      case Bindings.Sprint:
       inputAction = _inputs.Player.Run;
       bindingIndex = 0;
       break;
      case Bindings.Jump:
       inputAction = _inputs.Player.Jump;
       bindingIndex = 0;
       break;
      case Bindings.Pause:
       inputAction = _inputs.Player.Pause;
       bindingIndex = 0;
       break;
     }
     inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
     {
      callback.Dispose();
      _inputs.Player.Enable();
      onActionRebound();

      PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS,_inputs.SaveBindingOverridesAsJson());
      PlayerPrefs.Save();
     }).Start();
    }
   
}
