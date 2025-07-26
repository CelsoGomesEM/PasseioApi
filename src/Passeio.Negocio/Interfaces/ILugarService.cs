using Passeio.Negocio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passeio.Negocio.Interfaces
{
    public interface ILugarService
    {
        Task Adicionar(Lugar lugar);
        Task Atualizar(Lugar lugar);
        Task Remover(Guid id);
    }
}
