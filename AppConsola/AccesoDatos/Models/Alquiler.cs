using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Models
{
    public  class Alquiler
    {
        public int Id { get; set; }
        public int Socioid { get; set; }
        public DateTime Fechaentrega { get; set; }
        public DateTime Fechadevolucion { get; set; }
        public decimal Monto { get; set; }
        public List<Pelicula> listapeliculas { get; set; }
        public Socio Socio { get; set; }
      
        public decimal calcularrecarga()
        {
            int dias = (DateTime.Now - Fechadevolucion).Days;
            decimal recargo = Monto * (0.10m * dias);
            decimal montofinal = Monto + recargo;
            return montofinal;
        }
    }
}
