using Pattern.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EntityManager : Singleton<EntityManager> {

    private HashSet<BaseGameEntity> m_EntityMap = new HashSet<BaseGameEntity>();

    public BaseGameEntity GetEntityFromID(int id)
    {
        return m_EntityMap.FirstOrDefault(b => b.ID == id);
    }

    public void RegisterEntity(BaseGameEntity newEntity)
    {
        m_EntityMap.Add(newEntity);
    }

    public void RemoveEntity(BaseGameEntity pEntity)
    {
        m_EntityMap.Remove(pEntity);
    }
}
