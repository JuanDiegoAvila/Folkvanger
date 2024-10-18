namespace Assets.Scripts
{
    public enum PawnState { Idle, Walking, Chopping, Building, Resting }
    public enum ResourceType { Wood, Gold, Food }

    public class Resource
    {
        public ResourceType resourceType;
        public int amount;
    }
}