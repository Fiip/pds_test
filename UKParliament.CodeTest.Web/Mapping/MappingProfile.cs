using AutoMapper;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Person, PersonViewModel>().
                ForMember(dst => dst.DepartmentName, opt => opt.MapFrom((src, dst, ctx) => { return src.Department?.Name; }));

            CreateMap<PersonUpsertViewModel, Person>();

            CreateMap<Department, DepartmentViewModel>();
        }
    }
}
