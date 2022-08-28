using System;

//Requerimiento 1.- Grabar la fecha y hora en el log
//Requerimiento 2.- Programar la produccion For -> for(Asignacion Condicion; Incremento) Bloque de instruccones | Intruccion 
//Requerimiento 3.- Programar la produccion Incremento -> Identificador ++ | --
//Requerimiento 4.- Programar la produccion Switch -> switch (Expresion) {Lista de casos}
//Requerimiento 5.- Programar la produccion ListaDeCasos -> case Expresion: listaInstruccionesCase (break;)? (ListaDeCasos)?
//Requerimiento 6.- Incluir en el Switch un default optativo
//Requerimiento 7.- Levantar una excepcion cuando el archivo de prueba.cpp no exista
//Requerimiento 8.- Si el programa a compilar es suma.cpp debera generar un suma.log
//Requerimiento 9.- Es necesario que el error lexico o sintactico indique el numero de liena en el que ocurrio

namespace Sintaxis_1
{
    public class Lenguaje : Sintaxis
    {
        public Lenguaje()
        {

        }
        public Lenguaje(string nombre) : base(nombre)
        {

        }
        //Programa  -> Librerias? Variables? Main
        public void Programa()
        {
            Libreria();
            Variables();
            Main();
        }

        //Librerias -> #include<identificador(.h)?> Librerias?
        private void Libreria()
        {
            if (getContenido() == "#")
            {
                match("#");
                match("include");
                match("<");
                match(Tipos.Identificador);
                if (getContenido() == ".")
                {
                    match(".");
                    match("h");
                }
                match(">");
                if (getContenido() == "#")
                Libreria();
            }
        }

         //Variables -> tipo_dato Lista_identificadores; Variables?
        private void Variables()
        {
            if (getClasificacion() == Tipos.TipoDato)
            {
                match(Tipos.TipoDato);
                Lista_identificadores();
                match(Tipos.FinSentencia);
                Variables();
            }
        }

         //Lista_identificadores -> identificador (,Lista_identificadores)?
        private void Lista_identificadores()
        {
            match(Tipos.Identificador);
            if (getContenido() == ",")
            {
                match(",");
                Lista_identificadores();
            }
        }
        //Bloque de instrucciones -> {lista de intrucciones?}
        private void BloqueInstrucciones()
        {
            match("{");
            if (getContenido() != "}")
            {
                ListaInstrucciones();
            }    
            match("}"); 
        }

        //ListaInstrucciones -> Instruccion ListaInstrucciones?
        private void ListaInstrucciones()
        {
            Instruccion();
            if (getContenido() != "}")
            {
                ListaInstrucciones();
            }
        }

        //ListaInstruccionesCase -> Instruccion ListaInstruccionesCase?
        private void ListaInstruccionesCase()
        {
            Instruccion();
            if (getContenido() != "case" && getContenido() !=  "break" && getContenido() != "default" && getContenido() != "}")
            {
                ListaInstruccionesCase();
            }
        }

        //Instruccion -> Printf | Scanf | If | While | do while | For | Switch | Asignacion
        private void Instruccion()
        {
            if (getContenido() == "printf")
            {
                Printf();
            }
            else if (getContenido() == "scanf")
            {
                Scanf();
            }
            else if (getContenido() == "if")
            {
                If();
            }
            else if (getContenido() == "while")
            {
                While();
            }
            else if(getContenido() == "do")
            {
                Do();
            }
            else if(getContenido() == "for")
            {
                For();
            }
            else if(getContenido() == "switch")
            {
                Switch();
            }
            else
            {
                Asignacion();
            }
        }

        //Asignacion -> identificador = cadena | Expresion;
        private void Asignacion()
        {
            match(Tipos.Identificador);
            match("=");
            if (getClasificacion() == Tipos.Cadena)
            {
                match(Tipos.Cadena);
            }
            else
            {
                Expresion();
            }
            match(";");
        }

        //While -> while(Condicion) bloque de instrucciones | instruccion
        private void While()
        {
            match("while");
            match("(");
            Condicion();
            match(")");
             if (getContenido() == "{") 
            {
                BloqueInstrucciones();
            }
            else
            {
                Instruccion();
            }
        }

