using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Models
{
    public class Message<T>
    {
        public bool IsSuccess { get; set; }

        public string[] ReturnMessage { get; set; }

        public T Data { get; set; }
    }
}
