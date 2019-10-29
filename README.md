# Mi_Primer_Analizador_lexico
Repositorio del laboratorio de Lenguajes formales impartido por mi en donde se subirán ejemplos y material relacionado con el curso.

Analizadorlexico sencillo utilizando c# de visual studio comunity 2017

Se analiza que todos los caracteres ingresados sean aceptados por una calculadora sencilla agrupandolos en los tokens sihuientes:
Numero_Real

Numero_Entero

Signo_Mas

Signo_Menos

Signo_Por

Signo_Div

Signo_Pow

Parentecis_Izq

Parentecis_Der

ejemplo de cadena valida:

(2+4)-8^3+(12*5-(4/2))


# Mi_Primer_Analizador_Sintactico
Se agrego el analizador sintactico, tipo LL1, basado en lo visto en clase, se agrego el simbolo Ultimo en los tokens, y otros cambios menores en las clases de analisis_lexico y token

