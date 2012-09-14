
namespace Naskar.QueryOverSpec.Test.Specs
{
    using Naskar.QueryOverSpec.Test.Entities;

    public interface ICourseWithVotes
    {
        ISpecification<Course> By();
    }
}
