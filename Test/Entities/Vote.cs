namespace Naskar.QueryOverSpec.Test.Entities
{
    public class Vote : Entity
    {
        public virtual Course Course { get; set; }

        public virtual string Mail { get; set; }
    }
}
