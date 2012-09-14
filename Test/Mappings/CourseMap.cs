namespace Naskar.QueryOverSpec.Test.Mappings
{
    using Naskar.QueryOverSpec.Test.Entities;

    public class CourseMap : EntityMap<Course>
    {
        public CourseMap()
        {
            Id(x => x.Id)
                .GeneratedBy.Increment();

            Map(x => x.Name)
                .Length(100);

            References(x => x.Instructor)
                .Cascade.All();

            HasMany(x => x.Votes)
                .Cascade.All();
        }
    }
}
