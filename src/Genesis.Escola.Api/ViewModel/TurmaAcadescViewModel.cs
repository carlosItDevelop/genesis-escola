using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.Api.ViewModel
{
    public class TurmaAcadescViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Serie { get; set; }
        public string Turma { get; set; }
        public string Turno { get; set; }
        public string Nome { get; set; }
        public string Ciclo { get; set; }

    }
}
