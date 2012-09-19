// --------------------------------------------------------------------------------------------------------------------
// Autor: rafaeluchoa
// Data de criação: 14/09/2012 09:00:00 AM
// --------------------------------------------------------------------------------------------------------------------

namespace Naskar.QueryOverSpec
{
    using System;

    using NHibernate;

    using Naskar.QueryOverSpec.Impl;

    /// <summary>
    /// Customização usando uma action sobre um queryover.
    /// </summary>
    /// <typeparam name="TEntity">tipo da entidade</typeparam>
    public class QueryOverSpecification<TEntity> : IQueryOverSpecification, ISpecification<TEntity>
        where TEntity : class, IIdAccessor
    {
        /// <summary>
        /// A action a ser executada sobre o queryover.
        /// </summary>
        private readonly Action<IQueryOver<TEntity, TEntity>> _action;

        /// <summary>
        /// Cria uma specificatio usando uma action sobre um queryover.
        /// </summary>
        /// <param name="action"></param>
        public QueryOverSpecification(Action<IQueryOver<TEntity, TEntity>> action)
        {
            _action = action;
        }

        /// <summary>
        /// Executa a adição das restrições para essa specification usando um queryover.
        /// </summary>
        /// <typeparam name="TE">entidade raiz</typeparam>
        /// <typeparam name="TSe">entidade relacionada ou a mesma entidade raiz.</typeparam>
        /// <param name="queryOver">queryover usando a consulta.</param>
        public void Builder<TE, TSe>(IQueryOver<TE, TSe> queryOver)
        {
            _action((IQueryOver<TEntity, TEntity>)queryOver);
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
