using AutoMapper;
using SampleMvcApp.Entities.Concrete;

namespace SampleMvcApp.WebApp
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                MemberMapping(cfg);
                DepartmentMapping(cfg);
            });
        }

        private static void DepartmentMapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Department, DepartmentCommand>().ReverseMap();
            cfg.CreateMap<Department, DepartmentQuery>().ReverseMap();
        }

        private static void MemberMapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Member, MemberQuery>()
                .ForMember(m => m.NameSurname, opt => opt.ResolveUsing(x => $"{x.Name} {x.Surname}"))
                .ForMember(m => m.IsActive, opt => opt.ResolveUsing(x => (x.IsActive) ? "Evet" : "Hayır"))
                .ReverseMap();

            cfg.CreateMap<Member, MemberCommand>()
                .ForMember(m => m.IsActive, opt => opt.ResolveUsing(x => x.IsActive ? "on" : "off"));

            cfg.CreateMap<MemberCommand, Member>()
                .ForMember(m => m.IsActive, opt => opt.ResolveUsing(x => x.IsActive == "on" ? true : false));
        }
    }
}