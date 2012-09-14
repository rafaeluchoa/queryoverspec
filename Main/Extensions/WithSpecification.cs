// --------------------------------------------------------------------------------------------------------------------
// Autor: rafaeluchoa
// Data de criação: 14/09/2012 09:00:00 AM
// --------------------------------------------------------------------------------------------------------------------

namespace Naskar.QueryOverSpec.Extensions
{
    using System;
    using System.Linq.Expressions;

    using Naskar.QueryOverSpec.Impl;

    /// <summary>
    /// Usa duas specification usando um relacionamento.
    /// </summary>
    /// <typeparam name="TEntity">tipo da entidade</typeparam>
    /// <typeparam name="TEntityWith">subtipo da entidade</typeparam>
    public sealed class WithSpecification<TEntity, TEntityWith> : ISpecification<TEntity>
        where TEntity : Entity
        where TEntityWith : Entity
    {
        /// <summary>
        /// Lado esquerdo da expressão binária.
        /// </summary>
        private readonly ISpecification<TEntity> _root;

        /// <summary>
        /// Relação entre a TEntity e TEntityWith.
        /// </summary>
        private readonly Expression<Func<TEntity, object>> _path;

        /// <summary>
        /// Lado direito da expressão binária.
        /// </summary>
        private readonly ISpecification<TEntityWith> _with;

        /// <summary>
        /// Default constructor for AndSpecification
        /// </summary>
        /// <param name="root">entidade raiz.</param>
        /// <param name="path">path</param>
        /// <param name="with">with</param>
        public WithSpecification(
            ISpecification<TEntity> root, Expression<Func<TEntity, object>> path, ISpecification<TEntityWith> with)
        {
            _root = root;
            _path = path;
            _with = with;
        }

        /// <summary>
        /// Lado esquerdo da expressão binária.
        /// </summary>
        public ISpecification<TEntity> Root
        {
            get { return _root; }
        }

        /// <summary>
        /// Relação entre TEntity e TEntityWith.
        /// </summary>
        public Expression<Func<TEntity, object>> Path
        {
            get { return _path; }
        }

        /// <summary>
        /// Lado direito da expressão binária.
        /// </summary>
        public ISpecification<TEntityWith> With
        {
            get { return _with; }
        }

        /// <summary>
        /// Método Accept do ISpecificationVisitor.
        /// </summary>
        /// <param name="visitor">visitor.</param>
        public void Accept(ISpecificationVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}