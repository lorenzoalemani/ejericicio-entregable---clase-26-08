using AccesoDatos.Models;
using AccesoDatos.Repositories;

// 1. Instanciamos el repositorio.

IGenericRepository<Autor> autorRepository = new GenericRepository<Autor>();
IGenericRepository<Libro> libroRepository = new GenericRepository<Libro>();
IGenericRepository<Categoria> categoriaRepository = new GenericRepository<Categoria>();
bool continuar = true;

while (continuar)
{
    Console.WriteLine("=================================================");
    Console.WriteLine("Gestión de Biblioteca");
    Console.WriteLine("=================================================");
    Console.WriteLine();
    Console.WriteLine("1. Agregar Autor (Alta)");
    Console.WriteLine("2. Agregar Libro (Alta)");
    Console.WriteLine("3. Ver todos los libros");
    Console.WriteLine("4. Agregar Categoria (Alta)");
    Console.WriteLine("5. Ver categorias");
    Console.WriteLine("6. Modificar libro");
    Console.WriteLine("7. Modificar autor");
    Console.WriteLine("8. Eliminar libro");
    Console.WriteLine("9. Salir");


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
            CrearCategoria();

            break;
        case "5":
            VerCategorias();
            break;
        case "6":
            Modificarnombrelibro();
            break;
        case "7":
            ModificarNombreAutor();
            break;
        case "8":
            EliminarLibro();
            break;
        case "9":
            Console.WriteLine("¡Cerrando el sistema de biblioteca!");
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
    var nuevoAutor = new Autor
    {
       Nombre = name,
       
    };

    autorRepository.Agregar(nuevoAutor);
    Console.WriteLine("Usuario agregado exitosamente.");
    PresioneParaContinuar();
}
void AltaLibro()
{
    Console.WriteLine("ingrese el titulo del libro");
    string titulo = Console.ReadLine();

    

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

    var Categorias = categoriaRepository.ObtenerTodos();
    foreach( var categoria in Categorias)
    {
        Console.WriteLine($" ID: {categoria.Id} |  Nombre {categoria.Nombre}");
    }

    Console.WriteLine("ingrese el id de la categoria q quiere asignarle al libro");
    int id = int.Parse(Console.ReadLine());
    var Categoria = categoriaRepository.ObtenerPorId(id);
    

        var nuevoLibro = new Libro
    {
       
        Titulo = titulo,
        Anio = fecha,
        AutorId = idl,
        Estado = true,
        CategoriaId = id,
    };

    libroRepository.Agregar(nuevoLibro);
    Console.WriteLine("Libro agregado exitosamente.");
    autor.Listalibros.Add(nuevoLibro);
    Categoria.listalibros.Add(nuevoLibro);
    PresioneParaContinuar();

}



void ModificarNombreAutor()
{
    var listaautores = autorRepository.ObtenerTodos();
    foreach(var a in listaautores)
    {
        Console.WriteLine($"ID :{a.Id} ||| NOMBRE:{a.Nombre}");
    }
    Console.WriteLine("ingrese el id del autor a modificar");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var autorcambiar = autorRepository.ObtenerPorId(id);
        if(autorcambiar != null)
        {
            Console.WriteLine("ingrese el nombre nuevo para el autor");
            autorcambiar.Nombre = Console.ReadLine();
            autorRepository.Modificar(autorcambiar);
            Console.WriteLine("nombre de autor cambiado correctamente");
        }
        else
        {
            Console.WriteLine("No se encontró ningún usuario con ese ID.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }

}

void Modificarnombrelibro()
{
    Mostrartodosloslibros();
    Console.WriteLine("ingrese el id del libro");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var librocambiar = libroRepository.ObtenerPorId(id);
        if(librocambiar != null)
        {
            Console.WriteLine("ingrese el nuevo nombre para el libro");
            librocambiar.Titulo = Console.ReadLine();
            libroRepository.Modificar(librocambiar);
            Console.WriteLine("titulo del libro modificado correctamente");
        }
        else
        {
            Console.WriteLine("no se encontro libro con ese id");
        }
    }
    else
    {
        Console.WriteLine("id invalido");
    }
}
void CrearCategoria()
{
    Console.WriteLine("ingrese el nombre de la categoria");
    string nombre = Console.ReadLine();
    var nuevadategoria = new Categoria
    {
        Nombre = nombre,
    };
    categoriaRepository.Agregar(nuevadategoria);
}
void VerCategorias()
{
    var vercategorias = categoriaRepository.ObtenerTodos();
    foreach( var categoria in vercategorias)
    {
        Console.WriteLine(categoria.Nombre);
    }
}

void EliminarLibro()
{
    var verlista = libroRepository.ObtenerTodosCon("Autor");
    foreach(var libro in verlista)
    {
        Console.WriteLine($"Nombre : {libro.Id} Titulo: {libro.Titulo}   Autor: {libro.Autor.Nombre}   AutorID: {libro.AutorId}");
    }
    Console.WriteLine("ingrese el id del libro q desea eliminar");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var libro = libroRepository.ObtenerPorId(id);
        if (libro != null)
        {
            libro.Estado = false;
            Console.WriteLine("libro eliminado correctamente");
        }
    }

}


void Mostrartodosloslibros()
{
    Console.WriteLine("LISTA DE LOS LIBROS ACTUALES");
    var listalibros = libroRepository.ObtenerTodosCon("Autor");
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
