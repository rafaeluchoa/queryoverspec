
namespace Naskar.QueryOverSpec.Test.Tests
{
    using FluentAssertions;

    using Microsoft.Practices.Unity;

    using Naskar.QueryOverSpec.Impl;
    using Naskar.QueryOverSpec.Test.Entities;
    using Naskar.QueryOverSpec.Test.Repository;
    using Naskar.QueryOverSpec.Test.Specs;
    using Naskar.QueryOverSpec.Test.Unity;
    using NHibernate.Criterion;
    using NUnit.Framework;

    [TestFixture]
    public class TestSpec
    {
        private UnityContainer _container;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _container = new UnityFactory().Create("Naskar.QueryOverSpec");
        }

        [Test]
        public void TestAddFindAll()
        {
            // Arrange
            var course1 = new Course() { Name = "Java" };
            var course2 = new Course() { Name = "PHP" };

            var repository = _container.Resolve<IRepository>();
            repository.Add(course1);
            repository.Add(course2);

            // Act
            var list = repository.Find<Course>();

            // Assert
            list.Should().HaveCount(2);

            // Cleanup
            repository.RemoveAll<Course>();
        }

        [Test]
        public void TestQueryOverAsParameterAction()
        {
            // Arrange
            var course1 = new Course() { Name = "Java" };
            var course2 = new Course() { Name = "PHP" };

            var vote1 = new Vote() { Course = course1, Mail = "teste" };
            course1.Votes.Add(vote1);

            var repository = _container.Resolve<IRepository>();
            repository.Add(course1);
            repository.Add(course2);

            // Act
            var list =
                repository.Find<Vote>(
                    x => x.JoinQueryOver(y => y.Course).Where(y => y.Name.IsInsensitiveLike("%java%")));

            // Assert
            list.Should().HaveCount(1);

            // Cleanup
            repository.RemoveAll<Course>();
        }

        [Test]
        public void TestSpecification()
        {
            // Arrange
            var course1 = new Course() { Name = "Java" };
            var course2 = new Course() { Name = "PHP" };

            var vote1 = new Vote() { Course = course1, Mail = "teste" };
            course1.Votes.Add(vote1);

            var courseByExample = _container.Resolve<IByExampleSpec<Course>>();
            var courseWithVotes = _container.Resolve<ICourseWithVotes>();

            var repository = _container.Resolve<IRepository>();
            repository.Add(course1);
            repository.Add(course2);

            // Act
            var list =
                repository.Find(courseWithVotes.By().And(courseByExample.By(new Course() { Name = "ava" })));

            // Assert
            list.Should().HaveCount(1);

            // Cleanup
            repository.RemoveAll<Course>();
        }

        [Test]
        public void TestQueryOverWith()
        {
            // Arrange
            var course1 = new Course() { Name = "Java" };
            var course2 = new Course() { Name = "PHP" };

            var vote1 = new Vote() { Course = course1, Mail = "teste" };
            course1.Votes.Add(vote1);

            var voteByExample = _container.Resolve<IByExampleSpec<Vote>>();

            var repository = _container.Resolve<IRepository>();
            repository.Add(course1);
            repository.Add(course2);

            // Act
            var list =
                repository.Find<Vote>(
                    voteByExample.By().With(
                        y => y.Course, new LambdaSpecification<Course>(x => x.Name.IsInsensitiveLike("ava", MatchMode.Anywhere)))
                        .And(x => x.Mail.IsInsensitiveLike("tes", MatchMode.Anywhere)));

            // Assert
            list.Should().HaveCount(1);

            // Cleanup
            repository.RemoveAll<Course>();
        }

        [Test]
        public void TestSpecificationWith()
        {
            // Arrange
            var instructor1 = new Instructor() { Name = "fulano", Ative = true };
            var instructor2 = new Instructor() { Name = "ciclano", Ative = true };

            var course1 = new Course() { Name = "Java", Instructor = instructor1 };
            instructor1.Courses.Add(course1);

            var course2 = new Course() { Name = "PHP" };

            course1.Votes.Add(new Vote() { Course = course1, Mail = "teste" });
            course2.Votes.Add(new Vote() { Course = course2, Mail = "teste" });

            var instructorByExample = _container.Resolve<IByExampleSpec<Instructor>>();
            var courseWithVotes = _container.Resolve<ICourseWithVotes>();

            var repository = _container.Resolve<IRepository>();
            repository.Add(instructor1);
            repository.Add(instructor2);
            repository.Add(course1);
            repository.Add(course2);

            var instructorExample = new Instructor() { Name = "fulano" };

            // Act
            var list =
                repository.Find(
                    courseWithVotes.By().With(x => x.Instructor, instructorByExample.By(instructorExample)));

            // Assert
            list.Should().HaveCount(1);

            // Cleanup
            repository.RemoveAll<Instructor>();
        }

        [Test]
        public void TestSpecificationWithOr()
        {
            // Arrange
            var instructor1 = new Instructor() { Name = "fulano", Ative = true };
            var instructor2 = new Instructor() { Name = "ciclano", Ative = true };

            var course1 = new Course() { Name = "Java", Instructor = instructor1 };
            instructor1.Courses.Add(course1);

            var course2 = new Course() { Name = "PHP", Instructor = instructor2 };
            instructor2.Courses.Add(course2);

            var course3 = new Course() { Name = "C#" };

            var byExamplcourseSpec = _container.Resolve<IByExampleSpec<Course>>();

            var repository = _container.Resolve<IRepository>();
            repository.Add(course1);
            repository.Add(course2);
            repository.Add(course3);
            repository.Add(instructor1);
            repository.Add(instructor2);

            // Act
            var list =
                repository.Find(
                    byExamplcourseSpec.By().With(
                        x => x.Instructor,
                        new LambdaSpecification<Instructor>(x => x.Name.IsInsensitiveLike("fulano", MatchMode.Anywhere)).Or(
                            new LambdaSpecification<Instructor>(x => x.Name.IsInsensitiveLike("ciclano", MatchMode.Anywhere)))));

            // Assert
            list.Should().HaveCount(2);

            // Cleanup
            repository.RemoveAll<Instructor>();
        }

        [Test]
        public void TestSpecificationWithOrUsandoCriterion()
        {
            // Arrange
            var instructor1 = new Instructor() { Name = "fulano", Ative = true };
            var instructor2 = new Instructor() { Name = "ciclano", Ative = true };

            var course1 = new Course() { Name = "Java", Instructor = instructor1 };
            instructor1.Courses.Add(course1);

            var course2 = new Course() { Name = "PHP", Instructor = instructor2 };
            instructor2.Courses.Add(course2);

            var course3 = new Course() { Name = "C#" };

            var courseByExample = _container.Resolve<IByExampleSpec<Course>>();

            var repository = _container.Resolve<IRepository>();
            repository.Add(course1);
            repository.Add(course2);
            repository.Add(course3);
            repository.Add(instructor1);
            repository.Add(instructor2);

            // Act
            var list =
                repository.Find(
                    courseByExample.By()
                        .With(x => x.Instructor, new LambdaSpecification<Instructor>(x => x.Name.IsInsensitiveLike("fulano", MatchMode.Anywhere))
                        .Or(new CriterionSpecification<Instructor>(Property.ForName("Name").Like("ciclano", MatchMode.Anywhere)))));

            // Assert
            list.Should().HaveCount(2);

            // Cleanup
            repository.RemoveAll<Instructor>();
        }
    }
}
