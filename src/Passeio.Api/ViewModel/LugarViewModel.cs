using Microsoft.AspNetCore.Mvc.ModelBinding;
using Passeio.Negocio.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Passeio.Api.ViewModel
{
    public class LugarViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Localizacao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string UrlFoto { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Avaliacao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid CategoriaId { get; set; }

        public string Categoria { get; set; } = string.Empty;
    }
}
