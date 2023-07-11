using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private UIManager uIManager;
    private PlayerInput playerInput;

    private InputAction touchPressAction;
    private InputAction touchPositionAction;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        uIManager = GetComponent<UIManager>();

        touchPressAction = playerInput.actions["TouchPress"];
        touchPositionAction = playerInput.actions["TouchPosition"];
    }

    private void OnEnable()
    {
        touchPressAction.performed += TouchPressed;
    }

    private void OnDisable()
    {
        touchPressAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext ctx)
    {
        uIManager.UITouched(touchPositionAction.ReadValue<Vector2>());
    }
}
