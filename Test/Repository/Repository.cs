
namespace Naskar.QueryOverSpec.Test.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Microsoft.Practices.Unity;

    using Naskar.QueryOverSpec.Impl;

    using NHibernate;
    using NHibernate.Transform;

    public class Repository : IRepository
    {
        [Dependency]
        public ISessionFactory Factory { private get; set; }

        public IList<TEntity> Find<TEntity>(Action<IQueryOver<TEntity, TEntity>> builder) where TEntity : class, IIdAccessor
        {
            var session = Factory.OpenSession();

            var query = session.QueryOver<TEntity>();
            builder(query);

            var list = query.List();

            session.Close();
            return list;
        }

        public IList<TEntity> Find<TEntity>(ISpecification<TEntity> spec) where TEntity : class, IIdAccessor
        {
            var session = Factory.OpenSession();
            var query = session.QueryOver<TEntity>();

            var visitor = new QueryOverSpecificationVisitor<TEntity>(query);
            spec.Accept(visitor);

            var list = query.List();

            session.Close();
            return list;
        }

        public IList<TEntity> Find<TEntity>() where TEntity : class, IIdAccessor
        {
            var session = Factory.OpenSession();

            var list = session.QueryOver<TEntity>().List();

            session.Close();

            return list;
        }

        public IList<TResult> Find<TEntity, TResult>(ISpecification<TEntity> spec, params Expression<Func<TEntity, object>>[] projections) 
            where TEntity : class, IIdAccessor
        {
            var session = Factory.OpenSession();
            var query = session.QueryOver<TEntity>();

            var visitor = new QueryOverSpecificationVisitor<TEntity>(query);
            spec.Accept(visitor);

            query.Select(Mapping<TEntity, TResult>.ToProjections(projections))
                .TransformUsing(Transformers.AliasToBean<TResult>());

            var list = query.List<TResult>();

            session.Close();
            return list;
        }

        public IList<TResult> Find<TEntity, TResult>(ISpecification<TEntity> spec, Mapping<TEntity, TResult> map)
            where TEntity : class, IIdAccessor
        {
            var session = Factory.OpenSession();
            var query = session.QueryOver<TEntity>();

            var visitor = new QueryOverSpecificationVisitor<TEntity>(query);
            spec.Accept(visitor);

            query.Select(map.ToProjections()).TransformUsing(Transformers.AliasToBean<TResult>());

            var list = query.List<TResult>();

            session.Close();
            return list;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class, IIdAccessor
        {
            var session = Factory.OpenSession();
            session.Save(entity);
            session.Flush();
            session.Close();
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class, IIdAccessor
        {
            var session = Factory.OpenSession();
            session.Delete(entity);
            session.Flush();
            session.Close();
        }

        public void RemoveAll<TEntity>() where TEntity : class, IIdAccessor
        {
            foreach (var item in this.Find<TEntity>())
            {
                this.Remove(item);
            }
        }
    }
}
