using Microsoft.EntityFrameworkCore;

namespace UKParliament.CodeTest.Data;

public class PersonRepository(PersonManagerContext context) : BaseRepository<Person>(context)
{
    protected override DbSet<Person> DbSet => context.People;
}