// --------------------------------------------------------------------------------------------------------------------
// Autor: rafaeluchoa
// Data de criação: 7/31/2012 09:00:00 AM
// --------------------------------------------------------------------------------------------------------------------

namespace Naskar.QueryOverSpec.Extensions
{
    using Naskar.QueryOverSpec.Impl;

    /// <summary>
    /// Usa duas specification usando o operador OR.
    /// </summary>
    /// <typeparam name="TEntity">tipo da entidade</typeparam>
    public sealed class OrSpecification<TEntity> : BinaryCompositeSpecification<TEntity>
        where TEntity : class, IIdAccessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrSpecification&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        public OrSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
            : base(left, right)
        {
        }

        /// <summary>
        /// Método Accept do ISpecificationVisitor.
        /// </summary>
        /// <param name="visitor">visitor.</param>
        public override void Accept(ISpecificationVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}