//Programa desarrollado por Luis Javier Yela Quijada
//Basado en la catedra del ingeniero Erick Navarro
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_primer_analizador_lexico
{
    class Token
    {
        public enum Tipo
        {
            NUMERO_ENTERO,
            NUMERO_REAL,
            SIGNO_MAS,
            SIGNO_MEN,
            SIGNO_POR,
            SIGNO_DIV,
            SIGNO_POW,
            PARENTESIS_IZQ,
            PARENTESIS_DER,
            ULTIMO
        }

        private Tipo tipoToken;
        private String valor;

        public Token(Tipo tipoDelToken, String val)
        {
            this.tipoToken = tipoDelToken;
            this.valor = val;
        }

        public String Getval()
        {
            return valor;
        }

        public Tipo getTipo()
        {
            return this.tipoToken;
        }

        public String GetTipoString()
        {
            switch (tipoToken)
            {
                case Tipo.NUMERO_ENTERO:
                    return "Numero entero";
                case Tipo.NUMERO_REAL:
                    return "Numero real";
                case Tipo.PARENTESIS_DER:
                    return "Parentesis cierra";
                case Tipo.PARENTESIS_IZQ:
                    return "Parentesis abre";
                case Tipo.SIGNO_DIV:
                    return "Signo divicion";
                case Tipo.SIGNO_MAS:
                    return "Signo mas";
                case Tipo.SIGNO_MEN:
                    return "Signo menos";
                case Tipo.SIGNO_POR:
                    return "Signo por";
                case Tipo.SIGNO_POW:
                    return "Signo potecia";
                default:
                    return "Desconocido";
            }
        }

    }
}
