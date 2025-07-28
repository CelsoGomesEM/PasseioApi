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
            CreateMap<Lugar, LugarViewModel>().ReverseMap();
        }
    }
}
