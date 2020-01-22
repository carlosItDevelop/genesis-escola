using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.Api.ViewModel
{
    public class ResetPasswordViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


    }
}
