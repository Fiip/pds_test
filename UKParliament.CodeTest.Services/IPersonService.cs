using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services;

public interface IPersonService
{
    Task CreateAsync(Person p);
    Person? GetById(int id);
    List<Person> List();
    Task UpdateAsync(Person p);
}