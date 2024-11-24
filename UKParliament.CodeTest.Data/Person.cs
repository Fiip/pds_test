namespace UKParliament.CodeTest.Data;

public class Person : BaseEntity
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required DateOnly BirthDate { get; set; }

    public required int DepartmentId { get; set; }
}