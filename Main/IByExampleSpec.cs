// --------------------------------------------------------------------------------------------------------------------
// Autor: rafaeluchoa
// Data de criação: 7/31/2012 09:00:00 AM
// --------------------------------------------------------------------------------------------------------------------

namespace Naskar.QueryOverSpec
{
    /// <summary>
    /// Cria uma Specification que consulta uma entidade usando Query By Example.
    /// </summary>
    /// <typeparam name="TEntity">tipo da entidade</typeparam>
    public interface IByExampleSpec<TEntity> 
        where TEntity : Entity
    {
        /// <summary>
        /// Cria uma Specification que consulta uma entidade usando Query By Example.
        /// </summary>
        /// <param name="entity">uma instância da entidade.</param>
        /// <returns>uma speecification que pode efetuar a consulta.</returns>
        ISpecification<TEntity> By(TEntity entity = null);
    }
}
