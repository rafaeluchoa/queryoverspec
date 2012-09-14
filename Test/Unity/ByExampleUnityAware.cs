namespace Naskar.QueryOverSpec.Test.Unity
{
    using FluentNHibernate;

    using Microsoft.Practices.Unity;

    using Naskar.QueryOverSpec;

    public class ByExampleUnityAware : IUnityAware
    {
        public void Configure(IUnityAwareContext context)
        {
            var mapping = context.Container.ResolveAll<IMappingProvider>();
            foreach (var o in mapping)
            {
                var type = o.GetClassMapping().Type;

                var typeInterface = typeof(IByExampleSpec<>).MakeGenericType(type);
                var typeClass = typeof(ByExampleSpec<>).MakeGenericType(type);

                context.Container.RegisterType(typeInterface, typeClass, new ContainerControlledLifetimeManager());
            }
        }
    }
}
