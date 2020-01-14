using Genesis.Escola.Business.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using static Genesis.Escola.Business.Enumeradores.Enumeradores;

namespace Genesis.Escola.Business.Models
{
    public class Cursos : Entity
    {
        public string Titulo { get; set; }
        public EnumCurso Curso { get; set; }
        public string Descricao { get; set; }
    }
}
