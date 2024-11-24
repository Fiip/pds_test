using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services;

public class PersonService(IRepository<Person> repository, IRepository<Department> departmentRepository) : IPersonService
{
    public async Task CreateAsync(Person p)
    {
        await repository.CreateAsync(p);
    }

    public Person? GetById(int id)
    {
        return repository.GetById(id);
    }

    public List<Person> List()
    {
        var persons = repository.List();
        var departments = departmentRepository.List();
        foreach (var person in persons)
        {
            person.Department = departments.SingleOrDefault(d => d.Id == person.DepartmentId);
        }

        return persons;
    }

    public async Task UpdateAsync(Person p)
    {
        await repository.UpdateAsync(p);
    }
}