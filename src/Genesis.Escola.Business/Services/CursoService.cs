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
    public class CursoService : BaseService, ICursoService
    {
        #region Variaveis
        private readonly ICursosRepositorio _cursosRepositorio;
        #endregion

        #region Construtor
        public CursoService(ICursosRepositorio cursosRepositorio,
                            INotificador notificador) : base(notificador)
        {
            _cursosRepositorio = cursosRepositorio;
        }
        #endregion

        #region Adicionar
        public async Task<bool> Adicionar(Cursos curso)
        {
            // Validar o Estado da Entidade
            if (!ExecutarValidacao(new CursoValidacao(), curso)) return false;
            await _cursosRepositorio.Adicionar(curso);
            return true;
        }
        #endregion

        #region Atualizar
        public async Task<bool> Atualizar(Cursos curso)
        {
            if (!ExecutarValidacao(new CursoValidacao(), curso)) return false;
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
