// --------------------------------------------------------------------------------------------------------------------
// Autor: rafaeluchoa
// Data de criação: 14/09/2012 09:00:00 AM
// --------------------------------------------------------------------------------------------------------------------

namespace Naskar.QueryOverSpec.Impl
{
    using NHibernate;

    /// <summary>
    /// Builder que passa um QueryOver como parametro. As subclasses devem 
    /// adicionar suas restrições ao queryover.
    /// </summary>
    public interface IQueryOverSpecification
    {
        /// <summary>
        /// Executa a adição das restrições para essa specification usando um queryover.
        /// </summary>
        /// <typeparam name="TE">entidade raiz</typeparam>
        /// <typeparam name="TSe">entidade relacionada ou a mesma entidade raiz.</typeparam>
        /// <param name="queryOver">queryover usando a consulta.</param>
        void Builder<TE, TSe>(IQueryOver<TE, TSe> queryOver);
    }
}
