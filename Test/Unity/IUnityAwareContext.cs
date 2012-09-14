namespace Naskar.QueryOverSpec.Test.Unity
{
    using Microsoft.Practices.Unity;

    public interface IUnityAwareContext
    {
        UnityContainer Container { get; }
    }
}
