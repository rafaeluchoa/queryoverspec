namespace Naskar.QueryOverSpec.Test.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using NHibernate;

    public interface IRepository
    {
        IList<TEntity> Find<TEntity>() where TEntity : class, IIdAccessor;

        IList<TEntity> Find<TEntity>(Action<IQueryOver<TEntity, TEntity>> builder) where TEntity : class, IIdAccessor;

        IList<TEntity> Find<TEntity>(ISpecification<TEntity> spec) where TEntity : class, IIdAccessor;

        IList<TResult> Find<TEntity, TResult>(
            ISpecification<TEntity> spec, params Expression<Func<TEntity, object>>[] projections) 
            where TEntity : class, IIdAccessor;

        IList<TResult> Find<TEntity, TResult>(ISpecification<TEntity> spec, Mapping<TEntity, TResult> map)
            where TEntity : class, IIdAccessor;

        void Add<TEntity>(TEntity entity) where TEntity : class, IIdAccessor;

        void Remove<TEntity>(TEntity entity) where TEntity : class, IIdAccessor;

        void RemoveAll<TEntity>() where TEntity : class, IIdAccessor;
    }
}
