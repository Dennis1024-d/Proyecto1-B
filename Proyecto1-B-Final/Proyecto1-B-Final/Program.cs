using System;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.InteropServices;
internal class Program
{
    private static void Main(string[] args)
    {
        //Variable
        //datos del operador
        string nom_operador = "";
        int capacidad_total = 0;
        string turno = "";
        bool datos_validos = false;
        //control de sistema
        int tickets_creados = 0;
        int tickets_cerrados = 0;
        double dinero_recaudado = 0;
        int tiempo_simulado = 0;
        bool ticket_activo = false;
        int espacios_disponibles;
        string placa;
        int tipo_vehiculo = 0;
        string nom_cliente;
        int minuto_entrada = 0;
        //datos del ticket
        bool cliente_vip = false;
        //calculo
        int minutos_simular;
        double horas_cobradas;
        double tarifa = 0;
        double subtotal;
        double multa;
        double descuento_vip;
        double recargo;
        double monto_final = 0;
        //menu 
        int menu;


        //1. REGISTRO INICIAL DEL SISTEMA
        Console.WriteLine("¡Bienvenido a su turno en SmartPark!");


        while (!datos_validos)
        {
            Console.Clear(); //Limpia la pantalla, es decir, deja en blanco lo que el usuario visualiza

            Console.WriteLine("Ingrese nombre del operador: ");
            nom_operador = Console.ReadLine();

            Console.WriteLine("Ingrese su código de turno (4 caracteres): ");
            turno = Console.ReadLine();

            Console.WriteLine("Ingrese la capacidad del parqueo: ");
            capacidad_total = int.Parse(Console.ReadLine());

            //Validación de los datos ingresados
            if (turno.Length != 4 && capacidad_total < 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El código debe tener exactamente cuatro caracteres y la capacidad debe ser mínimo 10 parqueos.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Presione Enter para intentar de nuevo...");
                Console.ReadLine(); // Espera que el usuario vea el mensaje
            }
            else if (turno.Length != 4) //Lenght = cantidad de caracteres permitidos
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El código debe tener exactamente cuatro caracteres.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Presione Enter para intentar de nuevo...");
                Console.ReadLine(); // Espera que el usuario vea el mensaje
            }
            else if (capacidad_total < 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("La capacidad debe ser mínimo 10 parqueos.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Presione Enter para intentar de nuevo...");
                Console.ReadLine(); // Espera que el usuario vea el mensaje
            }
            else
            {
                datos_validos = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Registro completado correctamente.");
            }
        }
        Console.Clear();
        //2. MENU CICLO
        do
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. CREAR TICKET");
            Console.WriteLine("2. REGISTAR SALIDA");
            Console.WriteLine("3. VER ESTADO");
            Console.WriteLine("4. SIMULAR TIEMPO");
            Console.WriteLine("5 SALIR");
            Console.ForegroundColor = ConsoleColor.White;
            menu = int.Parse(Console.ReadLine());

            //Validar si el valor ingresado es válido
            while (menu < 1 || menu > 5)  // Indica que si el número ingresado es un negativo o mayor a 5, el programa pedirá que ingrese de nuevo un número que sea válido.
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("La opción ingresada no es válida. Ingrese un número del 1 al 5: ");
                Console.ForegroundColor = ConsoleColor.White;
                menu = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.Clear(); //Limpia la pantalla, es decir, deja en blanco lo que el usuario visualiza

            switch (menu)
            {
                case 1: //Parte A - crear ticket
                    if (ticket_activo == true) //Indica que no se puede proceder si ya hay un ticket activo
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Ya existe ticket activo");

                    }
                    else
                    {
                        espacios_disponibles = capacidad_total - tickets_creados; //actualiza la cantidad de parqueos disponibles según la cantidad de tickets creados

                        if (espacios_disponibles == 0) //No permite avanzar debido a que ya no hay parqueos disponibles
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Parqueo lleno.");

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Ingrese placa (6 a 8 caracteres):");
                            placa = Console.ReadLine();


                            while (placa.Length < 6 || placa.Length > 8) //Permite pasar SOLO si la placa tiene 6 u 8 caracteres, sino regresa hasta que ingrese los datos válidos.
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error. Ingrese una placa válida:");
                                Console.ForegroundColor = ConsoleColor.White;
                                placa = Console.ReadLine();

                            }
                            Console.ForegroundColor = ConsoleColor.White; //Se presentan las opciones de vehículos y se guardan en una variable para usar esa información después
                            Console.WriteLine("Ingrese su tipo de vehiculo");
                            Console.WriteLine("1. Moto");
                            Console.WriteLine("2. Vehículo");
                            Console.WriteLine("3. Pickup");
                            Console.ForegroundColor = ConsoleColor.White;
                            tipo_vehiculo = int.Parse(Console.ReadLine());

                            //Validar si el valor ingresado es válido
                            while (tipo_vehiculo < 1 || tipo_vehiculo > 3) //Indica que si el número ingresado es un negativo o mayor a 3, el programa pedirá que ingrese de nuevo un número que sea válido.
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("La opción ingresada no es válida. Ingrese 1, 2 o 3: ");
                                Console.ForegroundColor = ConsoleColor.White;
                                tipo_vehiculo = int.Parse(Console.ReadLine());
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                            Console.WriteLine("Ingrese el nombre del cliente");
                            nom_cliente = Console.ReadLine();

