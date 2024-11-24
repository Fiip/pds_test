using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services;

public class DepartmentService(IRepository<Department> repository) : IDepartmentService
{
    public List<Department> List()
    {
        return repository.List();
    }
}