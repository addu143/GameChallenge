namespace GameChallenge.Core.DBEntities
{
    public class PlayerBet : BaseEntity
    {
        public int RandomNumberByUser { get; set; }

        public int RandomNumberBySystem { get; set; }

        public double Amount { get; set; }

        public string Comment { get; set; }

        public Player Player { get; set; }
    }
}
