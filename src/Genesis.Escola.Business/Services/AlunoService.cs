using System;
using System.Threading.Tasks;
using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Interfaces.Services;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Business.Models.Validations;

namespace Genesis.Escola.Business.Services
{
    public class AlunoService : BaseService, IAlunoService
    {
        #region Variaveis
        private readonly IAlunoRepositorio _alunoRepositorio;
        #endregion

        #region Construtor
        public AlunoService(IAlunoRepositorio alunoRepositorio,
                            INotificador notificador) : base(notificador)
        {
            _alunoRepositorio = alunoRepositorio;
        }
        #endregion

        #region Adicionar
        public async Task<bool> Adicionar(Aluno aluno)
        {
            // Validar o Estado da Entidade
            if (!ExecutarValidacao(new AlunoValidacao(), aluno)) return false;
            await _alunoRepositorio.Adicionar(aluno);
            return true;
        }
        #endregion

        #region Atualizar
        public async Task<bool> Atualizar(Aluno aluno)
        {
            if (!ExecutarValidacao(new AlunoValidacao(), aluno)) return false;
            await _alunoRepositorio.Atualizar(aluno);
            return true;
        }
        #endregion

        #region Remover
        public async Task<bool> Remover(Guid id)
        {
            await _alunoRepositorio.Remover(id);
            return true;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _alunoRepositorio.Dispose();
        }
        #endregion
    }
}
