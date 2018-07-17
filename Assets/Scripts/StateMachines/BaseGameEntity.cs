using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameEntity : MonoBehaviour {
    private int m_ID;
    private static int m_NextValidID = 0;

    void Start()
    {
        OnInitialize();
    }

    public virtual void OnInitialize()
    {
        m_ID = m_NextValidID++;
    }

    public abstract void Update(float deltaTime);
    public abstract bool HandleMessage(Telegram msg);

    public int ID
    {
        get { return m_ID; }
    }
}

public struct Telegram
{
    int Sender;
    int Receiver;
    int Msg;
    float DispatchTime;
    object ExtraInfo;
}