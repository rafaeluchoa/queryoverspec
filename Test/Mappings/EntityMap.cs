namespace Naskar.QueryOverSpec.Test.Mappings
{
    using FluentNHibernate.Mapping;

    public abstract class EntityMap<TEntity> : ClassMap<TEntity> where TEntity : class, IIdAccessor
    {
        protected EntityMap()
        {
            Id(x => x.Id)
                .GeneratedBy.Increment();
        }
    }
}
