namespace Naskar.QueryOverSpec.Test.Mappings
{
    using Naskar.QueryOverSpec.Test.Entities;

    public class VoteMap : EntityMap<Vote>
    {
        public VoteMap()
        {
            Map(x => x.Mail)
                .Length(255);

            References(x => x.Course)
                .Cascade.None();
        }
    }
}
