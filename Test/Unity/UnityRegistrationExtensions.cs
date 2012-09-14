namespace Naskar.QueryOverSpec.Test.Unity
{
    using global::Unity.AutoRegistration;

    public static class UnityRegistrationExtensions
    {
        /// <summary>
        /// Register a type as itself (as opposed to its interfaces)
        /// </summary>
        /// <param name="registration">registration </param>
        /// <returns>self type</returns>
        public static IFluentRegistration AsSelf(this IFluentRegistration registration)
        {
            return registration.As(t => t);
        }
    }
}