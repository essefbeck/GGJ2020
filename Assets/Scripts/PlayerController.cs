using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public void OnAim(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>().ToString());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                Debug.Log("Move");
                break;

            case InputActionPhase.Started:
                break;

            case InputActionPhase.Canceled:
                break;
        }
    }
}
