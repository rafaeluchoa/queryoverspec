// --------------------------------------------------------------------------------------------------------------------
// Autor: rafaeluchoa
// Data de criação: 29/08/2012 09:00:00 AM
// --------------------------------------------------------------------------------------------------------------------

namespace Naskar.QueryOverSpec
{
    using NHibernate;

    using Naskar.QueryOverSpec.Impl;

    /// <summary>
    /// Cria uma Specification que não faz nenhuma restrição da entidade.
    /// </summary>
    /// <typeparam name="TEntity">tipo da entidade.</typeparam>
    public class NoRestrictionsSpecification<TEntity> : IQueryOverSpecification, ISpecification<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Nao acrescenta nenhuma restrição.
        /// </summary>
        /// <typeparam name="TE"></typeparam>
        /// <typeparam name="TSe"></typeparam>
        /// <param name="queryOver"></param>
        public void Builder<TE, TSe>(IQueryOver<TE, TSe> queryOver)
        {
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