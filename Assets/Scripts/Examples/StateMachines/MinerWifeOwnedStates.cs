using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WifesGlobalState : State<MinerWife>
{
    public override void Enter(MinerWife entity)
    {
        
    }

    public override void Execute(MinerWife entity)
    {
        if (Random.value < 0.1 && !entity.FSM.IsInState(entity.GetMinerWifeState(MinerWife.StateMinerWifeType.StateVisitBathroom)))
        {
            entity.FSM.ChangeState(entity.GetMinerWifeState(MinerWife.StateMinerWifeType.StateVisitBathroom));
        }
    }

    public override void Exit(MinerWife entity)
    {
        
    }

    public override bool OnMessage(MinerWife entity, Telegram telegram)
    {
        MessageReader.MessageType msg = (MessageReader.MessageType)telegram.Msg;

        switch (msg)
        {
            case MessageReader.MessageType.Msg_HiHoneyImHome:
                Debug.Log("Message handled by " + EntityNames.GetNameOfEntity(entity.ID) + " at time: " +
                     Time.time);
                Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Hi honey. Let me make you some of mah fine " +
                    "country stew");
                entity.FSM.ChangeState(entity.GetMinerWifeState(MinerWife.StateMinerWifeType.StateCookStew));
                return true;
        }
        return false;

    }
}

public class DoHouseWork : State<MinerWife>
{
    public override void Enter(MinerWife entity)
    {
        Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Time to do some more housework!");
    }

    public override void Execute(MinerWife entity)
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Moppin' the floor");
                break;
            case 1:
                Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Washin' the dishes");
                break;
            case 2:
                Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Makin' the bed");
                break;
        }
    }

    public override void Exit(MinerWife entity)
    {

    }

    public override bool OnMessage(MinerWife entity, Telegram telegram)
    {
        return false;
    }
}

public class VisitBathroom : State<MinerWife>
{
    public override void Enter(MinerWife entity)
    {
        Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Walin' to the can. Need to powda mah pretty li'lle nose");
    }

    public override void Execute(MinerWife entity)
    {
        Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Ahhhhhh! Sweet relief!");
        entity.FSM.RevertToPreviousState();
    }

    public override void Exit(MinerWife entity)
    {
        Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Leavin' the Jon");
    }

    public override bool OnMessage(MinerWife entity, Telegram telegram)
    {
        return false;
    }
}

public class CookStew : State<MinerWife>
{
    public override void Enter(MinerWife entity)
    {
        if(!entity.Cooking)
        {
            Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Putting the stew in the oven");
            MessageDispatcher.Instance.DispatchMessage(1.5f, entity.ID, entity.ID, (int)MessageReader.MessageType.Msg_StewReady ,MessageDispatcher.NO_ADDITIONAL_INFO);
            entity.Cooking = true;
        }
    }

    public override void Execute(MinerWife entity)
    {
        Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Fussin' over food");
    }

    public override void Exit(MinerWife entity)
    {
        Debug.Log(EntityNames.GetNameOfEntity(entity.ID) + ": Puttin' the stew on the table");
    }

    public override bool OnMessage(MinerWife entity, Telegram telegram)
    {
        MessageReader.MessageType msg = (MessageReader.MessageType)telegram.Msg;

        switch (msg)
        {
            case MessageReader.MessageType.Msg_StewReady:
                Debug.Log("Message received by " + EntityNames.GetNameOfEntity(entity.ID) + " at time: " +
                    Time.time);
                MessageDispatcher.Instance.DispatchMessage(MessageDispatcher.SEND_MSG_IMMEDIATELY,
                                                            entity.ID,
                                                            (int)EntityNames.EntityName.Miner_Bob,
                                                            (int)MessageReader.MessageType.Msg_StewReady,
                                                            MessageDispatcher.NO_ADDITIONAL_INFO);
                entity.Cooking = false;
                entity.FSM.ChangeState(entity.GetMinerWifeState(MinerWife.StateMinerWifeType.StateDoHousework));
                return true;
        }
        return false;
    }
}