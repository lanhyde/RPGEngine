using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMineAndDigForNugget : State<Miner>
{
    public override void Enter(Miner entity)
    {
        if(entity.Location != Miner.LocationType.Goldmine)
        {
            Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Walking to the goldmine");
            entity.Location = Miner.LocationType.Goldmine;
        }
    }

    public override void Execute(Miner entity)
    {
        entity.AddToGoldCarried(1);
        entity.IncreasseFatigue();
        Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Picking up a nugget");

        if(entity.PocketIsFull)
        {
            entity.FSM.ChangeState(entity.GetMinerState(Miner.StateMinerType.StateVisitBankAndDepositGold));
        }
        if(entity.Thirsty())
        {
            entity.FSM.ChangeState(entity.GetMinerState(Miner.StateMinerType.StateQuenchThirst));
        }
    }

    public override void Exit(Miner entity)
    {
        Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Ah'm leaving the goldmine with mah pockets " +
            "full o' sweet gold");
    }

    public override bool OnMessage(Miner entity, Telegram telegram)
    {
        // send msg to global message handler
        return false;
    }
}

public class VisitBankAndDepositGold : State<Miner>
{
    public override void Enter(Miner entity)
    {
        if(entity.Location != Miner.LocationType.Bank)
        {
            Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Goin' to the bank. Yes siree");
            entity.Location = Miner.LocationType.Bank;
        }
    }

    public override void Execute(Miner entity)
    {
        entity.AddToWealth(entity.GoldCarried);
        entity.GoldCarried = 0;
        Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Depositing gold. Total savings now" +
                    entity.Wealth);
        if(entity.Wealth >= Miner.ComfortLevel)
        {
            Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": WooHoo! Rich enough for now. Back " +
                "home to mah li'lle lady");
            entity.FSM.ChangeState(entity.GetMinerState(Miner.StateMinerType.StateGoHomeAndSleepTilRested));
        }
        else
        {
            entity.FSM.ChangeState(entity.GetMinerState(Miner.StateMinerType.StateEnterMineAndDigForNugget));
        }
    }

    public override void Exit(Miner entity)
    {
        Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Leavin' the bank");
    }

    public override bool OnMessage(Miner entity, Telegram telegram)
    {
        // send msg to global message handler
        return false;
    }
}

public class GoHomeAndSleepTilRested : State<Miner>
{
    public override void Enter(Miner entity)
    {
        throw new System.NotImplementedException();
    }

    public override void Execute(Miner entity)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit(Miner entity)
    {
        throw new System.NotImplementedException();
    }

    public override bool OnMessage(Miner entity, Telegram telegram)
    {
        throw new System.NotImplementedException();
    }
}

public class QuenchThirst : State<Miner>
{
    public override void Enter(Miner entity)
    {
        throw new System.NotImplementedException();
    }

    public override void Execute(Miner entity)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit(Miner entity)
    {
        throw new System.NotImplementedException();
    }

    public override bool OnMessage(Miner entity, Telegram telegram)
    {
        throw new System.NotImplementedException();
    }
}

public class EatStew : State<Miner>
{
    public override void Enter(Miner entity)
    {
        throw new System.NotImplementedException();
    }

    public override void Execute(Miner entity)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit(Miner entity)
    {
        throw new System.NotImplementedException();
    }

    public override bool OnMessage(Miner entity, Telegram telegram)
    {
        throw new System.NotImplementedException();
    }
}

