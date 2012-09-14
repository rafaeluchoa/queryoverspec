// --------------------------------------------------------------------------------------------------------------------
// Autor: rafaeluchoa
// Data de criação: 7/31/2012 09:00:00 AM
// --------------------------------------------------------------------------------------------------------------------

namespace Naskar.QueryOverSpec
{
    using System;
    using System.Linq.Expressions;

    using NHibernate;

    using Naskar.QueryOverSpec.Impl;

    /// <summary>
    /// Cria uma Specification usando Lambda.
    /// </summary>
    /// <typeparam name="TEntity">tipo da entidade.</typeparam>
    public class LambdaSpecification<TEntity> : IQueryOverSpecification, ISpecification<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Predicado filtro sobre uma entidade.
        /// </summary>
        private readonly Expression<Func<TEntity, bool>> _predicate;

        /// <summary>
        /// Cria uma LambdaSpecification usando um predicate.
        /// </summary>
        /// <param name="predicate">filtro sobre uma entidade.</param>
        public LambdaSpecification(Expression<Func<TEntity, bool>> predicate)
        {
            _predicate = predicate;
        }

        /// <summary>
        /// Executa a adição das restrições para essa specification usando um queryover.
        /// </summary>
        /// <typeparam name="TE">entidade raiz</typeparam>
        /// <typeparam name="TSe">entidade relacionada ou a mesma entidade raiz.</typeparam>
        /// <param name="queryOver">queryover usando a consulta.</param>
        public void Builder<TE, TSe>(IQueryOver<TE, TSe> queryOver)
        {
            queryOver.Where((Expression<Func<TSe, bool>>)(object)_predicate);
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