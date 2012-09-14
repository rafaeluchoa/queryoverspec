// --------------------------------------------------------------------------------------------------------------------
// Autor: rafaeluchoa
// Data de criação: 7/31/2012 09:00:00 AM
// --------------------------------------------------------------------------------------------------------------------

namespace Naskar.QueryOverSpec.Impl
{
    using Naskar.QueryOverSpec.Extensions;

    /// <summary>
    /// Visitor para cada tipo de Specification.
    /// </summary>
    public interface ISpecificationVisitor 
    {
        /// <summary>
        /// Método visit para Specification.
        /// </summary>
        /// <typeparam name="TEntity">tipo da entidade.</typeparam>
        /// <param name="specification">specification</param>
        void Visit<TEntity>(AndSpecification<TEntity> specification) where TEntity : Entity;

        /// <summary>
        /// Método visit para Specification.
        /// </summary>
        /// <typeparam name="TEntity">tipo da entidade.</typeparam>
        /// <param name="specification">specification</param>
        void Visit<TEntity>(OrSpecification<TEntity> specification) where TEntity : Entity;

        /// <summary>
        /// Método visit para Specification.
        /// </summary>
        /// <typeparam name="TEntity">tipo da entidade.</typeparam>
        /// <typeparam name="TEntityWith">tipo da entidade.</typeparam>
        /// <param name="specification">specification</param>
        void Visit<TEntity, TEntityWith>(WithSpecification<TEntity, TEntityWith> specification) 
            where TEntity : Entity
            where TEntityWith : Entity;

        /// <summary>
        /// Método visit para Specification.
        /// </summary>
        /// <typeparam name="TEntity">tipo da entidade.</typeparam>
        /// <param name="specification">specification</param>
        void Visit<TEntity>(IQueryOverSpecification specification) 
            where TEntity : Entity;
    }
}
