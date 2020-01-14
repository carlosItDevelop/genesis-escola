using Genesis.Escola.Api.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Genesis.Escola.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<UsuarioViewModel, IdentityRoleViewModel, string> //: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
