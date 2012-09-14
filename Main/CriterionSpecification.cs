// --------------------------------------------------------------------------------------------------------------------
// Autor: rafaeluchoa
// Data de criação: 7/31/2012 09:00:00 AM
// --------------------------------------------------------------------------------------------------------------------

namespace Naskar.QueryOverSpec
{
    using NHibernate;
    using NHibernate.Criterion;

    using Naskar.QueryOverSpec.Impl;

    /// <summary>
    /// Cria uma Specification usando ICriterion.
    /// </summary>
    /// <typeparam name="TEntity">tipo da entidade.</typeparam>
    public class CriterionSpecification<TEntity> : IQueryOverSpecification, ISpecification<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Predicado filtro sobre uma entidade.
        /// </summary>
        private readonly ICriterion _predicateCriterion;

        /// <summary>
        /// Cria uma CriterionSpecification usando um predicado ICriterion.
        /// </summary>
        /// <param name="predicateCriterion">filtro sobre uma entidade.</param>
        public CriterionSpecification(ICriterion predicateCriterion)
        {
            _predicateCriterion = predicateCriterion;
        }

        public void Builder<TE, TSe>(IQueryOver<TE, TSe> queryOver)
        {
            queryOver.And(_predicateCriterion);
        }

        /// <summary>
        /// Método Accept do ISpecificationVisitor.
        /// </summary>
        /// <param name="visitor">visitor.</param>
        public void Accept(ISpecificationVisitor visitor)
        {
            visitor.Visit<TEntity>(this);
        }
    }
}
