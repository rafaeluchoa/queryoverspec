namespace Naskar.QueryOverSpec.Test.Mappings
{
    using FluentNHibernate.Mapping;

    public abstract class EntityMap<TEntity> : ClassMap<TEntity> where TEntity : Entity
    {
        protected EntityMap()
        {
            Id(x => x.Id)
                .GeneratedBy.Increment();
        }
    }
}
