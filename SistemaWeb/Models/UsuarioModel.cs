using SistemaContatos.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaContatos.Models
{
    public class UsuarioModel
    {
        public int id {  get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string nome { get; set; }
        [Required(ErrorMessage = "O campo Login é obrigatório")]
        public string login {get; set; }
        [Required(ErrorMessage = "O campo E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um E-mail válido")]
        public string email { get; set; }
        [Required(ErrorMessage = "O campo Perfil é obrigatório")]
        public PerfilEnum Perfil { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public bool SenhaValida(string Senha)
        {
            return senha == Senha;
        }

    }
}
