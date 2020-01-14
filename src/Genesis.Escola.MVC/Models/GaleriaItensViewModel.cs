﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Escola.MVC.Models
{
    public class GaleriaItensViewModel
    {
        public GaleriaItensViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Titulo")]
        public string Titulo { get; set; }
        [Display(Name = "Descrição Resumida")]
        public string Resumo { get; set; }
        public Guid GaleriaId { get; set; }
        public byte[] ImagemUpload { get; set; }
        public virtual GaleriaViewModel Galeria { get; set; }

        [Display(Name = "Imagem")]
        public string CaminhoImagem { get; set; }
    }
}
