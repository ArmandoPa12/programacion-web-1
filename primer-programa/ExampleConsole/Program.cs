using System;
using System.Collections.Generic;
using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Introduccion
{
    internal class Program
    {
        // String // string 
        // int  // int32 // int64
        // decimal 
        // float
        // bool
        // double
        // char
        // byte
        //clases 
        //
        public static int gradosCentigradosAFeherenhit(int grados)
        {

            return grados + 32;
        }
        //921 = 1      444=3
        public static int ContarDigitosPares(int n)
        {
            int contador = 0;
            if (n == 0)
                return 1;


            while (n > 0)
            {
                int d = n % 10;
                n /= 10;
                if (d % 2 == 0) {
                    contador++;
                }

            }

            return contador;
        }

        public static int ContarDigitosImpares(int n)
        {
            int contador = 0;
            if (n == 0)
                return 1;


            while (n > 0)
            {
                int d = n % 10;
                n /= 10;
                if (d % 2 == 1)
                {
                    contador++;
                }

            }

            return contador;
        }

        public static int mayorde5Numeros(int a, int b, int c, int d, int e) {

            int mayor = a;
            if (mayor < b) { 
             mayor = b;
            }
            if (mayor < c)
            {
                mayor = c;
            }
            if (mayor < d)
            {
                mayor = d;
            }
            if (mayor < e)
            {
                mayor = e;
            }

            return mayor;
        }
        public static void Mayor5()
        {
            try
            {
                String entrada = "";
                Console.WriteLine("Mayor de 5 numeros");
                Console.Write("Ingresa a: ");
                entrada = Console.ReadLine();
                int a = int.Parse(entrada);
                Console.Write("Ingresa b : ");
                entrada = Console.ReadLine();
                int b = int.Parse(entrada);
                Console.Write("Ingresa c : ");
                entrada = Console.ReadLine();
                int c = int.Parse(entrada);
                Console.Write("Ingresa d : ");
                entrada = Console.ReadLine();
                int d = int.Parse(entrada);
                Console.Write("Ingresa e : ");
                entrada = Console.ReadLine();
                int e = int.Parse(entrada);

                int mayor = mayorde5Numeros(a, b, c, d, e);
                Console.WriteLine($"El mayor es {mayor}");
            }
            catch (Exception ex) {
                Console.Error.WriteLine(ex.ToString());
            }
            
        }
        public static void ContarParesOpcion()
        {
            Console.WriteLine("Contar pares del numero");
            Console.WriteLine("Ingresa el numero n :");

            try
            {
                String entrada = Console.ReadLine();
                int n = int.Parse(entrada);
                int count = ContarDigitosPares(n);
                Console.WriteLine($"El numero {n} tiene {count} digitos pares");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }

        public static void areaCirculo()
        {
            Console.WriteLine("Vamos a calcular el area de un circulo");
            Console.WriteLine("Ingresa el radio :");

            try
            {
                String entrada = Console.ReadLine();
                Double radio = Double.Parse(entrada);
                Double valor = Math.PI * Math.Pow(radio, 2);
                Console.WriteLine($"El area de un circulo con radio {radio} es de {valor}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }

        public static void perimetroRect()
        {
            Console.WriteLine("Vamos a calcular el perimetro de un rectangulo");
            

            try
            {
                Console.WriteLine("Ingresa la base :");
                String entrada = Console.ReadLine();
                Double basee = Double.Parse(entrada);
                Console.WriteLine("Ingresa la altura :");
                entrada = Console.ReadLine();
                Double altura = Double.Parse(entrada);
                Double valor = (basee * 2) +  (altura * 2);
                Console.WriteLine($"El perimetro es de {valor}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }

        public static void ContarImparesOpcion()
        {
            Console.WriteLine("Contar pares del numero");
            Console.WriteLine("Ingresa el numero n :");

            try
            {
                String entrada = Console.ReadLine();
                int n = int.Parse(entrada);
                int count = ContarDigitosImpares(n);
                Console.WriteLine($"El numero {n} tiene {count} digitos impares");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }

        public static bool EsPrimo(int numero)
        {
            if (numero <= 1)
            {
                return false;
            }
            for (int i = 2; i < numero; i++)
            {
                if (numero % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static void Primo()
        {
            Console.WriteLine("Vamos a saber si el numero es primo");
            Console.WriteLine("Ingresa el numero n :");
            try
            {
                String entrada = Console.ReadLine();
                int n = int.Parse(entrada);
                Boolean primo = EsPrimo(n);
                if (primo == false)
                {
                    Console.WriteLine($"el numero {n}No es un numero Primo");
                }
                else
                {
                    Console.WriteLine($"el numero {n} es un numero Primo");
                }
                    
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }

        public static void Conversor()
        {
            Console.WriteLine("Conversor de grados centrigrados a farenhit");
            Console.WriteLine("Ingresa la temperatura:");

            try
            {
                String entrada = Console.ReadLine();
                int temperatura = int.Parse(entrada);
                int temperaturaF = gradosCentigradosAFeherenhit(temperatura);
                Console.WriteLine($"La temperatura en grados F es {temperaturaF}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }

        public static void factorial()
        {
            Console.WriteLine("Vamos a sacar el factorial de un numero");
            Console.WriteLine("Ingresa un numero cualquiera:");

            try
            {
                String entrada = Console.ReadLine();
                int fact = int.Parse(entrada);
                int res = 1;
                for (int i = 1; i < fact+1; i++)
                {
                    res = res * i;
                }


                Console.WriteLine($"El factorial de {fact} es {res}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }

        public static void Menu()
        {
            Console.WriteLine(@"Menu de opciones 
            1. Conversor 
            2. Mayor de 5 numeros
            3. Contar pares del numero
            4. Calcular el area de un circulo
            5. Perimetro de un rectangulo
            6. Contar digitos impares
            7. Es primo
            8. Factorial de N
            0. Salir");
        }

        static void Main(string[] args)
        {

            while (true) {
                Menu();
                String opcion = Console.ReadLine();
                if (string.IsNullOrEmpty(opcion))
                    continue;
                opcion = opcion.Trim();
                if ("1".Equals(opcion))
                {
                    Conversor();
                    continue;
                }
                if ("2".Equals(opcion)) {
                    Mayor5();
                    continue;
                }
                if ("3".Equals(opcion))
                {
                    ContarParesOpcion();
                    continue;
                }
                if ("4".Equals(opcion))
                {
                    areaCirculo();
                    continue;
                }
                if ("5".Equals(opcion))
                {
                    perimetroRect();
                    continue;
                }
                if ("6".Equals(opcion))
                {
                    ContarImparesOpcion();
                    continue;
                }
                if ("7".Equals(opcion))
                {
                    Primo();
                    continue;
                }
                if ("8".Equals(opcion))
                {
                    factorial();
                    continue;
                }
                if ("0".Equals(opcion)) {
                    Console.WriteLine("Adios!");
                    break;
                }
            }
        }
    }
}