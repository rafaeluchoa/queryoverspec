// --------------------------------------------------------------------------------------------------------------------
// Autor: rafaeluchoa
// Data de criação: 7/31/2012 09:00:00 AM
// --------------------------------------------------------------------------------------------------------------------

namespace Naskar.QueryOverSpec
{
    using Naskar.QueryOverSpec.Impl;

    /// <summary>
    /// Filtro sobre uma Entidade.
    /// </summary>
    /// <typeparam name="TEntity">tipo da entidade</typeparam>
    public interface ISpecification<TEntity> 
        where TEntity : Entity
    {
        /// <summary>
        /// Método Accept do ISpecificationVisitor.
        /// </summary>
        /// <param name="visitor">visitor.</param>
        void Accept(ISpecificationVisitor visitor);
    }
}
