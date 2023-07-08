using System.ComponentModel.DataAnnotations;

namespace SistemaWeb.Models
{
    public class ContatoModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string nome { get; set; }
        [Required(ErrorMessage = "O campo E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um E-mail válido")]
        public string email { get; set; }
        [Required(ErrorMessage = "O campo telefone é obrigatório")]
        [Phone(ErrorMessage = "Digite um Telefone válido")]
        public string telefone { get; set; }


    }
}
