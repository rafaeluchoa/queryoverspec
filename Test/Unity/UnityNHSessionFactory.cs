namespace Naskar.QueryOverSpec.Test.Unity
{
    using FluentNHibernate.Cfg;

    using Microsoft.Practices.Unity;

    using NHibernate;
    using NHibernate.Cfg;

    public class UnityNHSessionFactory : IUnityAware
    {
        public void Configure(IUnityAwareContext context)
        {
            var nhconfig = new Configuration();
            nhconfig.Configure();

            var fluentConfig = Fluently
                .Configure(nhconfig)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UnityNHSessionFactory>()); // TODO: mappings

            var sessionFactory = fluentConfig.BuildSessionFactory();
            context.Container.RegisterInstance(typeof(ISessionFactory), sessionFactory);
        }
    }
}
