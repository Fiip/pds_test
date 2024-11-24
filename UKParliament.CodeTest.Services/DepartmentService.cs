using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services;

public class PersonService(IRepository<Person> repository) : IPersonService
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
        return repository.List();
    }

    public async Task UpdateAsync(Person p)
    {
        await repository.UpdateAsync(p);
    }
}