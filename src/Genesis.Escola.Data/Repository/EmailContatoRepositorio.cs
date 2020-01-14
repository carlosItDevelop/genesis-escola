using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Data.Context;
using Genesis.Escola.Data.Repository.Generico;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Data.Repository
{
    public class EmailContatoRepositorio : RepositorioGenerico<EmailContato>, IEmailContatoRepositorio
    {
        public EmailContatoRepositorio(Contexto contexto) : base(contexto) { }
    }
}
