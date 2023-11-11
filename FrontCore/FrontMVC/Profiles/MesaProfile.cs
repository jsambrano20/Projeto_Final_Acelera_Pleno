using AutoMapper;
using FrontMVC.Models.Mesa;

namespace FrontMVC.Profiles
{
    public class MesaProfile : Profile
    {
        public MesaProfile()
        {
            CreateMap<MesaModel, MesaIncluir>().ReverseMap();
            CreateMap<MesaModel, MesaAlterar>().ReverseMap();
            CreateMap<MesaModel, OcuparMesa>().ReverseMap();
            CreateMap<MesaModel, OcuparMesaPost>().ReverseMap();
        }
    }
}
