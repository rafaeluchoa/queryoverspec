// --------------------------------------------------------------------------------------------------------------------
// Autor: rafaeluchoa
// Data de criação: 7/31/2012 09:00:00 AM
// --------------------------------------------------------------------------------------------------------------------

namespace Naskar.QueryOverSpec.Impl
{
    using System;
    using System.Linq.Expressions;
    using Naskar.QueryOverSpec.Extensions;
    using NHibernate.Criterion;

    /// <summary>
    /// Métodos de extensão para Specifications.
    /// </summary>
    public static class SpecificationBuilder
    {
        /// <summary>
        /// Cria uma AndSpecification.
        /// </summary>
        /// <typeparam name="TEntity">tipo da entidade.</typeparam>
        /// <param name="left">left.</param>
        /// <param name="right">right.</param>
        /// <returns>uma specification que une as duas specifications.</returns>
        public static ISpecification<TEntity> And<TEntity>(this ISpecification<TEntity> left, ISpecification<TEntity> right)
            where TEntity : Entity
        {
            return new AndSpecification<TEntity>(left, right);
        }

        /// <summary>
        /// Cria uma AndSpecification com uma lambda.
        /// </summary>
        /// <typeparam name="TEntity">tipo da entidade.</typeparam>
        /// <param name="left">left.</param>
        /// <param name="expr">expression.</param>
        /// <returns>uma specification que une as duas specifications.</returns>
        public static ISpecification<TEntity> And<TEntity>(this ISpecification<TEntity> left, Expression<Func<TEntity, bool>> expr)
            where TEntity : Entity
        {
            return new AndSpecification<TEntity>(left, new LambdaSpecification<TEntity>(expr));
        }

        /// <summary>
        /// Cria uma AndSpecification com um criterion.
        /// </summary>
        /// <typeparam name="TEntity">tipo da entidade.</typeparam>
        /// <param name="left">left.</param>
        /// <param name="criterion">criterion.</param>
        /// <returns>uma specification que une as duas specifications.</returns>
        public static ISpecification<TEntity> And<TEntity>(this ISpecification<TEntity> left, ICriterion criterion)
            where TEntity : Entity
        {
            return new AndSpecification<TEntity>(left, new CriterionSpecification<TEntity>(criterion));
        }

        /// <summary>
        /// Cria uma OrSpecification.
        /// </summary>
        /// <typeparam name="TEntity">tipo da entidade.</typeparam>
        /// <param name="left">left.</param>
        /// <param name="right">right.</param>
        /// <returns>uma specification que une as duas specifications.</returns>
        public static ISpecification<TEntity> Or<TEntity>(this ISpecification<TEntity> left, ISpecification<TEntity> right)
            where TEntity : Entity
        {
            return new OrSpecification<TEntity>(left, right);
        }

        /// <summary>
        /// Cria uma WithSpecification.
        /// </summary>
        /// <typeparam name="TEntity">tipo da entidade.</typeparam>
        /// <typeparam name="TEntityWith">subtipo contido dentro de TEntity: TEntity.TEntityWith</typeparam>
        /// <returns>uma specification que une as duas specifications.</returns>
        public static ISpecification<TEntity> With<TEntity, TEntityWith>(
            this ISpecification<TEntity> root, Expression<Func<TEntity, object>> path, ISpecification<TEntityWith> with)
            where TEntity : Entity
            where TEntityWith : Entity
        {
            return new WithSpecification<TEntity, TEntityWith>(root, path, with);
        }
    }
}