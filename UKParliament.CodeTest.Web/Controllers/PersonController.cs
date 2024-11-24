using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController(IPersonService personService, IMapper mapper) : ControllerBase
{
    [SwaggerOperation("Creates a new person")]
    [SwaggerResponse((int)HttpStatusCode.OK, "The person was successfully created")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PersonUpsertViewModel model)
    {
        var person = mapper.Map<Person>(model);
        await personService.CreateAsync(person);
        return Ok();
    }

    [Route("{id:int}")]
    [SwaggerOperation("Return a person for a given id")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "No person found for given id")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Person data", typeof(PersonViewModel))]
    [HttpGet]
    public ActionResult<PersonViewModel> GetById(int id)
    {
        var person = personService.GetById(id);
        if(person == null) return NotFound();
        var personModel = mapper.Map<PersonViewModel>(person);
        return Ok(personModel);
    }

    [HttpGet]
    [SwaggerOperation("Return a list of persons")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Person data", typeof(List<PersonViewModel>))]
    public ActionResult<PersonViewModel> List()
    {
        var persons = personService.List();
        var personModels = persons.Select(mapper.Map<PersonViewModel>);
        return Ok(personModels);
    }

    [Route("{id:int}")]
    [SwaggerOperation("Updates a person")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "No person found for given id")]
    [SwaggerResponse((int)HttpStatusCode.OK, "The person was successfully updated")]
    [HttpPut]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody]PersonUpsertViewModel model)
    {
        var existingPerson = personService.GetById(id);
        if(existingPerson == null) return NotFound();

        var person = mapper.Map<Person>(model);
        person.Id = id;
        
        await personService.UpdateAsync(person);
        return Ok();
    }
}