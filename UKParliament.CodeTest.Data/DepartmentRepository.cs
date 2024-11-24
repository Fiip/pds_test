using Microsoft.EntityFrameworkCore;

namespace UKParliament.CodeTest.Data;

public class DepartmentRepository(PersonManagerContext context) : BaseRepository<Department>(context)
{
    protected override DbSet<Department> DbSet => context.Departments;
}