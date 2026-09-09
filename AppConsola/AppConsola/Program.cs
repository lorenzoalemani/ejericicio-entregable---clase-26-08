using AccesoDatos.Models;
using AccesoDatos.Repositories;

// 1. Instanciamos el repositorio.

IGenericRepository<Socio> socioRepository = new GenericRepository<Socio>();
IGenericRepository<Pelicula> peliculaRepository = new GenericRepository<Pelicula>();
IGenericRepository<Alquiler> alquilerRepository = new GenericRepository<Alquiler>();
bool continuar = true;

while (continuar)
{
    Console.WriteLine("=================================================");
    Console.WriteLine("Gestión de Biblioteca");
    Console.WriteLine("=================================================");
    Console.WriteLine();
    Console.WriteLine("1. Agregar Socio (Alta)");
    Console.WriteLine("2. Agregar Pelicula (Alta)");
    Console.WriteLine("3. registrar alquiler");
    Console.WriteLine("4. Ver alquileres por cada socio");
    Console.WriteLine("5. Ver alquileres con demora de devolucion por cada socio");
    Console.WriteLine("6. Ver reportes de pelis mas alquiladas");
    Console.WriteLine("7. reporte con el socio que más películas alquilo.");

    Console.WriteLine("8. Salir");


    Console.Write("Seleccione una opción: ");
    string opcion = Console.ReadLine();
    Console.Clear();

    switch (opcion)
    {
        case "1":
            AltaSocio();
            break;

        case "2":
            AltaPelicula();
            break;

        case "3":
            RegistrarAlquiler();
            break;

        case "4":
            veralquileresporsocio();

            break;
        case "5":
            veralquilerescondemora();
            break;
        case "6":
            verpeliculasmasalquiladas();
            break;
        case "7":
            versocioquemaspelisalquilo();
            break;
        case "8":
            Console.WriteLine("¡Cerrando el sistema de biblioteca!");
            continuar = false;

            break;

        default:
            Console.WriteLine("Opción no válida. Intente nuevamente.");
         
            break;
    }
}

void AltaSocio()
{
    Console.Write("Ingrese el nombre del socio: ");
    string name = Console.ReadLine();
    Console.WriteLine("ingrese el apellido");
    string apellido = Console.ReadLine();
    Console.WriteLine("ingrese el dni");
    int dni = int.Parse(Console.ReadLine());

    Console.WriteLine("ingrese el telefono");
    int telefono = int.Parse(Console.ReadLine());
    var nuevoSocio = new Socio
    {
      
       Nombre = name,
       Apellido = apellido,
       Dni = dni,
       Telefono = telefono

       
    };

    socioRepository.Agregar(nuevoSocio);
    Console.WriteLine("Usuario agregado exitosamente.");

}
void AltaPelicula()
{
    Console.WriteLine("ingrese el titulo de la pelicula");
    string titulo = Console.ReadLine();

    

    Console.WriteLine("ingrese el autor");
    string autor = Console.ReadLine();

   Console.WriteLine("ingrese la cantidad q hay disponibles");
   int cantidad = int.Parse(Console.ReadLine());
    

        var nuevoPelicula = new Pelicula
    {
       
        Titulo = titulo,
        Autor = autor,
        Cantidad = cantidad,
    };

    peliculaRepository.Agregar(nuevoPelicula);
    Console.WriteLine("Pelicula agregado exitosamente.");
   
  

}



