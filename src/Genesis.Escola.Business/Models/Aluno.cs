using Genesis.Escola.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Escola.Business.Models
{
    public class Aluno : Entity
    {
        public Aluno()
        {
        }


        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Curso { get; set; }
        public string Serie { get; set; }
        public string Turma { get; set; }
        public string Turno { get; set; }
        public string Numero { get; set; }
        public DateTime DtNascimento { get; set; }
        public string TextoFinalBoletim { get; set; }
        public string RespNome { get; set; }
        public string RespEndereco { get; set; }
        public string RespCidade { get; set; }
        public string RespEstado { get; set; }
        public string RespCep { get; set; }
        public string RespEmail { get; set; }
        public string Simbolo { get; set; }
        public bool Ativo { get; set; }

        public Guid TurmaId { get; set; }
        //public virtual Turma TurmaSite { get; set; }

    }
}
