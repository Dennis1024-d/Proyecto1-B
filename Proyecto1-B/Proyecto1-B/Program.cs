//Variable
//datos del operador
//control de sistema
//datos del ticket
//calculo
//menu 
using System.Runtime.InteropServices;

int menu;


//1. REGISTRO INICIAL DEL SISTEMA
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
            Console.WriteLine("PROCEDIMINETO A");
            break;
        case 2:
            Console.WriteLine("PROCEDIMINETO B");
            break;
        case 3:
            Console.WriteLine("PROCEDIMINETO C");
            break;
        case 4:
            Console.WriteLine("PROCEDIMINETO D");
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

} while (menu != 5);





//3. CREAR TICKER DE ENTRADA
//4 CALCULOS 
//5. VER ESTADO DEL PARQEO
//6. SIMULAR TIEMPO
//7.