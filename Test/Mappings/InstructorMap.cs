namespace Naskar.QueryOverSpec.Test.Mappings
{
    using Naskar.QueryOverSpec.Test.Entities;

    public class InstructorMap : EntityMap<Instructor>
    {
        public InstructorMap()
        {
            Map(x => x.Name)
                .Length(100);

            HasMany(x => x.Courses)
                .Cascade.All();
        }
    }
}
