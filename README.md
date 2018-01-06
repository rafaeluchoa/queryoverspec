# Naskar.QueryOverSpec

A simple implementation of the Specification Pattern using NHibernate QueryOver. Create queries reusing simple predicates.

### Refs

https://en.wikipedia.org/wiki/Specification_pattern

### Nuget

https://www.nuget.org/packages/Naskar.QueryOverSpec

### Examples:
	
	[Test]
	public void TestSpecification()
	{
		// Arrange
		var course1 = new Course() { Name = "Java" };
		var course2 = new Course() { Name = "C#" };

		var vote1 = new Vote() { Course = course1, Mail = "test@naskar.com" };
		course1.Votes.Add(vote1);

		var courseByExample = _container.Resolve<IByExampleSpec<Course>>();
		var courseWithVotes = _container.Resolve<ICourseWithVotes>();

		var repository = _container.Resolve<IRepository>();
		repository.Add(course1);
		repository.Add(course2);

		// Act
		var list = repository.Find(
                  courseWithVotes.By().
                  And(courseByExample.By(new Course() { Name = "ava" })));

		// Assert
		list.Should().HaveCount(1);

		// Cleanup
		repository.RemoveAll<Course>();
	}

### Specification:

	public class CourseWithVotes : ICourseWithVotes
	{
		public ISpecification<Course> By()
		{
			Action<IQueryOver<Course, Course>> action = x => x.JoinQueryOver(y => y.Votes);
			return new QueryOverSpecification<Course>(action);
		}
	}


### Repository:

	public IList<TEntity> Find<TEntity>(ISpecification<TEntity> spec) where TEntity : Entity
	{
		var session = Factory.OpenSession();
		var query = session.QueryOver<TEntity>();

		var visitor = new QueryOverSpecificationVisitor<TEntity>(query);
		spec.Accept(visitor);

		var list = query.List();

		session.Close();
		return list;
	}

### More Examples

