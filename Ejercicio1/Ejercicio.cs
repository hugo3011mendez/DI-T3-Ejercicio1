using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1
{
    class Ejercicio
    {
        // Creo una función para pedir la IP del PC :
        static string escribirIP()
        {
            bool verificadaIP = false;
            string ip = "";

            while (!verificadaIP)
            {
                Console.WriteLine("Escribe la IP del ordenador :");
                ip = Console.ReadLine();

                int contadorPuntos = 0;
                int contadorLetras = 0;
                for (int i = 0; i < ip.Length; i++)
                {
                    if (Char.IsLetter(ip[i]))
                    {
                        contadorLetras++;
                    }

                    if (ip[i] == '.')
                    {
                        contadorPuntos++;
                    }
                }



                if (contadorPuntos == 3 && contadorLetras == 0)
                {
                    try
                    {
                        int contador = 0;

                        for (int i = 0; i < ip.Split('.').Length; i++)
                        {
                            if (Convert.ToInt32(ip.Split('.')[i]) < 0 || Convert.ToInt32(ip.Split('.')[i]) > 255)
                            {
                                verificadaIP = false;
                            }
                            else
                            {
                                contador++;
                            }
                        }

                        if (contador == 4)
                        {
                            verificadaIP = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Ha saltado la excepción {0}", e.Message);
                        verificadaIP = false;
                        Console.WriteLine("Formato de IP incorrecto, vuelve a escribirla");
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }
                else
                {
                    verificadaIP = false;
                    Console.WriteLine("Formato de IP incorrecto, vuelve a escribirla");
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }

            return ip;
        }


        // Creo otra función para escribir la memoria RAM en GB :
        static int escribirRAM()
        {
            bool verificado = false;
            int ram = 0;

            try
            {
                while (!verificado)
                {
                    Console.WriteLine("Escribe la cantidad de memoria RAM en GB que tiene el equipo :");
                    ram = Convert.ToInt32(Console.ReadLine());

                    if (ram <= 0)
                    {
                        verificado = false;
                        Console.WriteLine("Has introducido una cantidad inválida de RAM, repite");
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    else
                    {
                        verificado = true;
                    }
                }

                return ram;
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido la excepción " + e.Message);
                throw;
            }

        }

        // Creo la función para introducir un nuevo elemento en la Hashtable, que llamará a los otros dos métodos que validan la pedida de datos :
        static void introducirDato(Hashtable equipos)
        {
            string ip = escribirIP();
            int ram = escribirRAM();

            equipos.Add(ip, ram);
            Console.WriteLine("Los datos del nuevo equipo se han introducido con éxito");
            Console.WriteLine("Escribe Enter para continuar : ");
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadLine();
        }


        // Función para eliminar un elemento de la Hashtable :
        static void eliminarDato(Hashtable equipos)
        {
            bool encontrado = false;

            Console.WriteLine("Escribe la IP del equipo que quiere eliminar : ");
            string ip = Console.ReadLine();

            foreach (string de in equipos.Keys)
            {
                if (de == ip)
                {
                    encontrado = true;
                    Console.WriteLine("La IP que has intoducido está dentro de la Hashtable");
                    Console.WriteLine("Procediendo a eliminar el equipo...");
                    Console.WriteLine("Pulsa Enter para proceder :");
                }
            }

            if (encontrado)
            {
                equipos.Remove(ip);
            }
            else
            {
                Console.WriteLine("No se ha encontrado el elemento que querías eliminar");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.ReadLine();
        }

        // Función para recorrer el hashtable y mostrar todos sus elementos
        static void mostrarColeccion(Hashtable equipos)
        {
            foreach (DictionaryEntry de in equipos)
            {
                Console.WriteLine("El equipo con la IP {0} tiene {1} GB de RAM", de.Key, de.Value);
            }
            Console.WriteLine("Escribe Enter para continuar : ");
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadLine();
        }


        // Función para mostrar el elemento indicado
        static void mostrarElemento(Hashtable equipos)
        {
            bool encontrado = false;
            string ip = "";
            Console.WriteLine("Escribe la IP del equipo que quieres ver :");
            ip = Console.ReadLine();

            foreach (string de in equipos.Keys)
            {
                if (de == ip)
                {
                    encontrado = true;
                    Console.WriteLine("La IP que has intoducido está dentro de la Hashtable");
                }
            }

            if (encontrado)
            {
            Console.WriteLine("El equipo con la IP {0} tiene {1} GB de RAM", ip, equipos[ip]);
            }
            else
            {
                Console.WriteLine("No se ha encontrado el equipo con esa IP");
            }

            Console.WriteLine("Escribe Enter para continuar : ");
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadLine();
        }


        // TODO : Hacer el apartado b)
        static void guardarEnArchivo(String ruta, Hashtable equipos)
        {
            using (StreamWriter sw = new StreamWriter(ruta))
            {
                foreach (DictionaryEntry de in equipos)
                {
                    sw.WriteLine("{0}|{1}", de.Key, de.Value);
                }
            }
        }


        static void cargarDeArchivo(String ruta, Hashtable equipos)
        {
            if (File.Exists(ruta))
            {

                string datos;
                string ip;
                int ram;

                StreamReader sr = new StreamReader(ruta);
                datos = sr.ReadLine();
                while (datos != null)
                {
                    ip = datos.Split('|')[0];
                    ram = Convert.ToInt32(datos.Split('|')[1]);

                    equipos.Add(ip, ram);

                    datos = sr.ReadLine();
                }
                sr.Close();
            }
        }



        static void Main(string[] args)
        {
            Hashtable equipos = new Hashtable();

            cargarDeArchivo("C:\\Users\\Hugo\\Desktop\\Desarrollo de Interfaces\\T3\\infoEj1.txt", equipos);

            int opcion = 0;
            while (opcion !=5)
            {
                
                Console.WriteLine("1- Introducir dato");
                Console.WriteLine("2- Eliminar dato por clave");
                Console.WriteLine("3- Mostrar la colección entera");
                Console.WriteLine("4- Mostrar un elemento de la colección");
                Console.WriteLine("5- Salir");
                Console.WriteLine("Qué quieres hacer?");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        introducirDato(equipos);
                        break;

                    case 2:
                        eliminarDato(equipos);
                        break;

                    case 3:
                        mostrarColeccion(equipos);
                        break;

                    case 4:
                        mostrarElemento(equipos);
                        break;

                    case 5:
                        Console.WriteLine("Adiós!");

                        guardarEnArchivo("C:\\Users\\Hugo\\Desktop\\Desarrollo de Interfaces\\T3\\infoEj1.txt", equipos);
                        break;
                }
            }
        }
    }
}
