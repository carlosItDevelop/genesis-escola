using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Interfaces.Services;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Services
{
    public class CursoAcadescService : BaseService, ICursoAcadescService
    {
        #region Variaveis
        private readonly ICursoAcadescRepositorio _cursosRepositorio;
        #endregion

        #region Construtor
        public CursoAcadescService(ICursoAcadescRepositorio cursosRepositorio,
                            INotificador notificador) : base(notificador)
        {
            _cursosRepositorio = cursosRepositorio;
        }
        #endregion

        #region Adicionar
        public async Task<bool> Adicionar(CursoAcadesc curso)
        {
            // Validar o Estado da Entidade
            if (!ExecutarValidacao(new CursoAcadescValidacao(), curso)) return false;
            await _cursosRepositorio.Adicionar(curso);
            return true;
        }
        #endregion

        #region Atualizar
        public async Task<bool> Atualizar(CursoAcadesc curso)
        {
            if (!ExecutarValidacao(new CursoAcadescValidacao(), curso)) return false;
            await _cursosRepositorio.Atualizar(curso);
            return true;
        }
        #endregion

        #region Excluir
        public async Task<bool> Remover(Guid id)
        {
            await _cursosRepositorio.Remover(id);
            return true;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _cursosRepositorio.Dispose();
        }
        #endregion
    }
}
