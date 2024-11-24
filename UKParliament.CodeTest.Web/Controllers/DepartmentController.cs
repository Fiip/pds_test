using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController(IDepartmentService departmentService, IMapper _mapper) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Return a list of departments")]
    [SwaggerResponse((int)HttpStatusCode.OK, "department data", typeof(List<DepartmentViewModel>))]
    public ActionResult<DepartmentViewModel> List()
    {
        var persons = departmentService.List();
        var personModels = persons.Select(_mapper.Map<DepartmentViewModel>);
        return Ok(personModels);
    }
}