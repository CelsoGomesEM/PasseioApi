using AutoMapper;
using Passeio.Api.ViewModel;
using Passeio.Negocio.Models;

namespace Passeio.Api.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Exemplo de mapeamento
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();

            CreateMap<LugarViewModel, Lugar>()
                .ForMember(dest => dest.Categoria, opt => opt.Ignore());

            CreateMap<Lugar, LugarViewModel>()
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Nome));
        }
    }
}
