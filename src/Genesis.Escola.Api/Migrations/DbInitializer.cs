using Genesis.Escola.Api.Data;
using Genesis.Escola.Api.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace Genesis.Escola.Api.Migrations
{
    public class DbInitializer
    {
        public static void Iniciar(ApplicationDbContext _contexto, UserManager<UsuarioViewModel> userManager, RoleManager<IdentityRoleViewModel> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedRoles
            (RoleManager<IdentityRoleViewModel> roleManager)
        {

            if (!roleManager.RoleExistsAsync
                ("Administrator").Result)
            {
                IdentityRoleViewModel role = new IdentityRoleViewModel();
                role.Name = "Administrator";
                role.Descricao = "Executar todas as operações.";
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
                ("Aluno").Result)
            {
                IdentityRoleViewModel role = new IdentityRoleViewModel();
                role.Name = "Aluno";
                role.Descricao = "Executar as operações do Aluno";
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
                ("Professor").Result)
            {
                IdentityRoleViewModel role = new IdentityRoleViewModel();
                role.Name = "Professor";
                role.Descricao = "Executar as operações do Professor";
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
                ("Usuario").Result)
            {
                IdentityRoleViewModel role = new IdentityRoleViewModel();
                role.Name = "Professor";
                role.Descricao = "Executar as operações do Usuario";
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }
        }

        public static void SeedUsers
            (UserManager<UsuarioViewModel> userManager)
        {
            if (userManager.FindByNameAsync
                    ("Admin").Result == null)
            {
                UsuarioViewModel user = new UsuarioViewModel();
                user.UserName = "Admin";
                user.Email = "ucaneto@hotmail.com";
                user.NomeCompleto = "Administrador do Sistema";

                IdentityResult result = userManager.CreateAsync
                    (user, "uL963852@@").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        "Administrator").Wait();
                }
            }
        }
    }
}
