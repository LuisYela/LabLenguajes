//Programa desarrollado por Luis Javier Yela Quijada
//Basado en la catedra del ingeniero Erick Navarro

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_primer_analizador_lexico
{
    class AnalizadorLexico
    {
        //Variable que representa la lista de tokens
        private LinkedList<Token> Salida;
        //Variable que representa el estado actual
        private int estado;
        //Variable que representa el lexema que actualmente se esta acumulando
        private String auxlex;

        public LinkedList<Token> escanear(String entrada)
        {
            //Le agrego caracter de fin de cadena porque hay lexemas que aceptan con el primer caracter del siguiente lexema 
            //y si este caracter no existe entonces perdemos el lexema
            entrada = entrada + "#";
            Salida = new LinkedList<Token>();
            estado = 0;
            auxlex="";
            Char c;
            //Ciclo que recorre de izquierda a derecha caracter por caracter la cadena de entrada
            for (int i = 0; i <=entrada.Length-1; i++)
            {
                c = entrada.ElementAt(i);
                //Switch en el que cada caso representa cada uno de los estados del conjunto de estados
                switch (estado)
                {
                    case 0:
                        //Para cada caso(o estado) hay un if elseif elseif... else que representan el conjunto de transiciones que
                        //salen de dicho estado, por ejemplo, estando en el estado 0 si el caracter reconocido es un dígito entonces,
                        //pasamos al estado 1 y acumulamos el caracter reconocido en auxLex, que es el auxiliar de lexemas.
                        if (Char.IsDigit(c))
                        {
                            estado = 1;
                            auxlex += c;
                        }else if (c.CompareTo('+') == 0)
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.SIGNO_MAS);
                        }
                        else if (c.CompareTo('-') == 0)
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.SIGNO_MEN);
                        }
                        else if (c.CompareTo('*') == 0)
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.SIGNO_POR);
                        }
                        else if (c.CompareTo('/') == 0)
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.SIGNO_DIV);
                        }
                        else if (c.CompareTo('^') == 0)
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.SIGNO_POW);
                        }
                        else if (c.CompareTo('(') == 0)
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.PARENTESIS_IZQ);
                        }
                        else if (c.CompareTo(')') == 0)
                        {
                            auxlex += c;
                            agregarToken(Token.Tipo.PARENTESIS_DER);
                        }
                        else
                        {
                            if(c.CompareTo('#')==0 && i == entrada.Length - 1)
                            {
                                //Hemos concluido el análisis léxico.
                                Console.WriteLine("Hemos concluido el analiss con exito");
                            }
                            else
                            {
                                Console.WriteLine("Error lexico con: "+ c);
                                estado = 0;
                            }
                        }
                        break;
                    case 1:
                        if (Char.IsDigit(c))
                        {
                            estado = 1;
                            auxlex += c;
                        }else if (c.CompareTo('.') == 0)
                        {
                            estado = 2;
                            auxlex += c;
                        }
                        else
                        {
                            agregarToken(Token.Tipo.NUMERO_ENTERO);
                            i -= 1;
                        }
                        break;
                    case 2:
                        if (Char.IsDigit(c))
                        {
                            estado = 3;
                            auxlex += c;
                        }
                        else
                        {
                            Console.WriteLine("Error lexico con: " + c + " desdpues del punto se esperaban mas numeros");
                            estado = 0;
                        }
                        break;
                    case 3:
                        if (Char.IsDigit(c))
                        {
                            estado = 3;
                            auxlex += c;
                        }
                        else
                        {
                            agregarToken(Token.Tipo.NUMERO_REAL);
                            i -= 1;
                        }
                        break;
                }
            }
            return Salida;
        }
        public void agregarToken(Token.Tipo tipo)
        {
            Salida.AddLast(new Token(tipo, auxlex));
            auxlex = "";
            estado=0;
        }
        //impresion de lista de tokens
        public void imprimirListaToken(LinkedList<Token> lista)
        {
            foreach (Token item in lista)
            {
                Console.WriteLine(item.GetTipoString() + " <--> " + item.Getval());
            }
        }

    }
}
