using System.ComponentModel.DataAnnotations;

namespace UKParliament.CodeTest.Web.ViewModels;

public class PersonUpsertViewModel
{
    [Required]
    public required string FirstName { get; set; }
    
    [Required]
    public required string LastName { get; set; }
    
    [Required]
    public required DateOnly BirthDate { get; set; }

    [Required]
    public required int DepartmentId { get; set; }
}