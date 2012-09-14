namespace Naskar.QueryOverSpec.Test.Unity
{
    using Microsoft.Practices.Unity;

    public class DefaultUnityAwareContext : IUnityAwareContext
    {
        public UnityContainer Container { get; set; }
    }
}
