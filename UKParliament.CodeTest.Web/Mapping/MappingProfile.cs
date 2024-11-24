using AutoMapper;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {

            CreateMap<Person, PersonViewModel>();
            CreateMap<PersonUpsertViewModel, Person>();

            CreateMap<Department, DepartmentViewModel>();
        }
    }
}
