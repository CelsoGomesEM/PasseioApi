using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passeio.Negocio.Models
{
    public class Lugar : Entity
    {
        public string Nome { get; set; }
        public string Localizacao { get; set; }
        public string UrlFoto { get; set; }
        public int Avaliacao { get; set; }
        public Categoria Categoria { get; set; }
        public Guid CategoriaId { get; set; }
    }
}
