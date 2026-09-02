using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Models
{
    public class Categoria
    {
        public string Nombre {  get; set; }
        public int Id { get; set; }
        public List<Libro> listalibros { get; set; } = new List<Libro>();
        

    }
}
