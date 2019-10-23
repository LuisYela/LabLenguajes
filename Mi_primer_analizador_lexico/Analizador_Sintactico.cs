//Programa desarrollado por Luis Javier Yela Quijada
//Basado en la catedra del ingeniero Erick Navarro

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_primer_analizador_lexico
{
    class Analizador_Sintactico
    {
        //Variable que se usa como indice para recorrer la lista de tokens
        int controlToken;
        /*Variable que representa el caracter de anticipacion que posee el parser para ralizar el analisis,
        en este caso se desarrollo un analizador LL1 (analizador predictivo recursivo),
        con solo un caracter(token) de anticipacion.*/
        Token tokenActual;
        //Lista de tokens que el parser recibe del analizador lexico
        LinkedList<Token>listaTok;
        /*El primer paso es generar una gramatica no ambigua, utilizaremos la gramatica de
        las expreciones aritmeticas vista en clase, que respeta la precedencia de operadores
        E->E+T
        E->E-T
        E->T
        T->T*F
        T->T/F
        T->F
        F->(E)
        F->numero

        Para poderla implementar dado que es LL1 debemos quitar la recursividad por la izquierda 
        al quitar la recursividad por la izquiera con la regla vista en clase obtenemos:

            Gramática que resuelve el problema:
            
            E-> T EP
            EP-> + T EP
            | - T EP
            | EPSILON
            T->F TP
            TP-> * F TP
            | / F TP
            | EPSILON
            F->  (E)
            | NUMERO
        
        Para todo no terminal del lado izquierdo de las produccion se crea un método
        Para cada no terminal del lado derecho de las producciones se hace una llamada al metodo correspondiente
        y para cada terminal del lado derecho de las producciones se hace una llamada
        al metodo emparejar(match) enviando como parametro el terminal
        */
        public void parsear(LinkedList<Token> tokens)
        {
            this.listaTok = tokens;
            controlToken = 0;
            tokenActual=listaTok.ElementAt(controlToken);
            //Llamada al no terminal inicial
            E();
        }
        public void E()
        {
            //E-> T EP
            T();
            EP();
        }
        public void EP()
        {
            if (tokenActual.getTipo()==Token.Tipo.SIGNO_MAS)
            {
                //EP-> + T EP
                emparejar(Token.Tipo.SIGNO_MAS);
                T();
                EP();
            }
            else if (tokenActual.getTipo() == Token.Tipo.SIGNO_MEN)
            {
                //EP-> - T EP
                emparejar(Token.Tipo.SIGNO_MEN);
                T();
                EP();
            }
            else
            {
                // EP-> EPSILON
                // Para esta producción de EP en epsilon (cadena vacía), simplemente no se hace nada.
            }
        }
        public void T()
        {
            // T->F TP
            F();
            TP();
        }
        public void TP()
        {
            if (tokenActual.getTipo() == Token.Tipo.SIGNO_POR)
            {
                // TP-> * F TP
                emparejar(Token.Tipo.SIGNO_POR);
                F();
                TP();
            }
            else if (tokenActual.getTipo() == Token.Tipo.SIGNO_DIV)
            {
                // TP-> / F TP
                emparejar(Token.Tipo.SIGNO_DIV);
                F();
                TP();
            }
            else
            {
                // TP-> EPSILON
            }
        }
        public void F()
        {
            if (tokenActual.getTipo() == Token.Tipo.PARENTESIS_IZQ)
            {
                //F->  (E)
                emparejar(Token.Tipo.PARENTESIS_IZQ);
                E();
                emparejar(Token.Tipo.PARENTESIS_DER);
            }
            else
            {
                //F->  NUMERO
                emparejar(Token.Tipo.NUMERO_ENTERO);
            }
        }
        /*
         * A continuación se programa el metodo emparejar(match)
         * 
         * Este metodo compara la entradda en la lista de tokens, es decir el tokenActual con lo que deberia
         * venir, que es lo que se pasa como parametro, es decir "tip".
         * 
         * Si "lo que viene" no es igual a "lo que deberia de venir", entonces se reporta el error,
         * de lo contrario si no hemos llegado al final de la lista de tokens pasamos a analizar el 
         * siguiente token.
         */
        public void emparejar(Token.Tipo tip)
        {
            if (tokenActual.getTipo()!=tip)
            {
                //ERROR si no viene lo que deberia
                Console.WriteLine("Error se esperaba "+ getTipoParaError(tip));
            }
            //como ya se dijo si no es el ultimo token, entonces incrementamos en uno el indice de controlToken
            //y damos un nuevo valor a tokenActual, que sera el suiguiente token en la lista.
            if (tokenActual.getTipo()!=Token.Tipo.ULTIMO)
            {
                controlToken += 1;
                tokenActual = listaTok.ElementAt(controlToken);
            }
        }
        //Este metodo solo nos devuelve el texto correspondiente al token esperado,
        //en caso de encontrar errores.
        public String getTipoParaError(Token.Tipo tip)
        {
            switch (tip)
            {
                case Token.Tipo.NUMERO_ENTERO:
                    return ("NUMERO");
                case Token.Tipo.SIGNO_MAS:
                    return ("MAS");
                case Token.Tipo.SIGNO_MEN:
                    return("MENOS");
                case Token.Tipo.SIGNO_POR:
                    return ("por");
                case Token.Tipo.SIGNO_DIV:
                    return ("DIV");
                case Token.Tipo.PARENTESIS_IZQ:
                    return ("PAR_ABRE");
                case Token.Tipo.PARENTESIS_DER:
                    return ("PAR_CIERRA");
                case Token.Tipo.ULTIMO:
                    return ("ultimo");
                default:
                    return ("Desconocido");
            }
        }
    }
}
