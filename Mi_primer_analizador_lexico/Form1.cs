//Programa desarrollado por Luis Javier Yela Quijada
//Basado en la catedra del ingeniero Erick Navarro
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mi_primer_analizador_lexico
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtEntrada.Text = "4*(5+6*(8/2)-7)-1";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String entrada = txtEntrada.Text;
            //Proceso de análisis léxico
            AnalizadorLexico lex = new AnalizadorLexico();
            LinkedList<Token> lTokens = lex.escanear(entrada);
            //Luego del analisis léxico obtenemos como salida una lista de tokens en este caso es lTokens,
            //ahora procedemos a imprimirla para mostrar en consola su contenido.
            lex.imprimirListaToken(lTokens);
            //Añadimos un token de finalisacion a nuestro listado de tokens para saber cuando ya se finalizo el analisis
            lTokens.AddLast(new Token(Token.Tipo.ULTIMO,"ultimo"));
            //Creamos un nuevo analizador sintactico
            Analizador_Sintactico parser = new Analizador_Sintactico();
            //Le mandamos nuestro listado de tokens al analizador sintanctico
            parser.parsear(lTokens);
            Console.WriteLine("FIN!!!");

        }
    }
}
