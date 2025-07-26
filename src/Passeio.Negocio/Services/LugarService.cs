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
    public class LugarService : BaseService, ILugarService
    {
        private readonly ILugarRepository _lugarRepository;

        public LugarService(ILugarRepository lugarRepository,
                                INotificador notificador)
                    : base(notificador)
        {
            _lugarRepository = lugarRepository;
        }

        public async Task Adicionar(Lugar lugar)
        {
            if (!ExecutarValidacao(new LugarValidation(), lugar)) return;

            await _lugarRepository.Adicionar(lugar);
        }

        public async Task Atualizar(Lugar lugar)
        {
            if (!ExecutarValidacao(new LugarValidation(), lugar)) return;

            await _lugarRepository.Atualizar(lugar);
        }

        public async Task Remover(Guid id)
        {
            await _lugarRepository.Remover(id);
        }
    }
}
