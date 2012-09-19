// --------------------------------------------------------------------------------------------------------------------
// Autor: rafaeluchoa
// Data de criação: 7/31/2012 09:00:00 AM
// --------------------------------------------------------------------------------------------------------------------

namespace Naskar.QueryOverSpec
{
    using NHibernate.Criterion;

    /// <summary>
    /// Cria uma CriterionSpecification usando Example do Criteria.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class ByExampleSpec<TEntity> : IByExampleSpec<TEntity>
        where TEntity : class, IIdAccessor
    {
        /// <summary>
        /// Cria uma Specification que consulta uma entidade usando Query By Example.
        /// </summary>
        /// <param name="entity">uma instância da entidade.</param>
        /// <returns>
        /// uma specification que pode efetuar a consulta.
        /// </returns>
        public ISpecification<TEntity> By(TEntity entity = null)
        {
            ISpecification<TEntity> spec = new NoRestrictionsSpecification<TEntity>();
            if (entity != null)
            {
                if (entity.Id != null && entity.Id > 0)
                {
                    spec = new LambdaSpecification<TEntity>(x => x.Id == entity.Id);
                }
                else
                {
                    spec = new QueryOverSpecification<TEntity>(x => x.And(Example.Create(entity).EnableLike(MatchMode.Anywhere).IgnoreCase()));
                }
            }

            return spec;
        }
    }
}
