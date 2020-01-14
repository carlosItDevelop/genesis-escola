using Genesis.Escola.Business.Interfaces.Generica;
using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces
{
    public interface IGaleriaItensRepositorio : IRepositorioGenerico<GaleriaItens>
    {
        Task <IEnumerable<GaleriaItens>> PegarFotos(Guid IdGaleria);
    }
}
