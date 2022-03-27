using System.ComponentModel.DataAnnotations;

namespace Web.UI.Models
{
    public class AnuncioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Versao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Quilometragem { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Observacao { get; set; }

    }
}
