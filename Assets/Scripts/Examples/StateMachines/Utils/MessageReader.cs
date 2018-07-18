using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MessageReader
{
    public enum MessageType
    {
        Msg_HiHoneyImHome,
        Msg_StewReady
    }
    public static string GetReadableMessage(int msg)
    {
        MessageType msgType = (MessageType)msg;
        switch(msgType)
        {
            case MessageType.Msg_HiHoneyImHome:
                return "HiHoneyImHome";
            case MessageType.Msg_StewReady:
                return "StewReady";
            default:
                return "Not recognized!";
        }
    }
}
