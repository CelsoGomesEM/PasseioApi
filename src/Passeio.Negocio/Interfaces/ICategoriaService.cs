using Passeio.Negocio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passeio.Negocio.Interfaces
{
    public interface ICategoriaService
    {
        Task Adicionar(Categoria categoria);
        Task Atualizar(Categoria categoria);
        Task Remover(Guid id);
    }
}
