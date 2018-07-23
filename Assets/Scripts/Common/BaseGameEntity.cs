using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameEntity : MonoBehaviour {
    private int m_ID;
    private static int m_NextValidID = 0;

    protected virtual void Start()
    {
        OnInitialize();
    }

    public virtual void OnInitialize()
    {
        m_ID = m_NextValidID++;
    }

    public abstract void Refresh(float deltaTime);
    public abstract bool HandleMessage(Telegram msg);

    public int ID
    {
        get { return m_ID; }
    }
}

public class Telegram
{
    public const float SmallestDelay = 0.25f;

    public int Sender;
    public int Receiver;
    public int Msg;
    public float DispatchTime;
    public object ExtraInfo;

    public Telegram()
    {
        DispatchTime = -1;
        Sender = -1;
        Receiver = -1;
        Msg = -1;
        ExtraInfo = null;
    }

    public Telegram(float time, int sender, int receiver, int msg, object info = null)
    {
        DispatchTime = time;
        Sender = sender;
        Receiver = receiver;
        Msg = msg;
        ExtraInfo = info;
    }

    public static bool operator==(Telegram t1, Telegram t2)
    {
        return (Mathf.Abs(t1.DispatchTime - t2.DispatchTime) < SmallestDelay) &&
               (t1.Sender == t2.Sender) &&
               (t1.Receiver == t2.Receiver) &&
               (t1.Msg == t2.Msg);
    }

    public static bool operator!=(Telegram t1, Telegram t2)
    {
        return !(t1 == t2);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return this == (Telegram)obj;
    }
}