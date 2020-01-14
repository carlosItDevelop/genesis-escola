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
    public class TurmaAcadescService : BaseService, ITurmaAcadescService
    {
        #region Variaveis
        private readonly ITurmaAcadescRepositorio _turmaRepositorio;
        #endregion

        #region Construtor
        public TurmaAcadescService(ITurmaAcadescRepositorio turmaRepositorio,
                                INotificador notificador) : base(notificador)
        {
            _turmaRepositorio = turmaRepositorio;
        }
        #endregion

        #region Adicionar
        public async Task<bool> Adicionar(TurmaAcadesc turma)
        {
            if (!ExecutarValidacao(new TurmaAcadescValidacao(), turma)) return false;
            await _turmaRepositorio.Adicionar(turma);
            return true;
        }
        #endregion

        #region Atualizar
        public async Task<bool> Atualizar(TurmaAcadesc turma)
        {
            if (!ExecutarValidacao(new TurmaAcadescValidacao(), turma)) return false;
            await _turmaRepositorio.Atualizar(turma);
            return true;
        }
        #endregion

        #region Excluir
        public async Task<bool> Remover(Guid id)
        {
            await _turmaRepositorio.Remover(id);
            return true;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _turmaRepositorio.Dispose();
        }
        #endregion
    }
}
