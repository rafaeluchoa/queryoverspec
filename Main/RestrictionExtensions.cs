// --------------------------------------------------------------------------------------------------------------------
// Autor: rafaeluchoa
// Data de criação: 7/31/2012 09:00:00 AM
// --------------------------------------------------------------------------------------------------------------------

namespace Naskar.QueryOverSpec
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    using NHibernate.Criterion;
    using NHibernate.Impl;

    using Expression = System.Linq.Expressions.Expression;

    /// <summary>
    /// Registra métodos de extensão como restrições adicionais 
    /// para o Domain quando usando QueryOver.
    /// </summary>
    public static class RestrictionExtensions
    {
        /// <summary>
        /// Adiciona um filtro a uma entidade de uma lista.
        /// Usado como extensão durante uma consulta usando QueryOver.
        /// </summary>
        /// <typeparam name="TEntity">tipo da entidade</typeparam>
        /// <param name="entity">entidade</param>
        /// <param name="predicate">filtro dentro da entidade da lista.</param>
        /// <returns>sempre retorna true, deve ser usando como uma extension com QueryOver.</returns>
        public static bool With<TEntity>(this IEnumerable<TEntity> entity, Func<TEntity, bool> predicate)
            where TEntity : class, IIdAccessor
        {
            return true;
        }

        #region Process

        /// <summary>
        /// Efetua o registro das extensions usadas no QueryOver.
        /// </summary>
        public static void RegisterQueryOverExtensions()
        {
            try
            {
                ExpressionProcessor.RegisterCustomMethodCall(
                    () => new List<IIdAccessor>().With(c => c != null), ProcessWith);
            }
            catch
            {
                // Already registered
            }
        }

        /// <summary>
        /// Cria um DetachedCriteria usando o lambda para entidade da lista.
        /// </summary>
        /// <param name="methodCallExpression">expression.</param>
        /// <returns>um criterio para ser incluído na consulta.</returns>
        internal static ICriterion ProcessWith(MethodCallExpression methodCallExpression)
        {
            var property = methodCallExpression.Arguments[0];

            var criterionProperty = methodCallExpression.Arguments[1];

            var propertyName = ExpressionProcessor.FindMemberExpression(property);
            var propertyParentName = ((MemberExpression)property).Member.DeclaringType.Name;

            var entityType = property.Type.GetGenericArguments()[0];
            var criterion = CreateCriterionFromLambda(criterionProperty);

            var dc =
                DetachedCriteria
                    .For(entityType)
                    .Add(criterion)
                    .SetProjection(Projections.Property(propertyParentName));

            return Property.ForName(propertyName).In(dc);
        }

        /// <summary>
        /// Cria um ICriterion baseado em um lambda expression. 
        /// Usa o ExpressionProcessor.ProcessLambdaExpression.
        /// </summary>
        /// <param name="expression">expression</param>
        /// <returns>as condições sobre uma entidade da expression.</returns>
        private static ICriterion CreateCriterionFromLambda(Expression expression)
        {
            var method = typeof(ExpressionProcessor).GetMethod(
                "ProcessLambdaExpression", BindingFlags.Static | BindingFlags.NonPublic);

            return (ICriterion)method.Invoke(null, new object[] { expression });
        }

         #endregion
    }
}
