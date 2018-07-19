using Pattern.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDispatcher : Singleton<MessageDispatcher>
{
    public const float SEND_MSG_IMMEDIATELY = 0.0f;
    public const object NO_ADDITIONAL_INFO = null;

    private HashSet<Telegram> priorityQueue = new HashSet<Telegram>();

    private void Discharge(BaseGameEntity receiver, Telegram msg)
    {
        if(!receiver.HandleMessage(msg))
        {
            Debug.Log("Message not handled");
        }
    }

    public void DispatchMessage(float delay, int sender, int receiver, int msg, object extraInfo)
    {
        BaseGameEntity senderEntity = EntityManager.Instance.GetEntityFromID(sender);
        BaseGameEntity receiverEntity = EntityManager.Instance.GetEntityFromID(receiver);

        if(receiverEntity == null)
        {
            Debug.LogWarning("No Receiver with ID of " + receiver + " found");
            return;
        }

        Telegram telegram = new Telegram
        {
            DispatchTime = 0,
            Sender = sender,
            Receiver = receiver,
            Msg = msg,
            ExtraInfo = extraInfo
        };

        if(delay <= 0.0f)
        {
            Debug.Log("Instant telegram dispatched at time " + Time.time +
                " by " + EntityNames.GetNameOfEntity(senderEntity.ID) + " for " +
                EntityNames.GetNameOfEntity(receiverEntity.ID) + ". Msg is " + MessageReader.GetReadableMessage(msg));
            Discharge(receiverEntity, telegram);
        }
        else
        {
            float currentTime = Time.time;
            telegram.DispatchTime = currentTime + delay;
            priorityQueue.Add(telegram);

            Debug.Log("Delayed telegram from " + EntityNames.GetNameOfEntity(senderEntity.ID) + " recorded " +
                "at time " + Time.time + " for " + EntityNames.GetNameOfEntity(receiverEntity.ID) +
                ". Msg is " + MessageReader.GetReadableMessage(msg));
        }
    }

    public void DispatchDelayedMessage()
    {
        float currentTime = Time.time;

        var enumerator = priorityQueue.GetEnumerator();
        while(priorityQueue.Count > 0 && enumerator.MoveNext() &&
            enumerator.Current.DispatchTime > 0)
        {
            Telegram telegram = enumerator.Current;
            BaseGameEntity receiverEntity = EntityManager.Instance.GetEntityFromID(telegram.Receiver);
            Debug.Log("Queued telegram ready for dispatch: Sent to " + EntityNames.GetNameOfEntity(receiverEntity.ID) +
                        ". Msg is " + MessageReader.GetReadableMessage(telegram.Msg));
            Discharge(receiverEntity, telegram);
            priorityQueue.Remove(telegram);
        }
    }
}
