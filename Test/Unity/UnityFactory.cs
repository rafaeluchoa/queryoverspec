namespace Naskar.QueryOverSpec.Test.Unity
{
    using Microsoft.Practices.Unity;

    using global::Unity.AutoRegistration;

    public class UnityFactory
    {
        public UnityContainer Create(string namespacePrefix)
        {
            var container = new UnityContainer();
            container.ConfigureAutoRegistration()
                .ExcludeAssemblies(assembly => !assembly.FullName.StartsWith(namespacePrefix))
                .Exclude(type => !type.FullName.Contains(namespacePrefix) || !type.IsClass || type.IsAbstract)
                .Include(
                    type =>
                    type.GetInterfaces().Length > 0,
                    Then.Register().AsAllInterfacesOfType().UsingSingletonMode())
                .Include(
                    type =>
                    type.GetInterfaces().Length > 0,
                    Then.Register().WithTypeName().UsingSingletonMode())
                .ApplyAutoRegistration();

            InitAware(container);

            return container;
        }

        private static void InitAware(UnityContainer container)
        {
            foreach (var item in container.ResolveAll<IUnityAware>())
            {
                item.Configure(new DefaultUnityAwareContext()
                    {
                        Container = container
                    });
            }
        }
    }
}   