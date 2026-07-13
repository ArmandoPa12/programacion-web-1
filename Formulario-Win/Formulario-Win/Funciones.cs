using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario_Win
{
    internal class Funciones
    {

        public int contarVocales(String palabra)
        {
            if (string.IsNullOrEmpty(palabra))
            {
                return 0;
            }
            else
            {
                return 0;
            }
        }


        // auxiliares
        private int contador(string palabra, string evaluar)
        {
            if (string.IsNullOrEmpty(palabra))
            {
                return 0;
            }
            
            int contador = 0;
            palabra = palabra.ToLower();
            foreach (char c in palabra)
            {
                if (c.ToString().Contains(evaluar))
                {
                    contador++;
                }
            }
            return contador;
        }
        private int palabras(String palabra)
        {
            if (string.IsNullOrEmpty(palabra))
            {
                return 0;
            }
            int contador = 0;
            palabra = palabra.ToLower();
            foreach (char c in palabra)
            {
                if (c.ToString().Contains(" "))
                {
                    contador++;
                }
            }
            return contador;
        }

    }
}
