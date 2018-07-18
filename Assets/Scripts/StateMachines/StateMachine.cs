using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> {
    private T m_Owner;
    private State<T> m_CurrentState;
    private State<T> m_PreviousState;
    private State<T> m_GlobalState;

    public StateMachine(T owner)
    {
        m_Owner = owner;
        m_CurrentState = null;
        m_PreviousState = null;
        m_GlobalState = null;
    }

    public State<T> CurrentState
    {
        set
        {
            m_CurrentState = value;
        }
        get
        {
            return m_CurrentState;
        }
    }

    public State<T> GlobalState
    {
        set
        {
            m_GlobalState = value;
        }
        get
        {
            return m_GlobalState;
        }
    }

    public State<T> PreviousState
    {
        set
        {
            m_PreviousState = value;
        }
        get
        {
            return m_PreviousState;
        }
    }

    public void Update(float deltaTime)
    {
        if(m_GlobalState != null)
        {
            m_GlobalState.Execute(m_Owner);
        }
        if(m_CurrentState != null)
        {
            m_CurrentState.Execute(m_Owner);
        }
    }

    public bool HandleMessage(Telegram msg)
    {
        if(m_CurrentState != null && m_CurrentState.OnMessage(m_Owner, msg))
        {
            return true;
        }
        if(m_GlobalState != null && m_GlobalState.OnMessage(m_Owner, msg))
        {
            return true;
        }
        return false;
    }

    public void ChangeState(State<T> newState)
    {
        m_PreviousState = m_CurrentState;
        m_CurrentState.Exit(m_Owner);
        m_CurrentState = newState;
        m_CurrentState.Enter(m_Owner);
    }

    public void RevertToPreviousState()
    {
        ChangeState(m_PreviousState);
    }

    public bool IsInState(State<T> s)
    {
        if (m_CurrentState.GetType() == s.GetType())
            return true;
        return false;
    }

    public string GetNameOfCurrentState()
    {
        return m_CurrentState.GetType().Name;
    }
}
