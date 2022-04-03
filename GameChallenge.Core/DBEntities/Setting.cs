namespace GameChallenge.Core.DBEntities
{
    public class Setting : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
