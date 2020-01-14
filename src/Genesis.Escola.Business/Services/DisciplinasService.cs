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
    public class DisciplinasService : BaseService, IDisciplinasService
    {
        #region Variaveis
        private readonly IDisciplinasRepositorio _disciplinasRepositorio;
        #endregion

        #region Construtor
        public DisciplinasService(IDisciplinasRepositorio disciplinasRepositorio,
                            INotificador notificador) : base(notificador)
        {
            _disciplinasRepositorio = disciplinasRepositorio;
        }
        #endregion

        #region Adicionar
        public async Task<bool> Adicionar(Disciplinas disciplinas)
        {
            // Validar o Estado da Entidade
            if (!ExecutarValidacao(new DisciplinasValidacao(), disciplinas)) return false;
            await _disciplinasRepositorio.Adicionar(disciplinas);
            return true;
        }
        #endregion

        #region Atualizar
        public async Task<bool> Atualizar(Disciplinas disciplinas)
        {
            if (!ExecutarValidacao(new DisciplinasValidacao(), disciplinas)) return false;
            await _disciplinasRepositorio.Atualizar(disciplinas);
            return true;
        }
        #endregion

        #region Excluir
        public async Task<bool> Remover(Guid id)
        {
            await _disciplinasRepositorio.Remover(id);
            return true;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _disciplinasRepositorio.Dispose();
        }
        #endregion
    }
}
