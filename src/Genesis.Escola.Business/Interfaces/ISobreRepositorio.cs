using Genesis.Escola.Business.Interfaces.Generica;
using Genesis.Escola.Business.Models;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces
{
    public interface ISobreRepositorio : IRepositorioGenerico<Sobre>
    {
       Task<Sobre> PegarDadosSobre();
    }
}
