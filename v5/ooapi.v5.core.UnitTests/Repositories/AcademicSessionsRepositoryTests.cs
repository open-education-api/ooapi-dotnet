using AutoFixture;
using NSubstitute;
using ooapi.v5.core.Utility;
using ooapi.v5.Enums;
using ooapi.v5.Models;
using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories;

[TestFixture]
public class AcademicSessionsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetAcademicSession_WithValidId_ReturnsAcademicSession()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var set = new List<AcademicSession>()
        {
            _fixture.Create<AcademicSession>(),
            _fixture.Create<AcademicSession>()
        }.AsQueryable();
        dbContext.AcademicSessionsNoTracking.Returns(set);
        var repository = new AcademicSessionsRepository(dbContext);

        // Act
        var result = repository.GetAcademicSession(set.First().AcademicSessionId, new DataRequestParameters());

        // Assert
        Assert.IsInstanceOf<AcademicSession>(result);
    }

    // [Test]
    // public void GetAcademicSession_WithInvalidId_ReturnsNull()
    // {
    //     // Arrange
    //     var academicSessionId = Guid.NewGuid();
    //     var dataRequestParameters = _fixture.Create<DataRequestParameters>();
    //     var set = Substitute.For<IQueryable<AcademicSession>>();
    //     _dbContext.AcademicSessionsNoTracking.Returns(set);
    //     set.Include(Arg.Any<System.Linq.Expressions.Expression<Func<AcademicSession, object>>>()).Returns(set);
    //     set.FirstOrDefault(Arg.Any<System.Linq.Expressions.Expression<Func<AcademicSession, bool>>>()).Returns((AcademicSession)null);
    //
    //     // Act
    //     var result = _repository.GetAcademicSession(academicSessionId, dataRequestParameters);
    //
    //     // Assert
    //     // _dbContext.Received(1).AcademicSessionsNoTracking;
    //     set.Received(1).Include(x => x.Attributes);
    //     set.Received(1).FirstOrDefault(x => x.AcademicSessionId.Equals(academicSessionId));
    //     Assert.IsNull(result);
    // }
    //
    // [Test]
    // public void GetAcademicSession_WithValidPrimaryCode_ReturnsAcademicSession()
    // {
    //     // Arrange
    //     var primaryCode = _fixture.Create<string>();
    //     var dataRequestParameters = _fixture.Create<DataRequestParameters>();
    //     var academicSession = _fixture.Create<AcademicSession>();
    //     academicSession.PrimaryCode = primaryCode;
    //     var set = Substitute.For<IQueryable<AcademicSession>>();
    //     _dbContext.AcademicSessionsNoTracking.Returns(set);
    //     set.Include(Arg.Any<System.Linq.Expressions.Expression<Func<AcademicSession, object>>>()).Returns(set);
    //     set.FirstOrDefault(Arg.Any<System.Linq.Expressions.Expression<Func<AcademicSession, bool>>>()).Returns(academicSession);
    //
    //     // Act
    //     var result = _repository.GetAcademicSession(primaryCode, dataRequestParameters);
    //
    //     // Assert
    //     // _dbContext.Received(1).AcademicSessionsNoTracking;
    //     set.Received(1).Include(x => x.Attributes);
    //     set.Received(1).FirstOrDefault(x => x.PrimaryCode.Equals(primaryCode));
    //     Assert.IsNotNull(result);
    //     Assert.AreEqual(academicSession, result);
    // }
    //
    // [Test]
    // public void GetAcademicSession_WithInvalidPrimaryCode_ReturnsNull()
    // {
    //     // Arrange
    //     var primaryCode = _fixture.Create<string>();
    //     var dataRequestParameters = _fixture.Create<DataRequestParameters>();
    //     var set = Substitute.For<IQueryable<AcademicSession>>();
    //     _dbContext.AcademicSessionsNoTracking.Returns(set);
    //     set.Include(Arg.Any<System.Linq.Expressions.Expression<Func<AcademicSession, object>>>()).Returns(set);
    //     set.FirstOrDefault(Arg.Any<System.Linq.Expressions.Expression<Func<AcademicSession, bool>>>()).Returns((AcademicSession)null);
    //
    //     // Act
    //     var result = _repository.GetAcademicSession(primaryCode, dataRequestParameters);
    //
    //     // Assert
    //     // _dbContext.Received(1).AcademicSessionsNoTracking;
    //     set.Received(1).Include(x => x.Attributes);
    //     set.Received(1).FirstOrDefault(x => x.PrimaryCode.Equals(primaryCode));
    //     Assert.IsNull(result);
    // }
    //
    // [Test]
    // public void GetAllOrderedBy_WithAcademicSessionType_ReturnsFilteredSet()
    // {
    //     // Arrange
    //     var academicSessionType = _fixture.Create<AcademicSessionTypeEnum>();
    //     var dataRequestParameters = _fixture.Create<DataRequestParameters>();
    //     var set = Substitute.For<IQueryable<AcademicSession>>();
    //     _dbContext.AcademicSessionsNoTracking.Returns(set);
    //     set.Include(Arg.Any<System.Linq.Expressions.Expression<Func<AcademicSession, object>>>()).Returns(set);
    //     set.Where(Arg.Any<System.Linq.Expressions.Expression<Func<AcademicSession, bool>>>()).Returns(set);
    //
    //     // Act
    //     var result = _repository.GetAllOrderedBy(dataRequestParameters, academicSessionType.ToString());
    //
    //     // Assert
    //     // _dbContext.Received(1).AcademicSessionsNoTracking;
    //     set.Received(1).Include(x => x.Attributes);
    //     set.Received(1).Where(x => x.AcademicSessionType.Equals(academicSessionType));
    //     Assert.IsInstanceOf<Pagination<AcademicSession>>(result);
    // }
    //
    // [Test]
    // public void GetAllOrderedBy_WithConsumer_ReturnsFilteredSet()
    // {
    //     // Arrange
    //     var consumer = _fixture.Create<string>();
    //     var dataRequestParameters = _fixture.Create<DataRequestParameters>();
    //     dataRequestParameters.Consumer = consumer;
    //     var set = Substitute.For<IQueryable<AcademicSession>>();
    //     _dbContext.AcademicSessionsNoTracking.Returns(set);
    //     set.Include(Arg.Any<System.Linq.Expressions.Expression<Func<AcademicSession, object>>>()).Returns(set);
    //     set.Include(Arg.Any<System.Linq.Expressions.Expression<Func<AcademicSession, IEnumerable<Consumer>>>>()).Returns(set);
    //
    //     // Act
    //     var result = _repository.GetAllOrderedBy(dataRequestParameters);
    //
    //     // Assert
    //     // _dbContext.Received(1).AcademicSessionsNoTracking;
    //     set.Received(1).Include(x => x.Attributes);
    //     set.Received(1).Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(consumer)));
    //     Assert.IsInstanceOf<Pagination<AcademicSession>>(result);
    // }
}

public class Foo
{
    public string PrimaryCodeType { get; set; }
    public string PrimaryCode { get; set; }

    public IdentifierEntry primaryCodeIdentifier
    {
        get
        {
            return new IdentifierEntry() { CodeType = PrimaryCodeType, Code = PrimaryCode };
        }
        set
        {
            PrimaryCode = value.Code;
            PrimaryCodeType = value.CodeType;
        }
    }
}