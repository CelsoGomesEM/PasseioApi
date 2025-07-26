using Passeio.Data.Context;
using Passeio.Negocio.Interfaces;
using Passeio.Negocio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passeio.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApiDBContext db) : base(db) {}

    }
}
