namespace Naskar.QueryOverSpec.Test.Entities
{
    using System.Collections.Generic;

    public class Instructor : Entity
    {
        public Instructor()
        {
            this.Courses = new List<Course>();
        }

        public virtual string Name { get; set; }

        public virtual bool Ative { get; set; }

        public virtual IList<Course> Courses { get; set; }
    }
}
