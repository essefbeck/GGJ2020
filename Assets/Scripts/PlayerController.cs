using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Haptics;
using UnityEngine.InputSystem.Users;

public class PlayerController : MonoBehaviour
{
    public float snapDistance;

    public float speed;

    private Prop currentProp;

    enum State
    {
        Idle,
        Moving,
        Working
    }

    private State state = State.Idle;

    [SerializeField] private Transform m_pointerRoot;
    [SerializeField] private PlayerInput m_PlayerInput;
    
    private Vector2 m_aimDirection;
    private RaycastHit2D[] m_RaycastResults = new RaycastHit2D[1];
    private int m_RaycastLayerMask;
    private Prop m_CurrentTarget;

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
                    m_pointerRoot.gameObject.SetActive(false);
                    EnterState(State.Moving);
                }

                break;

            case InputActionPhase.Started:
                break;

            case InputActionPhase.Canceled:
                break;
        }
    }

    public void OnWork(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (currentProp != null && currentProp.workRemaining > 0)
            {
                currentProp.DoWork();
            }
        }
    }

    public void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (currentProp != null)
                {
                    transform.position = currentProp.transform.position;
                }

                if (m_aimDirection.sqrMagnitude > 0f)
                {
                    m_pointerRoot.gameObject.SetActive(true);
                    m_pointerRoot.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, m_aimDirection));

                    if (Physics2D.RaycastNonAlloc(m_pointerRoot.position, m_aimDirection, m_RaycastResults, 100f, m_RaycastLayerMask) > 0)
                    {
                        var hit = m_RaycastResults[0];
                
                        var target = hit.transform.GetComponent<Prop>();

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
                break;
            case State.Moving:
                if (m_CurrentTarget != null)
                {
                    Vector3 targetVec = m_CurrentTarget.transform.position - transform.position;

                    if (targetVec.magnitude < snapDistance)
                    {
                        EnterState(State.Working);
                        currentProp = m_CurrentTarget;
                        m_CurrentTarget.BeginWork();
                        m_CurrentTarget = null;
                        // TODO: vibrate controller, make sound
                    }
                    else
                    {
                        // TODO: rotate to point at target

                        // move towards target
                        transform.position += speed * Time.deltaTime * targetVec.normalized;
                    }
                }
                break;
            case State.Working:
                if (currentProp != null)
                {
                    transform.position = currentProp.transform.position;
                    if (currentProp.workRemaining <= 0)
                    {
                        EnterState(State.Idle);
                    }
                }
                break;
        }
    }

    void EnterState(State newState)
    {
        ExitState(state);

        state = newState;
        switch (state)
        {
            case State.Idle:
                // set anim
                break;
            case State.Moving:
                // set anim
                break;
            case State.Working:
                // set anim
                break;
        }
    }

    void ExitState(State newState)
    {
        
    }

    void ShowWorkPrompt(bool show)
    {

    }
}
