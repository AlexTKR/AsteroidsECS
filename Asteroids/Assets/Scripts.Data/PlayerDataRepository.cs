namespace Scripts.Data
{
    public class PlayerDataRepository : IDataRepository<PlayerData>
    {
        public PlayerData Data { get; } = new PlayerData();
        
        public void Init()
        {
             
        }
    }
}
