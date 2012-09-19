// --------------------------------------------------------------------------------------------------------------------
// Autor: rafaeluchoa
// Data de criação: 14/09/2012 09:00:00 AM
// --------------------------------------------------------------------------------------------------------------------

namespace Naskar.QueryOverSpec.Impl
{
    using NHibernate;

    using System;
    using System.Linq.Expressions;

    using NHibernate.Criterion;

    using Naskar.QueryOverSpec.Extensions;

    /// <summary>
    /// Adiciona cada query over para cada tipo de Specification.
    /// </summary>
    public class QueryOverSpecificationVisitor<TE> : ISpecificationVisitor 
        where TE : IIdAccessor
    {
        /// <summary>
        /// QueryOver usando internamente para adicionar as specifications.
        /// </summary>
        private readonly IQueryOver<TE, TE> _queryOver;

        /// <summary>
        /// Cria uma specification visitor usando queryover.
        /// </summary>
        /// <param name="queryOver">queryover</param>
        public QueryOverSpecificationVisitor(IQueryOver<TE, TE> queryOver)
        {
            _queryOver = queryOver;
        }

        /// <summary>
        /// Método visit para Specification.
        /// </summary>
        /// <typeparam name="TEntity">tipo da entidade.</typeparam>
        /// <param name="specification">specification</param>
        /// <returns>
        /// ICriterion
        /// </returns>
        public void Visit<TEntity>(AndSpecification<TEntity> specification) where TEntity : class, IIdAccessor
        {
            var left = QueryOver.Of<TEntity>();
            specification.Left.Accept(new QueryOverSpecificationVisitor<TEntity>(left));

            var right = QueryOver.Of<TEntity>();
            specification.Right.Accept(new QueryOverSpecificationVisitor<TEntity>(right));

            left.Select(x => x.Id);
            right.Select(x => x.Id);

            var andRestriction = Restrictions.Conjunction()
                .Add(Subqueries.WhereProperty<TEntity>(x => x.Id).In(left))
                .Add(Subqueries.WhereProperty<TEntity>(x => x.Id).In(right));

            _queryOver.Where(andRestriction);
        }

        /// <summary>
        /// Método visit para Specification.
        /// </summary>
        /// <typeparam name="TEntity">tipo da entidade.</typeparam>
        /// <param name="specification">specification</param>
        public void Visit<TEntity>(OrSpecification<TEntity> specification) where TEntity : class, IIdAccessor
        {
            var left = QueryOver.Of<TEntity>();
            specification.Left.Accept(new QueryOverSpecificationVisitor<TEntity>(left));

            var right = QueryOver.Of<TEntity>();
            specification.Right.Accept(new QueryOverSpecificationVisitor<TEntity>(right));

            left.Select(x => x.Id);
            right.Select(x => x.Id);

            var orRestriction = Restrictions.Disjunction()
                .Add(Subqueries.WhereProperty<TEntity>(x => x.Id).In(left))
                .Add(Subqueries.WhereProperty<TEntity>(x => x.Id).In(right));

            _queryOver.Where(orRestriction);
        }

        /// <summary>
        /// Método visit para Specification.
        /// </summary>
        /// <typeparam name="TEntity">tipo da entidade.</typeparam>
        /// <typeparam name="TEntityWith">tipo da entidade relacionada.</typeparam>
        /// <param name="specification">specification</param>
        public void Visit<TEntity, TEntityWith>(WithSpecification<TEntity, TEntityWith> specification) 
            where TEntity : class, IIdAccessor
            where TEntityWith : class, IIdAccessor
        {
            specification.Root.Accept(this);

            var path = specification.Path;

            // hack para converter a expression
            var expr = (Expression<Func<TE, object>>)(object)path;

            var withQuery = QueryOver.Of<TEntityWith>();
            var visitor = new QueryOverSpecificationVisitor<TEntityWith>(withQuery);
            specification.With.Accept(visitor);
            withQuery.Select(x => x.Id);

            _queryOver.WithSubquery.WhereProperty(expr).In(withQuery);
        }

        /// <summary>
        /// Método visit para Specification.
        /// </summary>
        /// <typeparam name="TEntity">tipo da entidade.</typeparam>
        /// <param name="specification">specification</param>
        public void Visit<TEntity>(IQueryOverSpecification specification) 
            where TEntity : class, IIdAccessor
        {
            specification.Builder<TE, TE>(_queryOver);
        }
    }
}
