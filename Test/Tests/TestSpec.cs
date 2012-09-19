
namespace Naskar.QueryOverSpec.Test.Tests
{
    using FluentAssertions;

    using Microsoft.Practices.Unity;

    using Naskar.QueryOverSpec.Impl;
    using Naskar.QueryOverSpec.Test.Entities;
    using Naskar.QueryOverSpec.Test.Repository;
    using Naskar.QueryOverSpec.Test.Results;
    using Naskar.QueryOverSpec.Test.Specs;
    using Naskar.QueryOverSpec.Test.Unity;
    using NHibernate.Criterion;
    using NUnit.Framework;

    using RestrictionExtensions = Naskar.QueryOverSpec.RestrictionExtensions;

    [TestFixture]
    public class TestSpec
    {
        private UnityContainer _container;

        private IRepository _repository;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _container = new UnityFactory().Create("Naskar.QueryOverSpec");
            _repository = _container.Resolve<IRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            _repository.RemoveAll<Vote>();
            _repository.RemoveAll<Instructor>();
            _repository.RemoveAll<Course>();
        }

        [Test]
        public void TestAddFindAll()
        {
            // Arrange
            var course1 = new Course() { Name = "Java" };
            var course2 = new Course() { Name = "PHP" };
            
            _repository.Add(course1);
            _repository.Add(course2);

            // Act
            var list = _repository.Find<Course>();

            // Assert
            list.Should().HaveCount(2);
        }

        [Test]
        public void TestQueryOverAsParameterAction()
        {
            // Arrange
            var course1 = new Course() { Name = "Java" };
            var course2 = new Course() { Name = "PHP" };

            var vote1 = new Vote() { Course = course1, Mail = "teste" };
            course1.Votes.Add(vote1);

            _repository.Add(course1);
            _repository.Add(course2);

            // Act
            var list =
                _repository.Find<Vote>(
                    x => x.JoinQueryOver(y => y.Course).Where(y => y.Name.IsInsensitiveLike("%java%")));

            // Assert
            list.Should().HaveCount(1);
        }

        [Test]
        public void TestWithLambda()
        {
            // Arrange
            var course1 = new Course() { Name = "Java" };
            var course2 = new Course() { Name = "C#" };

            var vote1 = new Vote() { Course = course1, Mail = "teste" };
            course1.Votes.Add(vote1);

            _repository.Add(course1);
            _repository.Add(course2);

            RestrictionExtensions.RegisterQueryOverExtensions();

            // Act
            var list =
                _repository.Find<Course>(
                    new LambdaSpecification<Course>(x => x.Votes.With(y => y.Mail.IsInsensitiveLike("te", MatchMode.Anywhere))));

            // Assert
            list.Should().HaveCount(1);
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

            _repository.Add(course1);
            _repository.Add(course2);

            // Act
            var list =
                _repository.Find(courseWithVotes.By().And(courseByExample.By(new Course() { Name = "ava" })));

            // Assert
            list.Should().HaveCount(1);
        }

        [Test]
        public void TestUsingId()
        {
            // Arrange
            var course1 = new Course() { Name = "Java" };
            _repository.Add(course1);

            var courseByExample = _container.Resolve<IByExampleSpec<Course>>();

            // Act
            var list =
                _repository.Find(courseByExample.By(new Course() { Id = course1.Id }));

            // Assert
            list.Should().Contain(x => x.Id == course1.Id);
        }

        [Test]
        public void TestSpecificationAndCriterion()
        {
            // Arrange
            var course1 = new Course() { Name = "Java" };
            var course2 = new Course() { Name = "PHP" };

            var vote1 = new Vote() { Course = course1, Mail = "teste" };
            course1.Votes.Add(vote1);

            var courseWithVotes = _container.Resolve<ICourseWithVotes>();

            _repository.Add(course1);
            _repository.Add(course2);

            // Act
            var list =
                _repository.Find(courseWithVotes.By().And(Example.Create(new Course() { Name = "Java" })));

            // Assert
            list.Should().HaveCount(1);
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

            _repository.Add(course1);
            _repository.Add(course2);

            // Act
            var list =
                _repository.Find<Vote>(
                    voteByExample.By().With(
                        y => y.Course, new LambdaSpecification<Course>(x => x.Name.IsInsensitiveLike("ava", MatchMode.Anywhere)))
                        .And(x => x.Mail.IsInsensitiveLike("tes", MatchMode.Anywhere)));

            // Assert
            list.Should().HaveCount(1);
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

            _repository.Add(instructor1);
            _repository.Add(instructor2);
            _repository.Add(course1);
            _repository.Add(course2);

            var instructorExample = new Instructor() { Name = "fulano" };

            // Act
            var list =
                _repository.Find(
                    courseWithVotes.By().With(x => x.Instructor, instructorByExample.By(instructorExample)));

            // Assert
            list.Should().HaveCount(1);
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

            _repository.Add(course1);
            _repository.Add(course2);
            _repository.Add(course3);
            _repository.Add(instructor1);
            _repository.Add(instructor2);

            // Act
            var list =
                _repository.Find(
                    byExamplcourseSpec.By().With(
                        x => x.Instructor,
                        new LambdaSpecification<Instructor>(x => x.Name.IsInsensitiveLike("fulano", MatchMode.Anywhere)).Or(
                            new LambdaSpecification<Instructor>(x => x.Name.IsInsensitiveLike("ciclano", MatchMode.Anywhere)))));

            // Assert
            list.Should().HaveCount(2);
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

            _repository.Add(course1);
            _repository.Add(course2);
            _repository.Add(course3);
            _repository.Add(instructor1);
            _repository.Add(instructor2);

            // Act
            var list =
                _repository.Find(
                    courseByExample.By()
                        .With(x => x.Instructor, new LambdaSpecification<Instructor>(x => x.Name.IsInsensitiveLike("fulano", MatchMode.Anywhere))
                        .Or(new CriterionSpecification<Instructor>(Property.ForName("Name").Like("ciclano", MatchMode.Anywhere)))));

            // Assert
            list.Should().HaveCount(2);
        }

        [Test]
        public void TestSpecificationWithTransformResult()
        {
            // Arrange
            var course1 = new Course() { Name = "Java" };
            var course2 = new Course() { Name = "PHP" };

            var courseByExample = _container.Resolve<IByExampleSpec<Course>>();

            _repository.Add(course1);
            _repository.Add(course2);

            // Act
            var list = _repository.Find<Course, CourseDTO>(
                courseByExample.By(new Course() { Name = "PH" }), x => x.Id, x => x.Name);

            // Assert
            list.Should().HaveCount(1);
            list[0].Name.Should().NotBeNull().Equals(course1.Name);
        }

        [Test]
        public void TestSpecificationWithTransformMapping()
        {
            // Arrange
            var course1 = new Course() { Name = "Java" };
            var course2 = new Course() { Name = "PHP" };

            var courseByExample = _container.Resolve<IByExampleSpec<Course>>();

            _repository.Add(course1);
            _repository.Add(course2);

            // Act
            var list = _repository.Find(
                courseByExample.By(new Course() { Name = "PH" }),
                new Mapping<Course, CourseDTO>()
                    .Add(x => x.Id, y => y.Id)
                    .Add(x => x.Name, y => y.CompleteName));

            // Assert
            list.Should().HaveCount(1);
            list[0].CompleteName.Should().NotBeNull().Equals(course1.Name);
        }
    }
}
