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
                    // Así solo capturo las excepciones que me interesan
                    catch (OverflowException)
                    {
                        verificadaIP = false;
                        Console.WriteLine("Formato de IP incorrecto, has escrito un número demasiado grande");
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    catch (FormatException)
                    {
                        verificadaIP = false;
                        Console.WriteLine("Formato de IP incorrecto, has escrito un caracter no permitido para una IP");
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

            while (!verificado)
            {
                try
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
                catch (OverflowException)
                {
                    verificado = false;
                    Console.WriteLine("Has escrito un número demasiado grande, anda con más cuidado!");
                }
                catch (FormatException)
                {
                    verificado = false;
                    Console.WriteLine("El formato del número de memoria RAM es incorrecto, vuelve a escribirlo!");
                }
            }

            return ram;
        }


        // Creo la función para introducir un nuevo elemento en la Hashtable, que llamará a los otros dos métodos que validan la pedida de datos :
        static void introducirDato(Hashtable equipos)
        {
            string ip = escribirIP();
            int ram = escribirRAM();

            if (equipos.ContainsKey(ip))
            {
                Console.WriteLine("Esa IP ya ha sido introducida en la Hashtable!");
            }
            else
            {
                equipos.Add(ip, ram);
                Console.WriteLine("Los datos del nuevo equipo se han introducido con éxito");
                Console.WriteLine("Escribe Enter para continuar : ");
                Console.WriteLine();
                Console.WriteLine();
                Console.ReadLine();
            }
        }


        // Función para eliminar un elemento de la Hashtable :
        static void eliminarDato(Hashtable equipos)
        {

            Console.WriteLine("Escribe la IP del equipo que quiere eliminar : ");
            string ip = Console.ReadLine();

            if (equipos.ContainsKey(ip))
            {
                Console.WriteLine("La IP que has intoducido está dentro de la Hashtable");
                Console.WriteLine("Procediendo a eliminar el equipo...");
                equipos.Remove(ip);
            }
            else
            {
                Console.WriteLine("No se ha encontrado el equipo que querías eliminar");
            }

            Console.WriteLine("Pulsa Enter para proceder :");
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

            if (equipos.ContainsKey(ip))
            {
                Console.WriteLine("La IP que has intoducido está dentro de la Hashtable");
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


        // Función que guarda los equipos de la hashtable en un archivo indicado
        static void guardarEnArchivo(String ruta, Hashtable equipos)
        {
            try
            {
                File.Create(ruta);

                using (StreamWriter sw = new StreamWriter(ruta))
                {
                    foreach (DictionaryEntry de in equipos)
                    {
                        sw.WriteLine("{0}|{1}", de.Key, de.Value);
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Error : No se ha encontrado la ruta indicada del archivo");
            }
            catch (IOException)
            {
                Console.WriteLine("Error al guardar los datos en el archivo");
            }
        }


        // Función que carga los equipos de un archivo indicado para meterlos en la hashtable
        static void cargarDeArchivo(String ruta, Hashtable equipos)
        {
            try
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

                        if (!equipos.ContainsKey(ip))
                        {
                            equipos.Add(ip, ram);
                        }

                        datos = sr.ReadLine();
                    }
                    sr.Close();
                }
                else
                {
                    File.Create(ruta);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Error : No se ha encontrado la ruta indicada del archivo");
            }
            catch (IOException)
            {
                Console.WriteLine("Error al cargar los datos del archivo al programa");
            }
        }



        static void Main(string[] args)
        {
            Hashtable equipos = new Hashtable();

            cargarDeArchivo("..\\..\\..\\infoEj1.txt", equipos);
            int opcion = 0;
            while (opcion !=5)
            {
                
                Console.WriteLine("1- Introducir dato");
                Console.WriteLine("2- Eliminar dato por clave");
                Console.WriteLine("3- Mostrar la colección entera");
                Console.WriteLine("4- Mostrar un elemento de la colección");
                Console.WriteLine("5- Salir");
                Console.WriteLine("Qué quieres hacer?");

                try
                {
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

                            guardarEnArchivo("..\\..\\..\\infoEj1.txt", equipos);
                            break;

                        default:
                            Console.WriteLine("No existe esa opción!");
                            break;
                    }
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Has escrito un número mucho más grande al que deberías escribir, cuidado con lo que haces!");
                    Console.WriteLine();
                }
                catch (FormatException)
                {
                    Console.WriteLine("No has escrito un número! Asegúrate de escribir un valor numérico");
                    Console.WriteLine();
                }
            }
        }
    }
}
