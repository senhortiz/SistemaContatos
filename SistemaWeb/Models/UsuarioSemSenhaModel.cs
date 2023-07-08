using SistemaContatos.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaContatos.Models
{
    public class UsuarioSemSenhaModel
    {
        public int id {  get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string nome { get; set; }
        [Required(ErrorMessage = "O campo Login é obrigatório")]
        public string login {get; set; }

        [Required(ErrorMessage = "O campo email é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um E-mail válido")]
        public string email { get; set; }
        [Required(ErrorMessage = "O campo Perfil é obrigatório")]
        public PerfilEnum Perfil { get; set; }
    }
}
