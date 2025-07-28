using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Passeio.Api.ViewModel;
using Passeio.Negocio.Interfaces;
using Passeio.Negocio.Models;
using System.Collections.Generic;

namespace Passeio.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoriasController : MainController
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository categoriaRepository, 
                                    IMapper mapper,
                                    ICategoriaService categoriaService)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoriaViewModel>> ObterTodos()
        {
            var categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos());
            return categorias;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoriaViewModel>> ObterPorId(Guid id)
        {
            var categoria = await _categoriaRepository.ObterPorId(id);

            if (categoria == null)
                return NotFound();

            var categoriaViewModel = _mapper.Map<CategoriaViewModel>(categoria);

            return Ok(categoriaViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaViewModel>> Adicionar(CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var categoria = _mapper.Map<Categoria>(categoriaViewModel);

            await _categoriaService.Adicionar(categoria);

            return Ok(categoria);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CategoriaViewModel>> Atualizar(Guid id, CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var categoria = _mapper.Map<Categoria>(categoriaViewModel);

            await _categoriaService.Atualizar(categoria);

            return Ok(categoria);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CategoriaViewModel>> Deletar(Guid id)
        {
            var categoria = await _categoriaRepository.ObterPorId(id);

            if (categoria == null)
                return NotFound();

            await _categoriaRepository.Remover(id);

            return Ok(categoria);
        }

    }
}
