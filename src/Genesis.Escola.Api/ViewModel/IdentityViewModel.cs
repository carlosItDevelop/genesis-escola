using Microsoft.AspNetCore.Identity;

namespace Genesis.Escola.Api.ViewModel
{
    public class UsuarioViewModel : IdentityUser
    {
        public string NomeCompleto { get; set; }
        public string Usuario { get; set; }
        public string Permissao { get; set; }
    }

    public class IdentityRoleViewModel : IdentityRole
    {
        public string Descricao { get; set; }
    }
}
