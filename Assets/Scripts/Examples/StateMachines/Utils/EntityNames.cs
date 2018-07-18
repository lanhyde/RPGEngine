
public static class EntityNames
{
    public enum EntityName 
    {
        Miner_Bob = 0,
        Wife_Elsa
    }

    public static string GetNameOfEntity(int n)
    {
        EntityName entityName = (EntityName)n;
        switch (entityName)
        {
            case EntityName.Miner_Bob:
                return "Miner Bob";
            case EntityName.Wife_Elsa:
                return "Elsa";
            default:
                return "UNKNOWN!";
        }
    }
}
