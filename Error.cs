using System;
using System.IO;

namespace Sintaxis_1
{
    public class Error : Exception
    {
        public Error(string mensaje, StreamWriter log)
        {
            Console.WriteLine(mensaje);
            log.WriteLine(mensaje);
        }
    }
}