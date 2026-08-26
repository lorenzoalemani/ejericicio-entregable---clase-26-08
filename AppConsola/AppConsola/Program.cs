using AccesoDatos.Models;
using AccesoDatos.Repositories;

// 1. Instanciamos el repositorio.

IGenericRepository<Autor> autorRepository = new GenericRepository<Autor>();
IGenericRepository<Libro> libroRepository = new GenericRepository<Libro>();
bool continuar = true;

while (continuar)
{
    Console.WriteLine("=================================================");
    Console.WriteLine("\tGestión de Biblioteca");
    Console.WriteLine("=================================================");
    Console.WriteLine();
    Console.WriteLine("1. Agregar Autor (Alta)");
    Console.WriteLine("2. Agregar Libro (Alta)");
    Console.WriteLine("3. Ver todos los libros");
    Console.WriteLine("4. Salir");


    Console.Write("Seleccione una opción: ");
    string opcion = Console.ReadLine();
    Console.Clear();

    switch (opcion)
    {
        case "1":
            AltaAutor();
            break;

        case "2":
            AltaLibro();
            break;

        case "3":
            Mostrartodosloslibros();
            break;

        case "4":
            Console.WriteLine("¡Cerrando el sistema de usuarios!");
            continuar = false;
            break;

        
            

        default:
            Console.WriteLine("Opción no válida. Intente nuevamente.");
            PresioneParaContinuar();
            break;
    }
}

void AltaAutor()
{
    Console.Write("Ingrese el nombre del autor: ");
    string name = Console.ReadLine();

    Console.WriteLine("ingrese el id del autor");
    int id = int.Parse(Console.ReadLine());

    var nuevoAutor = new Autor
    {
       Nombre = name,
       Id = id
    };

    autorRepository.Agregar(nuevoAutor);
    Console.WriteLine("Usuario agregado exitosamente.");
    PresioneParaContinuar();
}
void AltaLibro()
{
    Console.WriteLine("ingrese el titulo del libro");
    string titulo = Console.ReadLine();

    Console.WriteLine("ingrese el id del libro");
    int id = int.Parse(Console.ReadLine());

    Console.WriteLine("ingrese el anio de publicacion");
    int fecha = int.Parse(Console.ReadLine());

    var autores = autorRepository.ObtenerTodos();
    foreach(var a in autores)
    {
        Console.WriteLine($"id: {a.Id} | Nombre: {a.Nombre}");
    }
    Console.WriteLine("ingrese el id del autor al que quiere asignarle el libro");
    int idl = int.Parse(Console.ReadLine());
    var autor = autorRepository.ObtenerPorId(idl);

    var nuevoLibro = new Libro
    {
        Id = id,
        Titulo = titulo,
        Anio = fecha,
        AutorId = idl,
    };

    libroRepository.Agregar(nuevoLibro);
    Console.WriteLine("Libro agregado exitosamente.");
    autor.Listalibros.Add(nuevoLibro);
    PresioneParaContinuar();

}






void Mostrartodosloslibros()
{
    Console.WriteLine("LISTA DE LOS LIBROS ACTUALES");
    var listalibros = libroRepository.ObtenerTodos();
    foreach( var l in listalibros)
    {
        Console.WriteLine($"ID : {l.Id} | TITULO: {l.Titulo} | ANIO: {l.Anio} | AUTOR {l.Autor.Nombre} | AUTORid {l.AutorId}");
    }
}

void PresioneParaContinuar()
{
    Console.WriteLine("\nPresione cualquier tecla para continuar...");
    Console.ReadKey();
    Console.Clear();
}
