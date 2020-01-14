﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.Api.ViewModel
{
    public class TarefaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Resumo da Tarefa")]
        public string DescricaoResumida { get; set; }

        [Display(Name = "Data Inicial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Data Final")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DataFinal { get; set; }

        [Display(Name = "Tarefa Completa")]
        public string DescricaoCompleta { get; set; }
        public string TurmaId { get; set; }

        public byte[] ImagemUpload { get; set; }

        [Display(Name = "Arquivo PDF")]
        public string CaminhoImagem { get; set; }
    }
}
