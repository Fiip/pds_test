using Moq;
using Xunit;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;

public class PersonServiceTests
{
    private readonly Mock<IRepository<Person>> _mockPersonRepository;
    private readonly Mock<IRepository<Department>> _mockDepartmentRepository;
    private readonly PersonService _service;

    public PersonServiceTests()
    {
        _mockPersonRepository = new Mock<IRepository<Person>>();
        _mockDepartmentRepository = new Mock<IRepository<Department>>();
        _service = new PersonService(_mockPersonRepository.Object, _mockDepartmentRepository.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldCallRepositoryCreateAsync()
    {
        // Arrange
        var person = new Person { Id = 1, FirstName = "John", LastName = "Doe", DepartmentId = 1, BirthDate = DateOnly.MaxValue };
        _mockPersonRepository.Setup(x => x.CreateAsync(person))
            .Returns(Task.FromResult(1));

        // Act
        await _service.CreateAsync(person);

        // Assert
        _mockPersonRepository.Verify(x => x.CreateAsync(person), Times.Once);
    }

    [Fact]
    public void GetById_ShouldReturnPersonFromRepository()
    {
        // Arrange
        var expectedPerson = new Person { Id = 1, FirstName = "John", LastName = "Doe", DepartmentId = 1, BirthDate = DateOnly.MaxValue };
        _mockPersonRepository.Setup(x => x.GetById(1))
            .Returns(expectedPerson);

        // Act
        var result = _service.GetById(1);

        // Assert
        Assert.Equal(expectedPerson, result);
        _mockPersonRepository.Verify(x => x.GetById(1), Times.Once);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenPersonNotFound()
    {
        // Arrange
        _mockPersonRepository.Setup(x => x.GetById(999))
            .Returns((Person)null);

        // Act
        var result = _service.GetById(999);

        // Assert
        Assert.Null(result);
        _mockPersonRepository.Verify(x => x.GetById(999), Times.Once);
    }

    [Fact]
    public void List_ShouldReturnPeopleWithTheirDepartments()
    {
        // Arrange
        var people = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName="Doe", DepartmentId = 1 , BirthDate = DateOnly.MinValue},
            new Person { Id = 2, FirstName = "Jane", LastName="Smith",DepartmentId = 2, BirthDate = DateOnly.MinValue },
            new Person { Id = 3, FirstName = "Bob", LastName="Johnson",DepartmentId = 1, BirthDate = DateOnly.MinValue }
        };

        var departments = new List<Department>
        {
            new Department { Id = 1, Name = "HR" },
            new Department { Id = 2, Name = "IT" }
        };

        _mockPersonRepository.Setup(x => x.List())
            .Returns(people);

        _mockDepartmentRepository.Setup(x => x.List())
            .Returns(departments);

        // Act
        var result = _service.List();

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal("HR", result.First(p => p.Id == 1).Department?.Name);
        Assert.Equal("IT", result.First(p => p.Id == 2).Department?.Name);
        Assert.Equal("HR", result.First(p => p.Id == 3).Department?.Name);

        _mockPersonRepository.Verify(x => x.List(), Times.Once);
        _mockDepartmentRepository.Verify(x => x.List(), Times.Once);
    }

    [Fact]
    public void List_ShouldHandlePersonWithNonExistentDepartment()
    {
        // Arrange
        var people = new List<Person>
        {
           new Person { Id = 1, FirstName = "John", LastName = "Doe", DepartmentId = 999, BirthDate = DateOnly.MaxValue }
        };

        var departments = new List<Department>
        {
            new Department { Id = 1, Name = "HR" }
        };

        _mockPersonRepository.Setup(x => x.List())
            .Returns(people);

        _mockDepartmentRepository.Setup(x => x.List())
            .Returns(departments);

        // Act
        var result = _service.List();

        // Assert
        Assert.Single(result);
        Assert.Null(result.First().Department);

        _mockPersonRepository.Verify(x => x.List(), Times.Once);
        _mockDepartmentRepository.Verify(x => x.List(), Times.Once);
    }

    [Fact]
    public void List_ShouldHandleEmptyLists()
    {
        // Arrange
        _mockPersonRepository.Setup(x => x.List())
            .Returns(new List<Person>());

        _mockDepartmentRepository.Setup(x => x.List())
            .Returns(new List<Department>());

        // Act
        var result = _service.List();

        // Assert
        Assert.Empty(result);

        _mockPersonRepository.Verify(x => x.List(), Times.Once);
        _mockDepartmentRepository.Verify(x => x.List(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldCallRepositoryUpdateAsync()
    {
        // Arrange
        var person = new Person { Id = 1, FirstName = "John", LastName = "Doe Updated", DepartmentId = 1, BirthDate = DateOnly.MaxValue };
        _mockPersonRepository.Setup(x => x.UpdateAsync(person))
            .Returns(Task.CompletedTask);

        // Act
        await _service.UpdateAsync(person);

        // Assert
        _mockPersonRepository.Verify(x => x.UpdateAsync(person), Times.Once);
    }
}