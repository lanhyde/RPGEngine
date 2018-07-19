using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerWife : BaseGameEntity
{
    private StateMachine<MinerWife> m_StateMachine;

    private Miner.LocationType m_Location;

    private bool m_Cooking;


    public enum StateMinerWifeType
    {
        StateWifesGlobalState,
        StateDoHousework,
        StateVisitBathroom,
        StateCookStew,
    }

    private State<MinerWife>[] minerStates = new State<MinerWife>[] {
        new WifesGlobalState(),
        new DoHouseWork(),
        new VisitBathroom(),
        new CookStew(),
    };

    public StateMachine<MinerWife> FSM
    {
        get { return m_StateMachine; }
    }

    public Miner.LocationType Location
    {
        get { return m_Location; }
        set { m_Location = value; }
    }

    public bool Cooking
    {
        get { return m_Cooking; }
        set { m_Cooking = value; }
    }

    public override void OnInitialize()
    {
        m_Location = Miner.LocationType.Shack;
        m_Cooking = false;
        m_StateMachine = new StateMachine<MinerWife>(this);
        m_StateMachine.CurrentState = minerStates[(int)StateMinerWifeType.StateDoHousework];
        m_StateMachine.GlobalState = minerStates[(int)StateMinerWifeType.StateWifesGlobalState];
        base.OnInitialize();
    }

    public State<MinerWife> GetMinerWifeState(StateMinerWifeType type)
    {
        return minerStates[(int)type];
    }

    public override void Refresh(float deltaTime)
    {
        m_StateMachine.Update(deltaTime);
    }

    public override bool HandleMessage(Telegram msg)
    {
        return m_StateMachine.HandleMessage(msg);
    }
}
