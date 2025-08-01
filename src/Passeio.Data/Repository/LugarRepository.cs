using Passeio.Data.Context;
using Passeio.Negocio.Interfaces;
using Passeio.Negocio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Passeio.Data.Repository
{
    public class LugarRepository : Repository<Lugar>, ILugarRepository
    {
        public LugarRepository(ApiDBContext db) : base(db){}

        public override async Task<List<Lugar>> ObterTodos()
        {
            return await Db.Lugares
                .AsNoTracking()
                .Include(l => l.Categoria) // <- Inclui os dados da Categoria
                .ToListAsync();
        }

        public async Task<bool> ExisteLugarComCategoria(Guid categoriaId)
        {
            return await Db.Lugares.AnyAsync(l => l.CategoriaId == categoriaId);
        }
    }
}