                            minuto_entrada = tiempo_simulado;
                            ticket_activo = true;
                            tickets_creados++;

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Ticket creado correctamente.");
                        }
                    }
                    break; //Termina la primera parte 

                case 2: // Registrar salida 
                    if (ticket_activo == false) //Verifica si NO hay un ticket activo
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No existe ticket activo.");
                    }
                    else
                    {
                        //Calcular el tiempo que el vehículo estuvo dentro del parqueo
                        int tiempo_estacionado = tiempo_simulado - minuto_entrada;

                        if (tiempo_estacionado <= 15) // Si tiempo es menor a 15 no se cobrara
                        {
                            monto_final = 0;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Tiempo menor o igual a 15 minutos. No hay cobro.");

                            dinero_recaudado += monto_final;
                            tickets_cerrados++; //Aumenta la cantidad de tickets cerrados
                            ticket_activo = false; //Aumenta la cantidad de tickets cerrados
                        }
                        else
                        {
                            //Se asignara la tarifa dependiendo del tipo de vehículo
                            switch (tipo_vehiculo)
                            {
                                case 1:
                                    tarifa = 5;
                                    break;
                                case 2:
                                    tarifa = 10;
                                    break;
                                case 3:
                                    tarifa = 15;
                                    break;
                            }

                            horas_cobradas = (double)tiempo_estacionado / 60; //Se convierte de minutos a horas
                            subtotal = horas_cobradas * tarifa; //Caculo de subtotal se multiplica tarifa por horas 
                            if (tiempo_estacionado > 360)
                            {
                                multa = 25;
                            }
                            else
                            {
                                multa = 0;
                            }
                            // Preguntar si es Vip o no el cliente 
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Es ViP? True/False");
                            cliente_vip = bool.Parse(Console.ReadLine());
                            if (cliente_vip == true) //Si es VIP se aplica descuento del 10%
                            {
                                descuento_vip = subtotal * 0.10;
                            }
                            else
                            {
                                descuento_vip = 0;
                            }
                            //Si el tiempo supera 12 horas se aplica recargo del 20%
                            if (tiempo_estacionado > 720)
                            {
                                recargo = (subtotal + multa - descuento_vip) * 0.20;
                            }
                            else // Si el cliente no supero las 12 horas no se aplica recargo
                            {
                                recargo = 0;
                            }
                            //Se calcula el monto final sumando todo y restando descuento
                            monto_final = subtotal + multa + recargo - descuento_vip;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("El cobro final sera de Q : " + monto_final);
                            dinero_recaudado = dinero_recaudado + monto_final;
                            tickets_cerrados = tickets_cerrados + 1;
                            ticket_activo = false;
                        }
                    }
                    break;
                case 3: // Ver estado del parqueo
                    int espacios_ocupados = ticket_activo ? 1 : 0; //Se calcula el monto final sumando todo y restando descuento
                    espacios_disponibles = capacidad_total - espacios_ocupados;// se calculan espacios disponibles

                    Console.WriteLine("-------- ESTADO DEL PARQUEO --------");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Capacidad total: " + capacidad_total);
                    Console.WriteLine("Espacios ocupados: " + espacios_ocupados);
                    Console.WriteLine("Espacios disponibles: " + espacios_disponibles);
                    Console.WriteLine("Tiempo simulado: " + tiempo_simulado + " minutos");
                    Console.WriteLine("Total recaudado: Q " + dinero_recaudado);
                    Console.WriteLine("Tickets creados: " + tickets_creados);
                    Console.WriteLine("Tickets cerrados: " + tickets_cerrados);
                    break;
                case 4: // Simular pasa del tiempo
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Ingrese la cantidad de minutos a simular (1-1440)");
                    minutos_simular = int.Parse(Console.ReadLine());

                    //Verifica que el numero este en el rango 
                    while (minutos_simular < 1 || minutos_simular > 1400)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("El valor que ingresó no es válido. Debe ingresar minutos entre 1 y 1440:");
                        Console.ForegroundColor = ConsoleColor.White;
                        minutos_simular = int.Parse(Console.ReadLine());
                    }
                    //Suma los minutos ingresados al tiempo total del sistema
                    tiempo_simulado = tiempo_simulado + minutos_simular;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Tiempo simulado actual: " + tiempo_simulado + " minutos");

                    if (ticket_activo == true)
                    {
                        int tiempo_estacionado = tiempo_simulado - minuto_entrada;

                        //Advertencia si supera 12 horas
                        if (tiempo_estacionado > 720)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("El vehículo ha superado 12 horas. Se aplicará recargo por permanencia extrema.");
                        }
                        //Advertencia si supera 6 horas
                        else if (tiempo_estacionado > 360)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("El vehículo ha superado 6 horas. Se aplicará una multa pronto.");
                        }
                    }

                    break;
                case 5: //Salir del sistema
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("===== RESUMEN FINAL DEL TURNO =====");
                    Console.WriteLine("Operador: " + nom_operador);
                    Console.WriteLine("Código de turno: " + turno);
                    Console.WriteLine("Tickets creados: " + tickets_creados);
                    Console.WriteLine("Tickets cerrados: " + tickets_cerrados);
                    Console.WriteLine("Dinero recaudado: Q " + dinero_recaudado);
                    Console.WriteLine("Tiempo simulado: " + tiempo_simulado + " minutos");
                    Console.WriteLine("Gracias por usar SmartPark");
                    Console.ReadLine();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opción inválida");
                    break;
            }

            if (menu != 5)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Presione ENTER para volver al menú...");
                Console.ReadLine();
            }
            Console.Clear();

        } while (menu != 5);
    }
}