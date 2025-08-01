using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passeio.Api.Extensions;
using Passeio.Api.ViewModel;
using Passeio.Negocio.Interfaces;
using Passeio.Negocio.Models;

namespace Passeio.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class LugaresController : MainController
    {
        private readonly ILugarRepository _lugarrepository;
        private readonly ILugarService _lugarservice;
        private readonly IMapper _mapper;
        public LugaresController(INotificador notificador, 
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
            var lugar = await _lugarrepository.ObterPorId(id);

            if (lugar == null)
                return NotFound();

            var lugarViewModel = _mapper.Map<LugarViewModel>(lugar);

            return Ok(lugarViewModel);
        }

        [HttpGet("filtrar")]
        public async Task<IEnumerable<LugarViewModel>> Filtrar(string nome_like, string categoria)
        {
            var lugares = await _lugarrepository.ObterTodos();

            var lugaresFiltro = lugares.Where(l =>
                (string.IsNullOrEmpty(nome_like) || l.Nome.Contains(nome_like)) &&
                (string.IsNullOrEmpty(categoria) || l.Categoria.Nome == categoria)
            );

            var retorno = _mapper.Map<IEnumerable<LugarViewModel>>(lugaresFiltro);

            return retorno;
        }


        [ClaimsAuthorize("Admin", "Geral")]
        [HttpPost]
        public async Task<ActionResult<LugarViewModel>> Adicionar(LugarViewModel lugarViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var lugar = _mapper.Map<Lugar>(lugarViewModel);

            await _lugarservice.Adicionar(lugar);

            return CustomResponse(lugarViewModel);
        }

        [ClaimsAuthorize("Admin", "Geral")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<LugarViewModel>> Atualizar(Guid id, LugarViewModel lugarViewModel)
        {
            if (id != lugarViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(lugarViewModel);
            }

            if (!ModelState.IsValid) 
                return CustomResponse(ModelState);

            await _lugarservice.Atualizar(_mapper.Map<Lugar>(lugarViewModel));

            return CustomResponse(lugarViewModel);
        }

        [ClaimsAuthorize("Admin", "Geral")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<LugarViewModel>> Excluir(Guid id)
        {
            var lugarViewModel = await _lugarrepository.ObterPorId(id);

            if (lugarViewModel == null) 
                return NotFound();

            await _lugarservice.Remover(id);

            return CustomResponse(lugarViewModel);
        }


    }
}
