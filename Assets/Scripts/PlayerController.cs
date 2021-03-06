﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Haptics;
using UnityEngine.InputSystem.Users;

public class PlayerController : MonoBehaviour
{
    public float snapDistance;

    public float speed;

    public AudioSource moveSound;
    
    private Prop currentProp;

    enum State
    {
        Idle,
        Moving,
        Working
    }

    private State state = State.Idle;

    [SerializeField] private Transform m_pointerRoot;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private GameObject m_workPrompt;
    [SerializeField] private SpriteRenderer m_sprite;
    
    private Vector2 m_aimDirection;
    private RaycastHit2D[] m_RaycastResults = new RaycastHit2D[1];
    private int m_RaycastLayerMask;
    private Prop m_CurrentTarget;
    private bool canPlaySound = true;
    private static readonly int Idle = Animator.StringToHash("idle");
    private static readonly int Moving = Animator.StringToHash("moving");
    private static readonly int Work = Animator.StringToHash("work");
    private static readonly int Aiming = Animator.StringToHash("aiming");

    public int playerId;

    void Awake()
    {
        m_RaycastLayerMask = LayerMask.GetMask("Targetable");
    }
    
    public void OnAim(InputAction.CallbackContext context)
    {
        if (Time.timeScale < 1f)
            return;
        
        m_aimDirection = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (Time.timeScale < 1f)
            return;
        
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                if (m_CurrentTarget != null)
                {
                    m_pointerRoot.gameObject.SetActive(false);
                    EnterState(State.Moving);
                    if (canPlaySound)
                    {
                        moveSound.Play();
                        canPlaySound = false;
                    }
                    
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
        if (Time.timeScale < 1f)
            return;
        
        if (context.phase == InputActionPhase.Performed)
        {
            if (currentProp != null && currentProp.workRemaining > 0)
            {
                m_Animator.SetTrigger(Work);
                currentProp.DoWork();
            }
        }
    }

    public void Update()
    {
        switch (state)
        {
            case State.Idle:
                m_sprite.transform.rotation = new Quaternion();
                m_sprite.flipX = false;

                if (currentProp != null)
                {
                    transform.position = currentProp.transform.position + currentProp.offset;
                }

                if (m_aimDirection.sqrMagnitude > 0f)
                {
                    m_Animator.SetBool(Aiming, true);
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
                                m_CurrentTarget.Untarget(playerId);
                            }

                            target.Target(playerId);
                            m_CurrentTarget = target;
                        }
                    }
                    else
                    {
                        if (m_CurrentTarget != null)
                        {
                            m_CurrentTarget.Untarget(playerId);
                            m_CurrentTarget = null;
                        }
                    }
                }
                else
                {
                    m_Animator.SetBool(Aiming, false);
                    m_pointerRoot.gameObject.SetActive(false);
                    
                    if (m_CurrentTarget != null)
                    {
                        m_CurrentTarget.Untarget(playerId);
                        m_CurrentTarget = null;
                    }
                }
                break;
            case State.Moving:
                if (m_CurrentTarget != null)
                {
                    Vector3 targetVec = (m_CurrentTarget.transform.position +  m_CurrentTarget.offset) - transform.position;

                    if (targetVec.magnitude < snapDistance)
                    {
                        EnterState(State.Working);
                        currentProp = m_CurrentTarget;
                        m_CurrentTarget.BeginWork(playerId);
                        m_CurrentTarget = null;
                        // TODO: vibrate controller, make sound
                    }
                    else
                    {
                        // TODO: rotate to point at target
                        //Vector3 lookVec = new Vector3(targetVec.x, targetVec.y, 0);
                        //m_sprite.rotation = Quaternion.LookRotation(lookVec, Vector3.back);

                        float angle = Vector3.Angle(new Vector3(0,1,0), new Vector3(targetVec.x, targetVec.y, 0));
                        if (targetVec.x < 0)
                        {
                            m_sprite.flipX = true;
                            m_sprite.transform.eulerAngles = new Vector3(0, 0, angle - 45);
                        }
                        else
                        {
                            m_sprite.transform.eulerAngles = new Vector3(0, 0, -angle + 45);
                        }

                        // move towards target
                        transform.position += speed * Time.deltaTime * targetVec.normalized;
                    }
                }
                break;
            case State.Working:
                m_sprite.transform.rotation = new Quaternion();
                m_sprite.flipX = false;

                if (currentProp != null)
                {
                    transform.position = currentProp.transform.position + currentProp.offset;
                    if (currentProp.workRemaining <= 0)
                    {
                        EnterState(State.Idle);
                    }
                }
                break;
        }
        
        m_workPrompt.SetActive(state == State.Working);
    }

    void EnterState(State newState)
    {
        ExitState(state);

        state = newState;
        switch (state)
        {
            case State.Idle:
                m_Animator.SetBool(Idle, true);
                break;
            case State.Moving:
                m_Animator.SetBool(Moving, true);
                break;
            case State.Working:
                canPlaySound = true;
                break;
        }
    }

    void ExitState(State oldState)
    {
        switch (oldState)
        {
            case State.Idle:
                m_Animator.SetBool(Idle, false);
                m_Animator.SetBool(Aiming, false);
                break;
            case State.Moving:
                m_Animator.SetBool(Moving, false);
                break;
            case State.Working:
                m_Animator.ResetTrigger(Work);
                break;
        }
    }
}
