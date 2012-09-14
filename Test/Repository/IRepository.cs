namespace Naskar.QueryOverSpec.Test.Repository
{
    using System;
    using System.Collections.Generic;

    using NHibernate;

    public interface IRepository
    {
        IList<TEntity> Find<TEntity>() where TEntity : Entity;

        IList<TEntity> Find<TEntity>(Action<IQueryOver<TEntity, TEntity>> builder) where TEntity : Entity;

        IList<TEntity> Find<TEntity>(ISpecification<TEntity> spec) where TEntity : Entity;

        void Add<TEntity>(TEntity entity) where TEntity : Entity;

        void Remove<TEntity>(TEntity entity) where TEntity : Entity;

        void RemoveAll<TEntity>() where TEntity : Entity;
    }
}
