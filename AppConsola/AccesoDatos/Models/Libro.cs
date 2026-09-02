using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Models
{
    public  class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Anio { get; set; }
        public Autor Autor { get; set; }
        public int AutorId { get; set; }
        public bool Estado { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
    }
}
