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
        Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Depositing gold. Total savings now " +
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
        if(entity.Location != Miner.LocationType.Shack)
        {
            Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Walin' home");
            entity.Location = Miner.LocationType.Shack;

            MessageDispatcher.Instance.DispatchMessage(MessageDispatcher.SEND_MSG_IMMEDIATELY,
                                                        entity.ID,
                                                        (int)EntityNames.EntityName.Wife_Elsa,
                                                        (int)MessageReader.MessageType.Msg_HiHoneyImHome,
                                                        MessageDispatcher.NO_ADDITIONAL_INFO);
        }
    }

    public override void Execute(Miner entity)
    {
        if(!entity.Fatigued())
        {
            Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": All mah fatigue has drained away. " +
                "Time to find more gold!");
            entity.FSM.ChangeState(entity.GetMinerState(Miner.StateMinerType.StateEnterMineAndDigForNugget));
        }
        else
        {
            entity.DecreaseFatigue();
            Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": ZZZZ...");
        }
    }

    public override void Exit(Miner entity)
    {
        
    }

    public override bool OnMessage(Miner entity, Telegram telegram)
    {
        MessageReader.MessageType msg = (MessageReader.MessageType)telegram.Msg;
        switch (msg)
        {
            case MessageReader.MessageType.Msg_StewReady:
                Debug.Log("Message handled by " + EntityNames.GetNameOfEntity(entity.ID) +
                    " at time: " + Time.time);
                Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Okay, Hun, ahm a comin'!");
                entity.FSM.ChangeState(entity.GetMinerState(Miner.StateMinerType.StateEatStew));
                return true;
        }
        return false;
    }
}

public class QuenchThirst : State<Miner>
{
    public override void Enter(Miner entity)
    {
        if(entity.Location != Miner.LocationType.Saloon)
        {
            entity.Location = Miner.LocationType.Saloon;
            Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Boy, ah sure is thusty! Waling to the saloon");
        }
    }

    public override void Execute(Miner entity)
    {
        entity.BuyAndDrinkAWhiskey();
        Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": That's mighty fine sippin' liquer");
        entity.FSM.ChangeState(entity.GetMinerState(Miner.StateMinerType.StateEnterMineAndDigForNugget));
    }

    public override void Exit(Miner entity)
    {
        Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Leaving the saloon, feelin' good");
    }

    public override bool OnMessage(Miner entity, Telegram telegram)
    {
        return false;
    }
}

public class EatStew : State<Miner>
{
    public override void Enter(Miner entity)
    {
        Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Smells real good elsa!");
    }

    public override void Execute(Miner entity)
    {
        Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Tates real good too!");
        entity.FSM.RevertToPreviousState();
    }

    public override void Exit(Miner entity)
    {
        Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Thankya li'lle lady. Ah better get back to whatever ah wuz doin'");
    }

    public override bool OnMessage(Miner entity, Telegram telegram)
    {
        return false;
    }
}

