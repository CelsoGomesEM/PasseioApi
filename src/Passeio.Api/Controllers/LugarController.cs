using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Passeio.Api.Extensions;
using Passeio.Api.ViewModel;
using Passeio.Data.Repository;
using Passeio.Negocio.Interfaces;
using Passeio.Negocio.Models;
using Passeio.Negocio.Services;

namespace Passeio.Api.Controllers
{
    [Route("api/[controller]")]
    public class LugarController : MainController
    {
        private readonly ILugarRepository _lugarrepository;
        private readonly ILugarService _lugarservice;
        private readonly IMapper _mapper;
        public LugarController(INotificador notificador, 
                               IUser appuser,
                               IMapper mapper, ILugarRepository repository, ILugarService lugarservice) : base(notificador, appuser)
        {
            _mapper = mapper;
            _lugarrepository = repository;
            _lugarservice = lugarservice;
        }

        [HttpGet]
        public async Task<IEnumerable<LugarViewModel>> ObterTodos()
        {
            var lugares = _mapper.Map<IEnumerable<LugarViewModel>>(await _lugarrepository.ObterTodos());
            return lugares;
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LugarViewModel>> ObterPorId(Guid id)
        {
            var categoria = await _lugarrepository.ObterPorId(id);

            if (categoria == null)
                return NotFound();

            var categoriaViewModel = _mapper.Map<LugarViewModel>(categoria);

            return Ok(categoriaViewModel);
        }

        //[ClaimsAuthorize("Admin", "Geral")]
        [HttpPost]
        public async Task<ActionResult<LugarViewModel>> Adicionar(LugarViewModel lugarViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var lugar = _mapper.Map<Lugar>(lugarViewModel);

            await _lugarservice.Adicionar(lugar);

            return CustomResponse(lugarViewModel);
        }
    }
}
