using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Models
{
    public class RetornoApi
    {
        public bool Success { get; set; }
        public string[] errors { get; set; }
    }
}
