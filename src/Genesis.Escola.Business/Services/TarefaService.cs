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
    public class TarefaService : BaseService, ITarefaService
    {
        private readonly ITarefaRepositorio _tarefaRepositorio;
        public TarefaService(ITarefaRepositorio tarefaRepositorio,
                                INotificador notificador) : base(notificador)
        {
            _tarefaRepositorio = tarefaRepositorio;
        }

        public async Task<bool> Adicionar(Tarefa tarefa)
        {
            // Validar o Estado da Entidade
            if (!ExecutarValidacao(new TarefaValidacao(), tarefa)) return false;

            await _tarefaRepositorio.Adicionar(tarefa);
            return true;
        }

        public async Task<bool> Atualizar(Tarefa tarefa)
        {
            if (!ExecutarValidacao(new TarefaValidacao(), tarefa)) return false;

            await _tarefaRepositorio.Atualizar(tarefa);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            await _tarefaRepositorio.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _tarefaRepositorio.Dispose();
        }
    }
}
