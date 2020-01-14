using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Enumeradores
{
    public class Enumeradores
    {
        public enum EnumCurso
        {
            EducacaoInfantil=1,
            FundamentalI=2,
            FundamentalII=3,
            Tecnico=4,
            EnsinoMedio=5
        };

        public enum EnumAtivoInativo
        {
            Ativo = 1,
            Inativo = 2,
        };
    }
}
