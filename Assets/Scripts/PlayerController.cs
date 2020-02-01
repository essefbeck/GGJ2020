using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform m_pointerRoot;
    
    private Vector2 m_aimDirection;
    
    public void OnAim(InputAction.CallbackContext context)
    {
        m_aimDirection = context.ReadValue<Vector2>();
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

    void Update()
    {
        if (m_aimDirection.sqrMagnitude > 0f)
        {
            m_pointerRoot.gameObject.SetActive(true);
            m_pointerRoot.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, m_aimDirection));
        }
        else
        {
            m_pointerRoot.gameObject.SetActive(false);
        }
    }
}
