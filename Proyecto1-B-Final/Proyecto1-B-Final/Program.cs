using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.InteropServices;
//Variable
//datos del operador
string nom_operador;
int capacidad_total = 0;
string turno;
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
    if (turno.Length != 4) //Lenght = cantidad de caracteres permitidos
    {
        Console.WriteLine("El código debe tener exactamente cuatro caracteres.");
        Console.WriteLine("Presione Enter para intentar de nuevo...");
        Console.ReadLine(); // Esperar que el usuario vea el mensaje
    }
    else if (capacidad_total < 10)
    {
        Console.WriteLine("La capacidad debe ser mínimo 10 parqueos.");
        Console.WriteLine("Presione Enter para intentar de nuevo...");
        Console.ReadLine(); // Esperar que el usuario vea el mensaje
    }
    else
    {
        datos_validos = true;
        Console.WriteLine("Registro completado correctamente.");
    }
}
Console.Clear();
//2. MENU CICLO
do
{
    Console.WriteLine("1. CREAR TICKET");
    Console.WriteLine("2. REGISTAR SALIDA");
    Console.WriteLine("3. VER ESTADO");
    Console.WriteLine("4. SIMULAR TIEMPO");
    Console.WriteLine("5 SALIR");
    menu = int.Parse(Console.ReadLine());

    Console.Clear();

    switch (menu)
    {
        case 1:
            if (ticket_activo == true)
            {
                Console.WriteLine("Ya existe ticket activo");
            }
            else
            {
                espacios_disponibles = capacidad_total - tickets_creados;

                if (espacios_disponibles == 0)
                {
                    Console.WriteLine("Parqueo lleno.");
                }
                else
                {
                    Console.WriteLine("Ingrese placa (6 a 8 caracteres):");
                    placa = Console.ReadLine();

                    while (placa.Length < 6 || placa.Length > 8)
                    {
                        Console.WriteLine("Error. Ingrese una placa válida:");
                        placa = Console.ReadLine();
                    }
                    Console.WriteLine("Ingrese su tipo de vehiculo");
                    Console.WriteLine("1. Moto");
                    Console.WriteLine("2. Vehículo");
                    Console.WriteLine("3. Pickup");
                    tipo_vehiculo = int.Parse(Console.ReadLine());

                    Console.WriteLine("Ingrese su nombre");
                    nom_cliente = Console.ReadLine();

                    minuto_entrada = tiempo_simulado;
                    ticket_activo = true;
                    tickets_creados++;

                    Console.WriteLine("Ticket creado correctamente.");
                }
            }
            break;

        case 2:
            if (ticket_activo == false)
            {
                Console.WriteLine("No existe ticket activo.");
            }
            else
            {
                int tiempo_estacionado = tiempo_simulado - minuto_entrada;

                if (tiempo_estacionado <= 15)
                {
                    monto_final = 0;
                    Console.WriteLine("Tiempo menor o igual a 15 minutos. No hay cobro.");
                }
                else
                {
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

                    horas_cobradas = (double)tiempo_estacionado / 60;
                    subtotal = horas_cobradas * tarifa;
                    if (tiempo_estacionado > 360)
                    {
                        multa = 25;
                    }
                    else
                    {
                        multa = 0;
                    }
                    Console.WriteLine("Es ViP? True/False");
                    cliente_vip = bool.Parse(Console.ReadLine());
                    if (cliente_vip == true)
                    {
                        descuento_vip = subtotal * 0.10;
                    }
                    else
                    {
                        descuento_vip = 0;
                    }
                    if (tiempo_estacionado > 720)
                    {
                        recargo = (subtotal + multa - descuento_vip) * 0.20;
                    }
                    else
                    {
                        recargo = 0;
                    }
                    monto_final = subtotal + multa + recargo - descuento_vip;
                    Console.WriteLine("El cobro final sera de : " + monto_final);
                    dinero_recaudado = dinero_recaudado + monto_final;
                    tickets_cerrados = tickets_cerrados + 1;
                    ticket_activo = false;
                }
            }
            break;
        case 3:
            int espacios_ocupados = tickets_creados - tickets_cerrados;
            espacios_disponibles = capacidad_total - espacios_ocupados;

            Console.WriteLine("-------- ESTADO DEL PARQUEO --------");
            Console.WriteLine("Capacidad total: " + capacidad_total);
            Console.WriteLine("Espacios ocupados: " + espacios_ocupados);
            Console.WriteLine("Espacios disponibles: " + espacios_disponibles);
            Console.WriteLine("Tiempo simulado: " + tiempo_simulado + " minutos");
            Console.WriteLine("Total recaudado: Q: " + dinero_recaudado);
            Console.WriteLine("Tickets creados: " + tickets_creados);
            Console.WriteLine("Tickets cerrados: " + tickets_cerrados);
            break;
        case 4:
            Console.WriteLine("Ingrese la cantidad de minutos a simular (1-1440");
            minutos_simular = int.Parse(Console.ReadLine());

            while (minutos_simular < 1 || minutos_simular > 1400)
            {
                Console.WriteLine("El valor que ingresó no es válido. Debe ingresar minutos entre 1 y 1440:");
                minutos_simular = int.Parse(Console.ReadLine());
            }
            tiempo_simulado = tiempo_simulado + minutos_simular;
            Console.WriteLine("Tiempo simulado actual: " + tiempo_simulado + "minutos");

            if (ticket_activo == true)
            {
                int tiempo_estacionado = tiempo_simulado - minuto_entrada;

                if (tiempo_estacionado > 720)
                {
                    Console.WriteLine("El vehículo ha superado 12 horas. Se aplicará recargo por permanencia extrema.");
                }
                else if (tiempo_estacionado > 360)
                {
                    Console.WriteLine("El vehículo ha superado 6 horas. Se aplicará una multa pronto.");
                }
            }

            break;
        case 5:
            Console.WriteLine("PROCEDIMINETO F");
            break;
        default:
            Console.WriteLine("Opción inválida");
            break;
    }

    if (menu != 5)
    {
        Console.WriteLine("Presione ENTER para volver al menú...");
        Console.ReadLine();
    }
    Console.Clear();

} while (menu != 5);