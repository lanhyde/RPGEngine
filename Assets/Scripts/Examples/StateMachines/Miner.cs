using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : BaseGameEntity {
    public const int ComfortLevel = 5;
    public const int MaxNuggets = 3;
    public const int ThirstLevel = 5;
    public const int TirednessThreshold = 5;

    private StateMachine<Miner> m_StateMachine;
    public enum LocationType { Shack, Goldmine, Bank, Saloon }
    private LocationType m_Location;

    private int m_GoldCarried;
    private int m_MoneyInBank;
    private int m_Thirst;
    private int m_Fatigue;

    public LocationType Location
    {
        get
        {
            return m_Location;
        }
        set
        {
            m_Location = value;
        }
    }

    public int GoldCarried
    {
        get
        {
            return m_GoldCarried;
        }
        set
        {
            m_GoldCarried = value;
        }
    }

    public bool PocketIsFull
    {
        get { return m_GoldCarried >= MaxNuggets; }
    }

    public int Wealth
    {
        get
        {
            return m_MoneyInBank;
        }
        set
        {
            m_MoneyInBank = value;
        }
    }

    public StateMachine<Miner> FSM
    {
        get { return m_StateMachine; }
    }

    public enum StateMinerType
    {
        StateEnterMineAndDigForNugget,
        StateVisitBankAndDepositGold,
        StateGoHomeAndSleepTilRested,
        StateQuenchThirst,
        StateEatStew
    }

    private State<Miner>[] minerStates = new State<Miner>[] {
        new EnterMineAndDigForNugget(),
        new VisitBankAndDepositGold(),
        new GoHomeAndSleepTilRested(),
        new QuenchThirst(),
        new EatStew()
    };

    public override void OnInitialize()
    {
        m_Location = LocationType.Shack;
        m_GoldCarried = 0;
        m_MoneyInBank = 0;
        m_Thirst = 0;
        m_Fatigue = 0;

        m_StateMachine = new StateMachine<Miner>(this);
        m_StateMachine.ChangeState(minerStates[(int)StateMinerType.StateGoHomeAndSleepTilRested]);
        base.OnInitialize();
    }

    public State<Miner> GetMinerState(StateMinerType type)
    {
        return minerStates[(int)type];
    }
    public override bool HandleMessage(Telegram msg)
    {
        return m_StateMachine.HandleMessage(msg);
    }

    public override void Refresh(float deltaTime)
    {
        m_Thirst += 1;
        m_StateMachine.Update(deltaTime);
    }

    public void AddToGoldCarried(int val)
    {
        m_GoldCarried += val;
        if(m_GoldCarried < 0)
        {
            m_GoldCarried = 0;
        }
    }

    public void AddToWealth(int val)
    {
        m_MoneyInBank += val;
        if(m_MoneyInBank < 0)
        {
            m_MoneyInBank = 0;
        }
    }

    public bool Thirsty()
    {
        if(m_Thirst > ThirstLevel)
        {
            return true;
        }
        return false;
    }

    public bool Fatigued()
    {
        if(m_Fatigue > TirednessThreshold)
        {
            return true;
        }
        return false;
    }

    public void IncreasseFatigue()
    {
        m_Fatigue += 1;
    }

    public void DecreaseFatigue()
    {
        m_Fatigue -= 1;
    }

    public void BuyAndDrinkAWhiskey()
    {
        m_Thirst = 0;
        m_MoneyInBank -= 2;
    }
}
