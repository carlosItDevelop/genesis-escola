using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Models
{
    public class CursoAcadescViewModel
    {
        //public CursoAcadesc()
        //{
        //    Id = Guid.NewGuid();
        //}

        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public int NumSeries { get; set; }

    }
}
