using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        [DisplayName("Codigo")]
        public string codigo { get; set; }
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        [DisplayName("Descripción")]
        public string descripcion { get; set; }
        [DisplayName("Marca")]
        public Marca IdMarca { get; set; }
        [DisplayName("Categoria")]
        public Categoria IdCategoria { get; set; }
        public string ImagenUrl { get; set; }
        [DisplayName("Precio")]
        public decimal precio { get; set; }
    }
}