void RegistrarAlquiler()
{
    var listasocios = socioRepository.ObtenerTodos();
    foreach(var a in listasocios)
    {
        Console.WriteLine($"ID :{a.Id} ||| NOMBRE:{a.Nombre} ||| APELLIDO: {a.Apellido}");
    }
    Console.WriteLine("ingrese el id del socio que quieres registrar el alquiler");
    int id = int.Parse(Console.ReadLine());

        var socio = socioRepository.ObtenerPorId(id);
        if(socio != null)
        {
            Console.WriteLine("socio encontrado con exito");
        }
        else
        {
            Console.WriteLine("No se encontró ningún usuario con ese ID.");
        }
   
    var listapelis = peliculaRepository.ObtenerTodos();
    Console.WriteLine("ingrese cuantas pelis quiere alquilar");
    int cant = int.Parse (Console.ReadLine());

    List<Pelicula> peliculasseleccionadas = new List<Pelicula>();
    for(int i = 0; i < cant; i++)
    {
        foreach (var a in listapelis)
        {
            Console.WriteLine($"ID :{a.Id} ||| NOMBRE:{a.Titulo} |||   {a.Autor}");
        }
        Console.WriteLine("ingrese el id de la peli que quiere alquilar");
        int idp = int.Parse(Console.ReadLine());
        var peli = peliculaRepository.ObtenerPorId(idp);
        if (peli.Cantidad > 0)
        {
            peliculasseleccionadas.Add(peli);
            peli.Cantidad--;
            peliculaRepository.Modificar(peli);
        }
        else
        {
            Console.WriteLine("no se puede agregar la peli porque no hay cantidad suficiente");
            i--;
        }
        

        
    }
    Console.WriteLine("ingrese el monto");
    decimal monto = decimal.Parse(Console.ReadLine());
    if(peliculasseleccionadas.Count > 0)
    {
        var nuevoalquiler = new Alquiler
        {
            Socioid = id,
            Fechaentrega = DateTime.Now,
            Fechadevolucion = DateTime.Now.AddDays(7),
            listapeliculas = peliculasseleccionadas,
            Monto = monto,
            
        };
        alquilerRepository.Agregar(nuevoalquiler);
        socio.Alquileres.Add(nuevoalquiler);
        socioRepository.Modificar(socio);
    }
    else
    {
        Console.WriteLine("no se puede registrar el alquiler porque no hay peliculas seleccionadas");
    }
    
}

void veralquileresporsocio()
{
    var socios = socioRepository.ObtenerTodosCon("Alquileres.listapeliculas");
    foreach (var s in socios)
    {
        Console.WriteLine($"Nombre: {s.Nombre} | Apellido: {s.Apellido} ");
        foreach (var sa in s.Alquileres)
        {
            foreach (var pelis in sa.listapeliculas)
            {
                Console.WriteLine($"Titulo: {pelis.Titulo}");
            }
        }
    }
}

void veralquilerescondemora()
{
    var socios = socioRepository.ObtenerTodosCon("Alquileres");
    foreach( var s in socios)
    {
        foreach(var sa in s.Alquileres)
        {
            
            if(sa.Fechadevolucion < DateTime.Now)
            {
                decimal montofinal = sa.calcularrecarga();
                sa.Monto = montofinal;
                Console.WriteLine($"Nombre: {s.Nombre}, tiene el alquier vencido y el monto al final es {montofinal}");
                alquilerRepository.Modificar(sa);

            }
        }
    }

    
}

void verpeliculasmasalquiladas()
{
    var Alquileres = alquilerRepository.ObtenerTodosCon("listapeliculas");
    var Peliculas = peliculaRepository.ObtenerTodos();
    int max = 0;
    int idmasbuscado = -1;
   foreach(var p in Peliculas)
    {
        int contador = 0;
        foreach(var a in Alquileres)
        {
            foreach(var al in a.listapeliculas)
            {
                if(al.Id == p.Id)
                {
                    contador++;
                }
            }
        }

        if(contador > max)
        {
            max = contador;
            idmasbuscado = p.Id;
        }
    }
   if(idmasbuscado >= 0)
    {
        var pelimb = peliculaRepository.ObtenerPorId(idmasbuscado);
        Console.WriteLine($"la pelicula mas alquilada es {pelimb.Titulo} ");
    }
    
}

void versocioquemaspelisalquilo()
{
    var socio = socioRepository.ObtenerTodosCon("Alquileres.listapeliculas");
    int max = 0;
    int idsocio = -1;
    foreach (var s in socio)
    {
        int totalsocio = 0;
        foreach (var sa in s.Alquileres)
        {
            totalsocio += sa.listapeliculas.Count;

        }
        if (totalsocio > max)
        {
            max = totalsocio;
            idsocio = s.Id;
        }
    }

    var sociomas = socioRepository.ObtenerPorId(idsocio);
    Console.WriteLine($"el socio con mas pelis alquiladas es {sociomas.Nombre}");
}