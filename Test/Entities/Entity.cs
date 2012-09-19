namespace Naskar.QueryOverSpec.Test.Entities
{
    public abstract class Entity : IIdAccessor
    {
        public virtual long? Id { get; set; }
    }
}
