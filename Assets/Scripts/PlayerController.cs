using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform m_pointerRoot;
    [SerializeField] private PlayerInput m_PlayerInput;
    
    private Vector2 m_aimDirection;
    private RaycastHit2D[] m_RaycastResults = new RaycastHit2D[1];
    private int m_RaycastLayerMask;
    private Targetable m_CurrentTarget;

    void Awake()
    {
        m_RaycastLayerMask = LayerMask.GetMask("Targetable");
    }
    
    public void OnAim(InputAction.CallbackContext context)
    {
        m_aimDirection = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                if (m_CurrentTarget != null)
                {
                    Debug.Log("Move");
                }

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

            if (Physics2D.RaycastNonAlloc(m_pointerRoot.position, m_aimDirection, m_RaycastResults, 100f, m_RaycastLayerMask) > 0)
            {
                var hit = m_RaycastResults[0];
                
                var target =hit.transform.GetComponent<Targetable>();

                if (target != m_CurrentTarget)
                {
                    if (m_CurrentTarget != null)
                    {
                        m_CurrentTarget.Untarget();
                    }

                    target.Target();
                    m_CurrentTarget = target;
                }
            }
            else
            {
                if (m_CurrentTarget != null)
                {
                    m_CurrentTarget.Untarget();
                    m_CurrentTarget = null;
                }
            }
        }
        else
        {
            m_pointerRoot.gameObject.SetActive(false);
            if (m_CurrentTarget != null)
            {
                m_CurrentTarget.Untarget();
                m_CurrentTarget = null;
            }
        }
    }
}
