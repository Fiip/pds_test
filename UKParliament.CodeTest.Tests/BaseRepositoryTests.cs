using Microsoft.EntityFrameworkCore;
using Moq;
using UKParliament.CodeTest.Data;
using Xunit;

public class BaseRepositoryTests
{
    public class TestEntity : BaseEntity
    {
        public string? Name { get; set; }
    }

    public class TestRepository : BaseRepository<TestEntity>
    {
        private readonly DbSet<TestEntity> _dbSet;

        public TestRepository(PersonManagerContext context, DbSet<TestEntity> dbSet) : base(context)
        {
            _dbSet = dbSet;
        }

        protected override DbSet<TestEntity> DbSet => _dbSet;
    }

    private readonly Mock<PersonManagerContext> _mockContext;
    private readonly Mock<DbSet<TestEntity>> _mockDbSet;
    private readonly TestRepository _repository;

    public BaseRepositoryTests()
    {
        _mockContext = new Mock<PersonManagerContext>();
        _mockDbSet = new Mock<DbSet<TestEntity>>();
        _repository = new TestRepository(_mockContext.Object, _mockDbSet.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddEntityAndSaveChanges()
    {
        var entity = new TestEntity { Name = "Test" };
        _mockDbSet.Setup(x => x.Add(entity));
        _mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _repository.CreateAsync(entity);

        _mockDbSet.Verify(x => x.Add(entity), Times.Once);
        _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(entity.Id, result);
    }
}