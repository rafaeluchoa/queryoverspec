// --------------------------------------------------------------------------------------------------------------------
// Autor: rafaeluchoa
// Data de criação: 7/31/2012 09:00:00 AM
// --------------------------------------------------------------------------------------------------------------------

namespace Naskar.QueryOverSpec.Extensions
{
    using Naskar.QueryOverSpec.Impl;

    /// <summary>
    /// Base class for composite specifications
    /// </summary>
    /// <typeparam name="TEntity">Type of entity that check this specification</typeparam>
    public abstract class BinaryCompositeSpecification<TEntity> : ISpecification<TEntity>
         where TEntity : class, IIdAccessor
    {
        /// <summary>
        /// Lado esquerdo da expressão binária.
        /// </summary>
        private readonly ISpecification<TEntity> _left;

        /// <summary>
        /// Lado direito da expressão binária.
        /// </summary>
        private readonly ISpecification<TEntity> _right;

        /// <summary>
        /// Default constructor for AndSpecification
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        protected BinaryCompositeSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
        {
            _left = left;
            _right = right;
        }

        /// <summary>
        /// Lado esquerdo da expressão binária.
        /// </summary>
        public ISpecification<TEntity> Left
        {
            get { return _left; }
        }

        /// <summary>
        /// Lado direito da expressão binária.
        /// </summary>
        public ISpecification<TEntity> Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Método Accept do ISpecificationVisitor.
        /// </summary>
        /// <param name="visitor">visitor.</param>
        public abstract void Accept(ISpecificationVisitor visitor);
    }
}