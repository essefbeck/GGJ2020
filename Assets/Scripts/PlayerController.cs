using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float snapDistance;

    public float speed;

    private GameObject currentProp;

    private GameObject targetProp;

    enum State
    {
        Idle,
        Moving,
        Working
    }

    private State state = State.Idle;

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

    public void Update()
    {
        if (currentProp != null)
        {
            transform.position = currentProp.transform.position;
        }

        ShowWorkPrompt(state == State.Working);

        switch (state)
        {
            case State.Idle:
                // get input, draw line, select target
                break;
            case State.Moving:
                if (targetProp != null)
                {
                    Vector3 targetVec = targetProp.transform.position - transform.position;

                    if (targetVec.magnitude < snapDistance)
                    {
                        EnterState(State.Working);
                        currentProp = targetProp;
                        targetProp = null;
                        // TODO: vibrate controller, make sound
                    }
                    else
                    {
                        // TODO: rotate to point at target

                        // move towards target
                        transform.position += targetVec.normalized * speed * Time.deltaTime;
                    }
                }
                break;
            case State.Working:
                if (currentProp != null)
                {
                    Prop prop = currentProp.GetComponent<Prop>();
                    if (prop.workRemaining <= 0)
                    {
                        EnterState(State.Idle);
                    }
                    else
                    {
                        bool xPressed = Input.GetButtonDown("X");
                        if (xPressed)
                        {
                            prop.DoWork();
                        }
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