        //Do -> do bloque de instrucciones | intruccion while(Condicion)
        private void Do()
        {
            match("do");
            if (getContenido() == "{")
            {
                BloqueInstrucciones();
            }
            else
            {
                Instruccion();
            } 
            match("while");
            match("(");
            Condicion();
            match(")");
            match(";");
        }
        //For -> for(Asignacion Condicion; Incremento) Bloque de instruccones | Intruccion 
        private void For()
        {
            match("for");
            match("(");
            Asignacion();
            Condicion();
            match(";");
            Incremento();
            match(")");
            if (getContenido() == "{")
            {
                BloqueInstrucciones();  
            }
            else
            {
                Instruccion();
            }
        }

        //Incremento -> Identificador ++ | --
        private void Incremento()
        {
            match(Tipos.Identificador);
            if(getContenido() == "+")
            {
                match("++");
            }
            else
            {
                match("--");
            }
        }

        //Switch -> switch (Expresion) {Lista de casos} | (default: )
        private void Switch()
        {
            match("switch");
            match("(");
            Expresion();
            match(")");
            match("{");
            ListaDeCasos();
            if(getContenido() == "default")
            {
                match("default");
                match(":");
                if (getContenido() == "{")
                {
                    BloqueInstrucciones();  
                }
                else
                {
                    Instruccion();
                }
            }
            match("}");
        }

        //ListaDeCasos -> case Expresion: listaInstruccionesCase (break;)? (ListaDeCasos)?
        private void ListaDeCasos()
        {
            match("case");
            Expresion();
            match(":");
            ListaInstruccionesCase();
            if(getContenido() == "break")
            {
                match("break");
                match(";");
            }
            if(getContenido() == "case")
            {
                ListaDeCasos();
            }
        }

        //Condicion -> Expresion operador relacional Expresion
        private void Condicion()
        {
            Expresion();
            match(Tipos.OperadorRelacional);
            Expresion();
        }

        //If -> if(Condicion) bloque de instrucciones (else bloque de instrucciones)?
        private void If()
        {
            match("if");
            match("(");
            Condicion();
            match(")");
            if (getContenido() == "{")
            {
                BloqueInstrucciones();  
            }
            else
            {
                Instruccion();
            }
            if (getContenido() == "else")
            {
                match("else");
                if (getContenido() == "{")
                {
                    BloqueInstrucciones();
                }
                else
                {
                    Instruccion();
                }
            }
        }

        //Printf -> printf(cadena);
        private void Printf()
        {
            match("printf");
            match("(");
            match(Tipos.Cadena);
            match(")");
            match(";");
        }

        //Scanf -> scanf(cadena);
        private void Scanf()    
        {
            match("scanf");
            match("(");
            match(Tipos.Cadena);
            match(")");
            match(";");
        }

        //Main      -> void main() Bloque de instrucciones
        private void Main()
        {
            match("void");
            match("main");
            match("(");
            match(")");
            BloqueInstrucciones();
        }

        //Expresion -> Termino MasTermino
        private void Expresion()
        {
            Termino();
            MasTermino();
        }
        //MasTermino -> (OperadorTermino Termino)?
        private void MasTermino()
        {
            if (getClasificacion() == Tipos.OperadorTermino)
            {
                match(Tipos.OperadorTermino);
                Termino();
            }
        }
        //Termino -> Factor PorFactor
        private void Termino()
        {
            Factor();
            PorFactor();
        }
        //PorFactor -> (OperadorFactor Factor)? 
        private void PorFactor()
        {
            if (getClasificacion() == Tipos.OperadorFactor)
            {
                match(Tipos.OperadorFactor);
                Factor();
            }
        }
        //Factor -> numero | identificador | (Expresion)
        private void Factor()
        {
            if (getClasificacion() == Tipos.Numero)
            {
                match(Tipos.Numero);
            }
            else if (getClasificacion() == Tipos.Identificador)
            {
                match(Tipos.Identificador);
            }
            else
            {
                match("(");
                Expresion();
                match(")");
            }
        }
    }
}