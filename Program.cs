using System;
using System.IO;

namespace Sintaxis_1
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Lenguaje a = new Lenguaje("C:\\Users\\marco\\OneDrive\\Escritorio\\Lenguajes y automatas 1\\Sintaxis_1\\examen.cpp");

                //a.Programa();
                while(!a.FinArchivo())
                {
                    a.NextToken();
                }
                a.cerrar();
            }
            catch (Exception e)
            {
                Console.WriteLine("Fin de compilacion");
            }
        }
    }
}