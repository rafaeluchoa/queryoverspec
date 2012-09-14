namespace Naskar.QueryOverSpec.Test.Entities
{
    using System.Collections.Generic;

    public class Course : Entity
    {
        public Course()
        {
            this.Votes = new List<Vote>();
        }

        public virtual string Name { get; set; }

        public virtual Instructor Instructor { get; set; }

        public virtual IList<Vote> Votes { get; set; }
    }
}
