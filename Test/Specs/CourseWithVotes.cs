namespace Naskar.QueryOverSpec.Test.Specs
{
    using System;
    using Naskar.QueryOverSpec.Test.Entities;
    using NHibernate;

    public class CourseWithVotes : ICourseWithVotes
    {
        public ISpecification<Course> By()
        {
            Action<IQueryOver<Course, Course>> action = x => x.JoinQueryOver(y => y.Votes);
           return new QueryOverSpecification<Course>(action);
        }
    }
}
