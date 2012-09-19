namespace Naskar.QueryOverSpec
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using NHibernate.Criterion;
    using NHibernate.Impl;

    /// <summary>
    /// Mapea resultados usando Expression entre duas entidades.
    /// </summary>
    /// <typeparam name="TFrom">de vem a propriedade</typeparam>
    /// <typeparam name="TResult">para onde vai a propriedade</typeparam>
    public class Mapping<TFrom, TResult>
    {
        private readonly List<Tuple<Expression<Func<TFrom, object>>, Expression<Func<TResult, object>>>> _maps
            = new List<Tuple<Expression<Func<TFrom, object>>, Expression<Func<TResult, object>>>>();

        /// <summary>
        /// Retorna os mapeamentos.
        /// </summary>
        public IList<Tuple<Expression<Func<TFrom, object>>, Expression<Func<TResult, object>>>> Maps
        {
            get
            {
                return _maps;
            }
        }

        /// <summary>
        /// Converte as projections com usando o mesmo nome da propriedade.
        /// </summary>
        /// <typeparam name="TE">tipo</typeparam>
        /// <param name="projections">projetions</param>
        /// <returns>projections usando o mesmo nome da propriedade</returns>
        public static ProjectionList ToProjections<TE>(params Expression<Func<TE, object>>[] projections)
        {
            var projectionList = Projections.ProjectionList();
            foreach (var expression in projections)
            {
                var name = ExpressionProcessor.FindMemberExpression(expression.Body);
                projectionList.Add(Projections.Property(expression), name);
            }

            return projectionList;
        }

        /// <summary>
        /// Adiciona um novo mapeamento.
        /// </summary>
        /// <param name="from">propriedade</param>
        /// <param name="to">propriedade</param>
        /// <returns></returns>
        public Mapping<TFrom, TResult> Add(Expression<Func<TFrom, object>> from, Expression<Func<TResult, object>> to)
        {
            _maps.Add(Tuple.Create(from, to));
            return this;
        }

        public ProjectionList ToProjections()
        {
            var projectionList = Projections.ProjectionList();
            foreach (var map in Maps)
            {
                var name = ExpressionProcessor.FindMemberExpression(map.Item2.Body);
                projectionList.Add(Projections.Property(map.Item1), name);
            }

            return projectionList;
        }
    }
}
