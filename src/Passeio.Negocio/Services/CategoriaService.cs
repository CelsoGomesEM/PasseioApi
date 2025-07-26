using Passeio.Negocio.Interfaces;
using Passeio.Negocio.Models;
using Passeio.Negocio.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passeio.Negocio.Services
{
    public class CategoriaService : BaseService, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository,
                                INotificador notificador) 
                    : base(notificador)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task Adicionar(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidation(), categoria)) return;

            await _categoriaRepository.Adicionar(categoria);
        }

        public async Task Atualizar(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidation(), categoria)) return;

            await _categoriaRepository.Atualizar(categoria);
        }

        public async Task Remover(Guid id)
        {
            await _categoriaRepository.Remover(id);
        }
    }
}
