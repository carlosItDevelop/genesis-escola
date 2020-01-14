using Genesis.Escola.Business.Notificacoes;
using System.Collections.Generic;

namespace Genesis.Escola.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
