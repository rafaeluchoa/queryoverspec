
namespace Naskar.QueryOverSpec.Test.Repository
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.Unity;
    using Naskar.QueryOverSpec.Impl;
    using NHibernate;

    public class Repository : IRepository
    {
        [Dependency]
        public ISessionFactory Factory { private get; set; }

        public IList<TEntity> Find<TEntity>(Action<IQueryOver<TEntity, TEntity>> builder) where TEntity : Entity
        {
            var session = Factory.OpenSession();

            var query = session.QueryOver<TEntity>();
            builder(query);

            var list = query.List();

            session.Close();
            return list;
        }

        public IList<TEntity> Find<TEntity>(ISpecification<TEntity> spec) where TEntity : Entity
        {
            var session = Factory.OpenSession();
            var query = session.QueryOver<TEntity>();

            var visitor = new QueryOverSpecificationVisitor<TEntity>(query);
            spec.Accept(visitor);

            var list = query.List();

            session.Close();
            return list;
        }

        public IList<TEntity> Find<TEntity>() where TEntity : Entity
        {
            var session = Factory.OpenSession();

            var list = session.QueryOver<TEntity>().List();

            session.Close();

            return list;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : Entity
        {
            var session = Factory.OpenSession();
            session.Save(entity);
            session.Flush();
            session.Close();
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : Entity
        {
            var session = Factory.OpenSession();
            session.Delete(entity);
            session.Flush();
            session.Close();
        }

        public void RemoveAll<TEntity>() where TEntity : Entity
        {
            foreach (var item in this.Find<TEntity>())
            {
                this.Remove(item);
            }
        }
    }
}
